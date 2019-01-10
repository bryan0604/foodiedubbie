﻿using System.Collections;
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

    public string GooglePlayUsername="";
    public int GooglePlayCurrentLevel=1;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        singletonGooglePlay = this;

        if (MenuManager == null)
        {
            MenuManager = Behaviour.FindObjectOfType<GoogleMainMenuManager>();
        }

        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.Activate();

            PlayGamesPlatform.DebugLogEnabled = true;
        }
        OnCheckingGooglePlayUser();
    }

    public void OnCheckingGooglePlayUser()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            MenuManager.DisplayInfo.text = "You have already logged on";

            MenuManager.GooglePlaySignIn.gameObject.SetActive(false);

            MenuManager.LoginPanelInfo.SetActive(true);

            GetUserInfos();
        }
        else
        {
            MenuManager.DisplayInfo.text = "You have not log in";

            Debug.LogWarning("You have not log in");
        }
    }

    public void TestAuthLogin()
    {
        Debug.Log("Processing Google Play Login");

        LoadingManager.singleton.LoadingScreen(true);

        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log("Success or Fail = " + success);

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
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);

        GetUserInfos();

        OnCheckingGooglePlayUser();
    }

    void GetUserInfos()
    {
        MenuManager.GooglePlayUsername.text = Social.localUser.userName;

        GooglePlayUsername = Social.localUser.userName; // username

        PlayGamesPlatform.Instance.LoadScores
            (
            GPGSIds.leaderboard_test_leaderboard_02,
            LeaderboardStart.PlayerCentered,
            100,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (data) =>
            {
                Debug.Log("USERNAME = " + Social.localUser.userName + "| SCORE = " + data.PlayerScore.formattedValue);

                MenuManager.GooglePlayCurrentLevel.text = data.PlayerScore.formattedValue.ToString(); // score

                GooglePlayCurrentLevel = int.Parse(MenuManager.GooglePlayCurrentLevel.text);

                Game_GlobalInfo.singleton.OnUpdatePlayerInfo(Social.localUser.userName, GooglePlayCurrentLevel);

                LoadingManager.singleton.LoadingScreen(false);

                MenuManager.LoginPanelInfo.SetActive(true);
            });
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
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_test_leaderboard_02);
        //Social.ShowLeaderboardUI(GPGSIds.leaderboard_test_leaderboard_01)
    }

    public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100 , success => { });
    }

    public void OnUpdateClearedLevel(int _NewDefeatedLevel)
    {
        if(_NewDefeatedLevel == 0)
        {
            return;
        }
        
        AddScoreToLeaderboard(GPGSIds.leaderboard_test_leaderboard_02, _NewDefeatedLevel);

        //TestAuthLogin();
    }

    public void AddScoreToLeaderboard(string id, long score)
    {
        Social.ReportScore(score, id, success => { });
    }
}
