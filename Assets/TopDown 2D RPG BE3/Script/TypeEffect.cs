using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    string targetMsg;
    public GameObject EndCursor;
    AudioSource audioSource;

    public int CharPerSeconds;
    int index;
    Text msgText;
    float interval;

    public bool isAnim;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
 
    }

    // Update is called once per frame
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(true);
        isAnim = true;

        interval = 1.0f / CharPerSeconds;
        Debug.Log(interval);
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
      
        if(targetMsg[index] != ' ' && targetMsg[index] != '.')
        {
            audioSource.Play();
        }

        index++;
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        EndCursor.SetActive(true);
        isAnim = false;
    }
}
