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

    protected string GoogleSignInTracking = "SignIn"; 

    public AvatarIncrementPointsManager AIPM;
    public static GooglePlayManager singletonGooglePlay;
    public DebugManager DebugMaster;
    public AchievementsManagement _AchieveManager;
    public Game_GlobalInfo _gameglobal;
    public GoogleMainMenuManager MenuManager;
    public LoadingManager _LoadManager;
    public NoticeManager _NoticeManage;
    public string GooglePlayUsername="";
    public int GooglePlayCurrentLevel=1;

    private void Awake()
    {
        _LoadManager.LoadingScreen(true);

        if (MenuManager == null)
        {
            MenuManager = FindObjectOfType<GoogleMainMenuManager>();
        }

        if(_NoticeManage == null)
        {
            _NoticeManage = NoticeManager.SingleTonyStark;
        }

        singletonGooglePlay = this;

        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.Activate();

            PlayGamesPlatform.DebugLogEnabled = true;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            print("Android");

            if(PlayerPrefs.HasKey(GoogleSignInTracking))
            {
                DebugMaster.OnDebugging(" Old player - Account found!");

                TestAuthLogin();
            }
            else
            {
                DebugMaster.OnDebugging(" New player - No login records found!");
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            print("Iphone");
        }
        else
        {
            print("Editor");
        }
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
        DebugMaster.OnDebugging("Processing Google Play Login");

        _LoadManager.LoadingScreen(true);

        Social.localUser.Authenticate((bool success) =>
        {
            DebugMaster.OnDebugging("Success or Fail = " + success);

            if (success)
            {
                ShowLoginInfo();

                _NoticeManage.OnActivationNoticeBoard(true, 9);
            }
            else
            {
                _LoadManager.LoadingScreen(false);

                _NoticeManage.OnActivationNoticeBoard(true, 10);
            }
        });
    }

    void ShowLoginInfo()
    {
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);

        MenuManager.OnCheckingGooglePlayUser();

        PlayerPrefs.SetString(GoogleSignInTracking, "True");
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

    public void UnlockAchievement(int Level, int _ExpAmount, bool isTopUp)
    {
        _LoadManager.LoadingScreen(true);

        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            _NoticeManage.OnActivationNoticeBoard(true, 6);

            _LoadManager.LoadingScreen(false);
            return;
        }
        else if (Game_GlobalInfo.singleton.Player_Lives < 1000)
        {
            _NoticeManage.OnActivationNoticeBoard(true, 4);

            _LoadManager.LoadingScreen(false);
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
            if (Level == 1)
            {
                DebugMaster.OnDebugging(PlayGamesPlatform.Instance.GetAchievement("CgkI__DU0doGEAIQCA").IsUnlocked.ToString());

                PlayGamesPlatform.Instance.IncrementAchievement("CgkI__DU0doGEAIQCA", _ExpAmount, (bool success) =>
                {
                    if(success)
                    {
                        _NoticeManage.OnActivationNoticeBoard(true, 7);
                    }
                });
            }
            else if (Level == 2)
            {
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI__DU0doGEAIQBw", _ExpAmount, (bool success) =>
                {
                    if(success)
                    {
                        _NoticeManage.OnActivationNoticeBoard(true, 7);
                    }
                });
            }
        }
        _LoadManager.LoadingScreen(false);
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
                    _NoticeManage.OnActivationNoticeBoard(true, 3);
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
                    _NoticeManage.OnActivationNoticeBoard(true, 3);
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
                    _NoticeManage.OnActivationNoticeBoard(true, 3);
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
