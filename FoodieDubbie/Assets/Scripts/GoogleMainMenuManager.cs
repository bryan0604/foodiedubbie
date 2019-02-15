using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class GoogleMainMenuManager : MonoBehaviour
{
    public static GoogleMainMenuManager singleTonGooglePlayMenu;
    public GooglePlayManager gpm;

    public GameObject LoginPanelInfo;
    public Text GooglePlayUsername;
    public Text GooglePlayCurrentLevel;
    public Text DisplayInfo;
    public Button GooglePlaySignIn;
    public Button GooglePlayLeaderboard;
    public Button GooglePlayAchievement;
    public Button AddLevelPoints;
    public Button UpgradeAvatar;

    private void Awake()
    {
        singleTonGooglePlayMenu = this;

        if(gpm==null)
        {
            gpm = Behaviour.FindObjectOfType<GooglePlayManager>();

            gpm.MenuManager = this;
        }

        OnCheckingGooglePlayUser();
    }

    private void Start()
    {
        GooglePlaySignIn.onClick.AddListener(gpm.TestAuthLogin);
        GooglePlayLeaderboard.onClick.AddListener(gpm.TestShowLeaderboard);
        GooglePlayAchievement.onClick.AddListener(gpm.TestShowAchievement);
        AddLevelPoints.onClick.AddListener(() => gpm.OnUpdateClearedLevel(0));
        UpgradeAvatar.onClick.AddListener(()=> { gpm.UnlockAchievement(1, 5, true); });

    }

    public void OnCheckingGooglePlayUser()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            DisplayInfo.text = "You have already logged on";

            GooglePlaySignIn.gameObject.SetActive(false);

            LoginPanelInfo.SetActive(true);

            gpm.GetUserInfos();

            gpm.DebugMaster.OnDebugging("You have already logged on");
        }
        else
        {
            DisplayInfo.text = "You have not log in";

            gpm.DebugMaster.OnDebugging("You have not log in");
        }
    }
}
