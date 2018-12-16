using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_MainMenu : MonoBehaviour
{
    public List<string> BossBattle_Levels = new List<string>();
    public Button Button_StartGame;
    public Button Button_Login;
    public Button Button_Quit;
    public Button Button_RankList;
    public Button Button_Continue;
    public Button Button_OnCheckNextLevel;

    public GameObject _InfoPanel_Bg;

    private void Awake()
    {
        Button_StartGame.onClick.AddListener(StartGame);
        Button_Login.onClick.AddListener(Login);
        Button_Quit.onClick.AddListener(Quit);
        Button_RankList.onClick.AddListener(RankList);
        Button_Continue.onClick.AddListener(Continue);
        Button_OnCheckNextLevel.onClick.AddListener(OnCheckNextLevel);
    }

    void StartGame()
    {
        Debug.Log("Start Game - Level 1");
        SceneManager.LoadScene(BossBattle_Levels[0],LoadSceneMode.Single);
    }

    void Login()
    {

    }

    void Quit()
    {
        Application.Quit();
    }

    void RankList()
    {

    }

    void Continue()
    {
        Debug.Log("Next Level = " + Game_GlobalInfo.singleton.Player_NextLevel);
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
