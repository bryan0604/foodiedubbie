using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour
{
    public Text GooglePlayUsername;
    public int GooglePlayCurrentLevel;
    public Button GooglePlaySignIn;
    public Button GooglePlayLeaderboard;

    private void Awake()
    {
        if(GooglePlaySignIn)
        {
            GooglePlaySignIn.onClick.AddListener(TestAuthLogin);
        }

        if(GooglePlayLeaderboard)
        {
            GooglePlayLeaderboard.onClick.AddListener(TestShowAchievement);
        }
    }

    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();

        //Social.CreateAchievement();

        //Social.LoadAchievements(success => { Debug.Log(success); });
    }

    public void TestAuthLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log("Success or Fail = " + success);

            Debug.Log(Social.localUser.userName + " " + Social.localUser.id + " " + Social.localUser.isFriend + " " + Social.localUser.authenticated);
            
            if (success)
            {
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);

                GooglePlayUsername.text = Social.localUser.userName; 
            }

            ((PlayGamesLocalUser)Social.localUser).GetStats((rc, stats) =>
            {
                // see  CommonStatusCodes for all values.       // -1 means cached stats, 0 is succeess
         
                if (rc <= 0 && stats.HasDaysSinceLastPlayed())
                {
                    Debug.Log("It has been " + stats.DaysSinceLastPlayed + " days");
                }
            });
        });

        //Debug.Log(config.AccountName + " " + config.Scopes);
    }

    public void TestShowAchievement()
    {
        Debug.Log("Showing Achievements");

        Social.Active.ShowAchievementsUI();

        Social.ShowAchievementsUI();
    }

    public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100 , success => { });
    }

    public void IncrementAchievementProgress(string id, int AmountIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, AmountIncrement, success => { });
    }
}
