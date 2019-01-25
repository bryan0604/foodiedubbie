using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelsManager : MonoBehaviour
{
    public List<int> LevelsRequirements = new List<int>();
    public List<Button> AllLevels = new List<Button>();
    public Button BackButton;
    public GameManager_MainMenu Main;
    public NoticeManager _NoticeManager;

    private void OnEnable()
    {
        //Debug.Log("Level Manager begins to process...");
        if(_NoticeManager==null)
        {
            _NoticeManager = FindObjectOfType<NoticeManager>();
        }

        BackButton.onClick.AddListener(Back);

        ProcessLevelsProgress();

        //gameObject.SetActive(false);
    }

    void ProcessLevelsProgress()
    {
        int _OpeningLevelAt = Game_GlobalInfo.singleton.Player_NextLevel;

        for (int i = 0; i < _OpeningLevelAt; i++)
        {
            AllLevels[i].interactable = true;
            AllLevels[i].onClick.AddListener(delegate { DebugButton(EventSystem.current.currentSelectedGameObject); });
            //Debug.Log(AllLevels[i].name);
        }
    }

    void DebugButton(GameObject number)
    {
        

        foreach (var item in AllLevels)
        {
            if(item.gameObject == number)
            {
                Debug.Log(item.transform.GetSiblingIndex() + 1);

                if(item.transform.GetSiblingIndex() + 1 >= SceneManager.sceneCountInBuildSettings)
                {
                    _NoticeManager.OnActivationNoticeBoard(true, 1);
                }
                else
                {
                    if(Game_GlobalInfo.singleton.Player_Lives <= LevelsRequirements[item.transform.GetSiblingIndex()])
                    {
                        _NoticeManager.OnActivationNoticeBoard(true, 0);

                        return;
                    }
                    SceneManager.LoadScene(item.transform.GetSiblingIndex() + 1, LoadSceneMode.Single);
                }
            }
        }
    }

    void Back()
    {
        Main.BackFromLevelSelect();
    }
}
