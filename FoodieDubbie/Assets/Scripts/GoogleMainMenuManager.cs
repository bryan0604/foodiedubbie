using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleMainMenuManager : MonoBehaviour
{
    public static GoogleMainMenuManager singleTonGooglePlayMenu;
    public GameObject LoginPanelInfo;
    public Text GooglePlayUsername;
    public Text GooglePlayCurrentLevel;
    public Text DisplayInfo;
    public Button GooglePlaySignIn;
    public Button GooglePlayLeaderboard;
    public Button GooglePlayAchievement;
    public Button AddLevelPoints;

    private void Awake()
    {
        singleTonGooglePlayMenu = this;
    }
}
