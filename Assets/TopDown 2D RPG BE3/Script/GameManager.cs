using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public talkManager talkmanager;
    public QuestManager questManager;
    public TypeEffect talk;
    public Animator portraitAnim;
    public Image portraitImg;
    public GameObject scanObject;
    public Animator talkPanel;
    public bool isAction;
    public int talkIndex;
    public Sprite prevPortrait;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id,objData.isNpc);

        talkPanel.SetBool("isShow",isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";


        if (talk.isAnim)//�׼ǹ�ư���� ��ȭ�� ������ �ѱ������ if��
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);

            talkData = talkmanager.GetTalk(id + questTalkIndex, talkIndex);
        }


        //��ȭ�� ������ �迭�� ������ NUll�̱⿡ �����ϼ��ְ�[false] ����� ��ȭ���� �ʱ�ȭ�� ����
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        if(isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);

            portraitImg.sprite = talkmanager.GetPortrait(id,int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1,1,1,1);
            
            if (prevPortrait != portraitImg.sprite)//�ʻ�ȭ �ִϸ��̼�
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
            
        }
        else
        {
            talk.SetMsg(talkData);
            portraitImg.color = new Color(0,0,0,0);
        }

        isAction = true;
        talkIndex++;
    }
}
