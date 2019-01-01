using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour
{
    public GameObject LoginPanelInfo;
    public Text GooglePlayUsername;
    public Text GooglePlayCurrentLevel;
    public Text DisplayInfo;
    public Button GooglePlaySignIn;
    public Button GooglePlayLeaderboard;
    public Button GooglePlayAchievement;
    public Button AddLevelPoints;
    private int CurrentLevel=1;

    private void Awake()
    {
        if(GooglePlaySignIn)
        {
            GooglePlaySignIn.onClick.AddListener(TestAuthLogin);
        }

        if(GooglePlayLeaderboard)
        {
            GooglePlayLeaderboard.onClick.AddListener(TestShowLeaderboard);
        }

        if(GooglePlayAchievement)
        {
            GooglePlayAchievement.onClick.AddListener(TestShowAchievement);
        }

        if(AddLevelPoints)
        {
            AddLevelPoints.onClick.AddListener(OnUpdateClearedLevel);
        }

        OnCheckingGooglePlayUser();
    }

    void OnCheckingGooglePlayUser()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            DisplayInfo.text = "You have already logged on";
        }
        else
        {
            DisplayInfo.text = "You have not log in";
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowLoginInfo();
        }
    }

    public void TestAuthLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log("Success or Fail = " + success);

            Debug.Log(Social.localUser.userName + " " + Social.localUser.id + " " + Social.localUser.isFriend + " " + Social.localUser.authenticated);
            
            if (success)
            {
                ShowLoginInfo();
            }
        });
    }

    void ShowLoginInfo()
    {
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.LEFT);

        GooglePlayUsername.text = Social.localUser.userName;

        ILeaderboard lb = PlayGamesPlatform.Instance.CreateLeaderboard();

        lb.id = GPGSIds.leaderboard_test_leaderboard_01;

        lb.LoadScores(ok =>
        {
            GooglePlayCurrentLevel.text = lb.localUserScore.value.ToString();
        });

        Game_GlobalInfo.singleton.Player_LatestDefeatedLevel = int.Parse(GooglePlayCurrentLevel.text)+1;

        LoginPanelInfo.SetActive(true);

        OnCheckingGooglePlayUser();

        Game_GlobalInfo.singleton.OnUpdatePlayerInfo(Social.localUser.userName);
    }

    public void TestShowAchievement()
    {
        Social.ShowAchievementsUI();
    }

    public void TestShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100 , success => { });
    }

    public void OnUpdateClearedLevel()
    {
        CurrentLevel++;

        AddScoreToLeaderboard(GPGSIds.leaderboard_test_leaderboard_01, CurrentLevel);

        GooglePlayCurrentLevel.text = CurrentLevel.ToString();
    }

    public void AddScoreToLeaderboard(string id, long score)
    {
        Social.ReportScore(score, id, success => { });
    }
}
