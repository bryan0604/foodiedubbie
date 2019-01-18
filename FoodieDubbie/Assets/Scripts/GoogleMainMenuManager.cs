﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        singleTonGooglePlayMenu = this;

        if(gpm==null)
        {
            gpm = Behaviour.FindObjectOfType<GooglePlayManager>();
        }
    }

    private void Start()
    {
        GooglePlaySignIn.onClick.AddListener(gpm.TestAuthLogin);
        GooglePlayLeaderboard.onClick.AddListener(gpm.TestShowLeaderboard);
        GooglePlayAchievement.onClick.AddListener(gpm.TestShowAchievement);
        AddLevelPoints.onClick.AddListener(() => gpm.OnUpdateClearedLevel(0));

    }
}