using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public List<Button> AllLevels = new List<Button>();
    public Button BackButton;
    public GameManager_MainMenu Main;

    private void OnEnable()
    {
        //Debug.Log("Level Manager begins to process...");
        BackButton.onClick.AddListener(Back);

        ProcessLevelsProgress();
    }

    void ProcessLevelsProgress()
    {
        int _OpeningLevelAt = Game_GlobalInfo.singleton.Player_NextLevel;

        for (int i = 0; i < _OpeningLevelAt; i++)
        {
            AllLevels[i].interactable = true;
            AllLevels[i].onClick.AddListener(()=> { LevelManagement(i); });
        }
    }

    void LevelManagement(int Level)
    {
        SceneManager.LoadScene(Level, LoadSceneMode.Single);
    }

    void Back()
    {
        Main.BackFromLevelSelect();
    }
}
