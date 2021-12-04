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
