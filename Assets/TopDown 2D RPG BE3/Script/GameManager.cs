using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public talkManager talkmanager;
    public QuestManager questManager;
    public TypeEffect talk;
    public Text questTalk;
    public Animator portraitAnim;
    public Image portraitImg;
    public GameObject scanObject;
    public Animator talkPanel;
    public bool isAction;
    public int talkIndex;
    public Sprite prevPortrait;
    public GameObject MenuSet;
    public GameObject Player;

    void Start()
    {
        GameLoad();
        questTalk.text = questManager.CheckQuest();
    }

    private void Update()
    {

        if(Input.GetButtonDown("Cancel"))//�޴�â ���� ����
        {
            if (MenuSet.activeSelf)
            {
                MenuSet.SetActive(false);
            }
            else
            {
                MenuSet.SetActive(true);
            }
            
        }
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
            questTalk.text = questManager.CheckQuest(id);
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

    public void GameSave()
    {
        PlayerPrefs.SetFloat("Player.X", Player.transform.position.x);
        PlayerPrefs.SetFloat("Player.Y", Player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetFloat("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        MenuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (PlayerPrefs.HasKey("Player.X"))
            return;

        float x = PlayerPrefs.GetFloat("Player.X");
        float y = PlayerPrefs.GetFloat("Player.Y");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        Player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.Controlobject();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
