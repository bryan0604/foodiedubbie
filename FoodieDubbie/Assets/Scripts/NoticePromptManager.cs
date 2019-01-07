using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePromptManager : MonoBehaviour
{
    public static NoticePromptManager singLeTon;
    public GameObject _InfoPanel_Bg;
    public Text TextContent;

    private void Awake()
    {
        singLeTon = this;
    }

    public void OnCheckNextLevel()
    {
        string tempString = "Next Level = " + Game_GlobalInfo.singleton.Player_NextLevel;

        OnActivateNoticeBoard(tempString);
    }

    public void OnActivateNoticeBoard(string _Content)
    {
        _InfoPanel_Bg.gameObject.SetActive(true);

        TextContent.text = _Content;

        StartCoroutine(ClosingInfoPanel());
    }

    IEnumerator ClosingInfoPanel()
    {
        yield return new WaitForSeconds(5f);

        _InfoPanel_Bg.gameObject.SetActive(false);

        if (_InfoPanel_Bg.GetComponent<Animator>().isActiveAndEnabled)
        {
            _InfoPanel_Bg.GetComponent<Animator>().Play("InfoPanel", 0, 0f);
        }
        else
        {

        }
    }
}
