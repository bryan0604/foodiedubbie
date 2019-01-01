using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginInfoManager : MonoBehaviour
{
    public Button ShowLoginInfo;
    public Button HideLoginInfo;
    public GameObject LoginInfoPanel;

    private void Awake()
    {
        if(ShowLoginInfo)
        {
            ShowLoginInfo.onClick.AddListener(() => { LoginPanel(true); });
        }

        if(HideLoginInfo)
        {
            HideLoginInfo.onClick.AddListener(() => { LoginPanel(false); });
        }
    }

    void LoginPanel(bool ShowPanel)
    {
        if(ShowPanel)
        {
            LoginInfoPanel.SetActive(true);

            ShowLoginInfo.gameObject.SetActive(false);
        }
        else
        {
            LoginInfoPanel.SetActive(false);

            ShowLoginInfo.gameObject.SetActive(true);
        }
    }

}
