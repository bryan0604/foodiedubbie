using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_MainMenu : MonoBehaviour
{
    public LoadingManager LM;
    public CharacterSelectionManager CSM;
    public AchievementsManagement AchievementsManagement;
    public LevelsManager _levelManager;
    public Button Button_Resume;
    public Button Button_Login;
    public Button Button_Quit;
    public Button Button_RankList;
    public Button Button_LevelSelect;
    public Button Button_OnCheckNextLevel;
    public Button Button_Avatar;
    public Text Text_ChosenAvatar;
    public GameObject Group_MainMenus;
    public GameObject Group_AvatarMenus;
    public GameObject _InfoPanel_Bg;
    public GameObject _LevelsPanel;
    public GameObject _MainPanel;
    public GameObject _LoginInfoPanel;
    public GameObject _LivesPanel;

    private void Awake()
    {
        if (LM == null) LM = Behaviour.FindObjectOfType<LoadingManager>();
        if (AchievementsManagement == null) AchievementsManagement = Behaviour.FindObjectOfType<AchievementsManagement>();

        Button_Resume.onClick.AddListener(Continue);
        Button_Quit.onClick.AddListener(Quit);
        Button_LevelSelect.onClick.AddListener(SelectLevel);
        Button_OnCheckNextLevel.onClick.AddListener(OnCheckingLevel);
        Button_Avatar.onClick.AddListener(AvatarSelect);
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

            // get next scene number for activate and check achievements
            //SceneManager.LoadScene(Game_GlobalInfo.singleton.Player_NextLevel);

            //StartCoroutine(_levelManager.DelayChangeScene(Game_GlobalInfo.singleton.Player_NextLevel));

            PlayerAbilitiesManager.PAM.OnEnteringGameLevels();

            int Temp_NextLevel = Game_GlobalInfo.singleton.Player_NextLevel;

            StartCoroutine(DelayChangeScene(Temp_NextLevel));

            AchievementsManagement.AchievementTracking(Temp_NextLevel, true);
        }
    }

    IEnumerator DelayChangeScene(int SceneInt)
    {
        LM.LoadingScreen(true);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(SceneInt, LoadSceneMode.Single);

        LM.LoadingScreen(false);
    }

    void AvatarSelect()
    {
        Group_AvatarMenus.gameObject.SetActive(true);
        Group_MainMenus.gameObject.SetActive(false);

        CSM.CheckPage();
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

    public void OnLeavingAvatarPage()
    {
        //CSM.GetAvatarSelection();
        Group_AvatarMenus.SetActive(false);
        Group_MainMenus.SetActive(true);
    }
}
