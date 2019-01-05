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
        Button_OnCheckNextLevel.onClick.AddListener(OnCheckNextLevel);
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
        Debug.Log("Next Level = " + Game_GlobalInfo.singleton.Player_NextLevel);

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

    void OnCheckNextLevel()
    {
        _InfoPanel_Bg.gameObject.SetActive(true);

        Text _text = _InfoPanel_Bg.transform.GetChild(0).GetComponent<Text>();

        _text.text = "Next Level = " + Game_GlobalInfo.singleton.Player_NextLevel;

        StartCoroutine(ClosingInfoPanel());
    }

    IEnumerator ClosingInfoPanel()
    {
        yield return new WaitForSeconds(5f);

        _InfoPanel_Bg.gameObject.SetActive(false);

        if(_InfoPanel_Bg.GetComponent<Animator>().isActiveAndEnabled)
        {
            _InfoPanel_Bg.GetComponent<Animator>().Play("InfoPanel", 0, 0f);
        }
        else
        {
            
        }       
    }
}
