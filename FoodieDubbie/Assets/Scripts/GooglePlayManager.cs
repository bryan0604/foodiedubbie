using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour
{
    public static GooglePlayManager singletonGooglePlay;

    public GoogleMainMenuManager MenuManager;

    private int CurrentLevel=1;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        singletonGooglePlay = this;

        if(MenuManager==null)
        {
            MenuManager = Behaviour.FindObjectOfType<GoogleMainMenuManager>();
        }

        if(MenuManager.GooglePlaySignIn)
        {
            MenuManager.GooglePlaySignIn.onClick.AddListener(TestAuthLogin);
        }

        if(MenuManager.GooglePlayLeaderboard)
        {
            MenuManager.GooglePlayLeaderboard.onClick.AddListener(TestShowLeaderboard);
        }

        if(MenuManager.GooglePlayAchievement)
        {
            MenuManager.GooglePlayAchievement.onClick.AddListener(TestShowAchievement);
        }

        if(MenuManager.AddLevelPoints)
        {
            MenuManager.AddLevelPoints.onClick.AddListener(OnUpdateClearedLevel);
        }

        OnCheckingGooglePlayUser();
    }

    void OnCheckingGooglePlayUser()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            MenuManager.DisplayInfo.text = "You have already logged on";
        }
        else
        {
            MenuManager.DisplayInfo.text = "You have not log in";
        }
    }

    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();

        PlayGamesPlatform.DebugLogEnabled = true;

        //Social.CreateAchievement();

        //Social.LoadAchievements(success => { Debug.Log(success); });
    }

    public void TestAuthLogin()
    {
        LoadingManager.singleton.LoadingScreen(true);

        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log("Success or Fail = " + success);

            Debug.Log(Social.localUser.userName + " " + Social.localUser.id + " " + Social.localUser.isFriend + " " + Social.localUser.authenticated);
            
            if (success)
            {
                ShowLoginInfo();
            }
            else
            {
                LoadingManager.singleton.LoadingScreen(false);
            }
        });
    }

    void ShowLoginInfo()
    {
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.LEFT);

        MenuManager.GooglePlayUsername.text = Social.localUser.userName;

        Social.LoadScores(GPGSIds.leaderboard_test_leaderboard_01, success =>
         {
             foreach (var item in success)
             {
                 if(item.userID == Social.localUser.id)
                 {
                     //DebugManager.OnDebugging(item.value.ToString());

                     MenuManager.GooglePlayCurrentLevel.text = item.value.ToString();

                     CurrentLevel = int.Parse(item.value.ToString());

                     Game_GlobalInfo.singleton.OnUpdatePlayerInfo(Social.localUser.userName, CurrentLevel);
                 }
                 //DebugManager.OnDebugging( item.value.ToString());
             }
         });

        LoadingManager.singleton.LoadingScreen(false);

        MenuManager.LoginPanelInfo.SetActive(true);

        OnCheckingGooglePlayUser();

        //});
    }

    void TestSignOut()
    {

    }

    public void TestShowAchievement()
    {
        Social.ShowAchievementsUI();
    }

    public void TestShowLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_test_leaderboard_01);
        //Social.ShowLeaderboardUI(GPGSIds.leaderboard_test_leaderboard_01)
    }

    public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100 , success => { });
    }

    public void OnUpdateClearedLevel()
    {
        CurrentLevel++;

        AddScoreToLeaderboard(GPGSIds.leaderboard_test_leaderboard_01, CurrentLevel);

        MenuManager.GooglePlayCurrentLevel.text = CurrentLevel.ToString();
    }

    public void AddScoreToLeaderboard(string id, long score)
    {
        Social.ReportScore(score, id, success => { });
    }
}
