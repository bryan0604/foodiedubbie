using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System;

public class LivesManager : MonoBehaviour
{
    public Button Button_WatchAds;
    public Text Text_Lives;

    private void Start()
    {
        Button_WatchAds.onClick.AddListener(ShowRewardedAd);

        OnUpdateLivesDisplay();
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("AddingLives"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("AddingLives", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");

                OnAddingLives();


                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    public void OnAddingLives()
    {
        if (Game_GlobalInfo.singleton.Player_Lives >= 5)
        {
            Debug.Log("Max lives!");
        }
        else
        {
            Game_GlobalInfo.singleton.Player_Lives++;
        }

        OnUpdateLivesDisplay();
    }

    public void OnUpdateLivesDisplay()
    {
        Text_Lives.text = Game_GlobalInfo.singleton.Player_Lives.ToString();
    }
}
