using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_MainMenu : MonoBehaviour
{
    //public List<string> BossBattle_Levels = new List<string>();
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

    private void Awake()
    {
        Button_Resume.onClick.AddListener(Continue);
        Button_Quit.onClick.AddListener(Quit);
        Button_LevelSelect.onClick.AddListener(SelectLevel);
        Button_OnCheckNextLevel.onClick.AddListener(OnCheckingLevel);
    }

    //void StartGame()
    //{
    //    Debug.Log("Start Game - Level 1");
    //    SceneManager.LoadScene(BossBattle_Levels[0],LoadSceneMode.Single);
    //}

    void Quit()
    {
        Application.Quit();
    }

    void Continue()
    {
        //Debug.Log("Next Level = " + Game_GlobalInfo.singleton.Player_NextLevel);

        if (Game_GlobalInfo.singleton.Player_NextLevel >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Scene coming soon!");

            return;
        }

        SceneManager.LoadScene(Game_GlobalInfo.singleton.Player_NextLevel);
    }

    void SelectLevel()
    {
        _LevelsPanel.SetActive(true);
        _MainPanel.SetActive(false);
        _LoginInfoPanel.SetActive(false);
    }

    public void BackFromLevelSelect()
    {
        _MainPanel.SetActive(true);
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
