using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour
{
    [System.Serializable]
    public class AvatarIncrementPointsManager
    {
        public List<int> Avatar_Pts_Increment = new List<int>(3);
    }

    public AvatarIncrementPointsManager AIPM;
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
        if (MenuManager == null)
        {
            MenuManager = FindObjectOfType<GoogleMainMenuManager>();
        }

        singletonGooglePlay = this;

        //Debug.Log(AIPM.Avatar00_Points_Increment);
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

                NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 9);
            }
            else
            {
                _LoadManager.LoadingScreen(false);

                NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 10);
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

                _LoadManager.LoadingScreen(false);

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

                    _gameglobal.AvatarsList[3] = true;
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
        LoadingManager.singleton.LoadingScreen(true);

        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 6);

            LoadingManager.singleton.LoadingScreen(false);
            return;
        }

        if (isTopUp)
        {
            for (int i = 0; i < _gameglobal.AvatarsList.Count; i++)
            {
                if(_gameglobal.AvatarsList[i])
                {

                }
                else
                {
                    DebugMaster.OnDebugging("Topping up on avatar = " + (i-1).ToString());

                    ToppingUpPoints(i-1);

                    return;
                }
            }

            DebugMaster.OnDebugging("All Avatars has been unlocked");
        }
        else
        {
            if (_Code == 1)
            {
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI__DU0doGEAIQCA", _ExpAmount, (bool success) =>
                {
                    if(success)
                    {
                        NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 7);
                    }
                });
            }
        }
        LoadingManager.singleton.LoadingScreen(false);
    }

    void ToppingUpPoints(int _Code)
    {
        if (_Code == 0)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkI__DU0doGEAIQCA", AIPM.Avatar_Pts_Increment[0], (bool success) =>
            {
                if (success)
                {
                    LivesManager.singleTonnie.OnConsumeLives(-1000);

                    DebugMaster.OnDebugging("Code = 0, Achievement = Avatar 0, Points = " + AIPM.Avatar_Pts_Increment[0]);
                }
                else
                {
                    NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 3);
                }
            });
        }
        else if (_Code == 1)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkI__DU0doGEAIQBw", AIPM.Avatar_Pts_Increment[1], (bool success) =>
            {
                if (success)
                {
                    LivesManager.singleTonnie.OnConsumeLives(-1000);

                    DebugMaster.OnDebugging("Code = 1, Achievement = Avatar 1, Points = " + AIPM.Avatar_Pts_Increment[1]);
                }
                else
                {
                    NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 3);
                }
            });
        }
        else if (_Code == 2)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkI__DU0doGEAIQBQ", AIPM.Avatar_Pts_Increment[2], (bool success) =>
            {
                if (success)
                {
                    LivesManager.singleTonnie.OnConsumeLives(-1000);

                    DebugMaster.OnDebugging("Code = 2, Achievement = Avatar 2, Points = " + AIPM.Avatar_Pts_Increment[2]);
                }
                else
                {
                    NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 3);
                }
            });
        }

        LoadingManager.singleton.LoadingScreen(false);
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
