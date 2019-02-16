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
    public DebugManager DebugMaster;
    public AchievementsManagement _AchieveManager;
    public Game_GlobalInfo _gameglobal;
    public GoogleMainMenuManager MenuManager;
    public LoadingManager _LoadManager;
    public string GooglePlayUsername="";
    public int GooglePlayCurrentLevel=1;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        if (MenuManager == null)
        {
            MenuManager = FindObjectOfType<GoogleMainMenuManager>();
        }

        singletonGooglePlay = this;

    }

    private void Start()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.Activate();

            PlayGamesPlatform.DebugLogEnabled = true;
        }
        //OnCheckingGooglePlayUser();
    }

    //public void OnCheckingGooglePlayUser()
    //{
    //    if (PlayGamesPlatform.Instance.localUser.authenticated)
    //    {
    //        MenuManager.DisplayInfo.text = "You have already logged on";

    //        MenuManager.GooglePlaySignIn.gameObject.SetActive(false);

    //        MenuManager.LoginPanelInfo.SetActive(true);

    //        GetUserInfos();

    //        DebugMaster.OnDebugging("You have already logged on");
    //    }
    //    else
    //    {
    //        MenuManager.DisplayInfo.text = "You have not log in";

    //        DebugMaster.OnDebugging("You have not log in");
    //    }
    //}

    public void TestAuthLogin()
    {
        Debug.Log("Processing Google Play Login");

        _LoadManager.LoadingScreen(true);

        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log("Success or Fail = " + success);

            if (success)
            {
                ShowLoginInfo();
            }
            else
            {
                _LoadManager.LoadingScreen(false);
            }
        });
    }

    void ShowLoginInfo()
    {
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);

        MenuManager.OnCheckingGooglePlayUser();

        //OnCheckingGooglePlayUser();

        //GetUserInfos();
    }

    public void GetUserInfos()
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

        Social.LoadAchievements(achievements =>
        {
            if (achievements.Length > 0)
            {
                CheckAchievementId(achievements);
            }
            else
            {
                Debug.Log("No achievements returned");
            }
        });
    }

    void CheckAchievementId(IAchievement[] achievements)
    {
        foreach (IAchievement achievement in achievements)
        {
            if (achievement.id == "CgkI__DU0doGEAIQCA") // test achievement
            {
                if (achievement.completed)
                {
                    DebugMaster.OnDebugging(" Avatar 00 - Unlocked");

                    _gameglobal.AvatarsList[1] = true;
                }
                else
                {
                    DebugMaster.OnDebugging(" Avatar 00 - Locked");
                }
            }
            else if (achievement.id == "CgkI__DU0doGEAIQBw")
            {
                if (achievement.completed)
                {
                    DebugMaster.OnDebugging(" Avatar 01 - Unlocked");

                    _gameglobal.AvatarsList[2] = true;
                }
                else
                {
                    DebugMaster.OnDebugging(" Avatar 01 - Locked");
                }
            }
            else if (achievement.id == "CgkI__DU0doGEAIQBQ")
            {
                if (achievement.completed)
                {
                    DebugMaster.OnDebugging(" Avatar 02 - Unlocked");

                    _gameglobal.AvatarsList[2] = true;
                }
                else
                {
                    DebugMaster.OnDebugging(" Avatar 02 - Locked");
                }
            }
        }
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

    public void UnlockAchievement(int _Code, int _ExpAmount, bool isTopUp)
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 6);
            return;
        }

        if (isTopUp)
        {
            ToppingUpPoints(_Code, _ExpAmount);
        }
        else
        {
            if (_Code == 1)
            {
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI__DU0doGEAIQCA", _ExpAmount, (bool success) =>
                {
                    if(success)
                        NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 7);
                });
            }
        }
    }

    void ToppingUpPoints(int _Code, int _ExpAmount)
    {
        if (_Code == 1)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkI__DU0doGEAIQCA", _ExpAmount, (bool success) =>
            {
                if (success)
                {
                    LivesManager.singleTonnie.OnConsumeLives(-1000);
                }
                else
                {
                    NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 3);
                }
            });
        }
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
