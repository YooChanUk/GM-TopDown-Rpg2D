using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public talkManager talkmanager;
    public QuestManager questManager;
    public Text talkText;
    public Image portraitImg;
    public GameObject scanObject;
    public GameObject talkPanel;
    public bool isAction;
    public int talkIndex;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id,objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);

        string talkData = talkmanager.GetTalk(id + questTalkIndex,talkIndex);

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
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkmanager.GetPortrait(id,int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1,1,1,1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(0,0,0,0);
        }

        isAction = true;
        talkIndex++;
    }
}
