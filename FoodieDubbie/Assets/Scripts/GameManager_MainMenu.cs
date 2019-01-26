using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_MainMenu : MonoBehaviour
{
    public LevelsManager _levelManager;
    public Button Button_Resume;
    public Button Button_Login;
    public Button Button_Quit;
    public Button Button_RankList;
    public Button Button_LevelSelect;
    public Button Button_OnCheckNextLevel;

    public GameObject _InfoPanel_Bg;
    public GameObject _LevelsPanel;
    public GameObject _MainPanel;
    public GameObject _LoginInfoPanel;
    public GameObject _LivesPanel;

    private void Awake()
    {

        Button_Resume.onClick.AddListener(Continue);
        Button_Quit.onClick.AddListener(Quit);
        Button_LevelSelect.onClick.AddListener(SelectLevel);
        Button_OnCheckNextLevel.onClick.AddListener(OnCheckingLevel);
    }

    void Quit()
    {
        Application.Quit();
    }

    void Continue()
    {
        if(Game_GlobalInfo.singleton.Player_Lives <= _levelManager.LevelsRequirements[Game_GlobalInfo.singleton.Player_NextLevel-1] )
        {
            NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 0);

            return;
        }
        else
        {
            if (Game_GlobalInfo.singleton.Player_NextLevel >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogWarning("Scene coming soon!");

                return;
            }

            SceneManager.LoadScene(Game_GlobalInfo.singleton.Player_NextLevel);
        }
    }

    void SelectLevel()
    {
        _LevelsPanel.SetActive(true);
        _MainPanel.SetActive(false);
        _LoginInfoPanel.SetActive(false);
        _LivesPanel.SetActive(false);
    }

    public void BackFromLevelSelect()
    {
        _MainPanel.SetActive(true);
        _LivesPanel.SetActive(true);
        _LevelsPanel.SetActive(false);
        if (Social.localUser.authenticated)
        {
            _LoginInfoPanel.SetActive(true);
        }
    }

    void OnCheckingLevel()
    {
        NoticePromptManager.singLeTon.OnCheckNextLevel();
    }
}
