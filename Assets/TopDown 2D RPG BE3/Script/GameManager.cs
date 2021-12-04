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


        if (talk.isAnim)//액션버튼으로 대화를 빠르게 넘기기위한 if문
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);

            talkData = talkmanager.GetTalk(id + questTalkIndex, talkIndex);
        }


        //대화가 끝나면 배열의 다음은 NUll이기에 움직일수있게[false] 만들고 대화상태 초기화후 종료
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
            
            if (prevPortrait != portraitImg.sprite)//초상화 애니메이션
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
