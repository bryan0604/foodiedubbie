using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeManager : MonoBehaviour
{
    public static NoticeManager SingleTonyStark;
    [TextArea]
    public List<string> Descriptions = new List<string>();
    public Text Text_Content;
    public GameObject NoticePanel;
    public Button Button_Closed;

    private void Awake()
    {
        SingleTonyStark = this;
    }

    private void Start()
    {
        Button_Closed.onClick.AddListener(() => { OnActivationNoticeBoard(false, 0); });
    }

    public void OnActivationNoticeBoard(bool toActivate, int Code)
    {
        if(toActivate)
        {
            //Time.timeScale = 0;

            NoticePanel.SetActive(toActivate);
            UpdateContext(Code);
        }
        else
        {
            //Time.timeScale = 1;

            NoticePanel.SetActive(toActivate);
        }
    }

    void UpdateContext(int Code)
    {
        Text_Content.text = Descriptions[Code];
    }
}
