using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    public static PauseGameManager singleTon;
    public GameObject PausePanel;
    public Button Button_MainMenu;
    public Button Button_Guidance;
    public Button Button_Pause;
    public Button Button_Unpause;

    private void Awake()
    {
        singleTon = this;
        Button_Pause.onClick.AddListener(() => { PauseActivate(true); });
        Button_Unpause.onClick.AddListener(() => { PauseActivate(false); });
        Button_MainMenu.onClick.AddListener(ReturnToMainMenu);
    }

    public void PauseActivate(bool activate)
    {
        if(activate)
        {
            Time.timeScale = 0;

            PausePanel.SetActive(true);

            Button_Pause.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;

            PausePanel.SetActive(false);

            Button_Pause.gameObject.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        AchievementsManagement.singletion.StopAchievementsTracking();

        SceneManager.LoadScene(0);

        //GooglePlayManager.singletonGooglePlay.OnCheckingGooglePlayUser();
    }

    public void ShowGuidance()
    {

    }
}
