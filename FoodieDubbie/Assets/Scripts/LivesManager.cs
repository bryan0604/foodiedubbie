using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System;

public class LivesManager : MonoBehaviour
{
    public static LivesManager singleTonnie;
    public Button Button_WatchAds;
    public Text Text_Lives;
    public bool AdsIsShowing;

    private void Awake()
    {
        singleTonnie = this;
    }

    private void Start()
    {
        Button_WatchAds.onClick.AddListener(ShowRewardedAd);

        OnUpdateLivesDisplay();
    }

    public void ShowRewardedAd()
    {
        if(AdsIsShowing == false)
        {
            if (Advertisement.IsReady("AddingLives"))
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show("AddingLives", options);
                AdsIsShowing = true;
            }
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");

                TreasureManagement.TM.OnShowingRewards(1);

                AdsIsShowing = false;
                //NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 8);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
    
    public void OnConsumeLives(int Quantity)
    {

        NoticeManager.SingleTonyStark.OnActivationNoticeBoard(true, 2);

        Game_GlobalInfo.singleton.Player_Lives += Quantity;

        OnUpdateLivesDisplay();
    }

    public void OnAddingLives(int Quantity)
    {
        if (Game_GlobalInfo.singleton.Player_Lives >= 5)
        {
            //Debug.Log("Max lives!");
            Game_GlobalInfo.singleton.Player_Lives += Quantity;
        }
        else
        {
            Game_GlobalInfo.singleton.Player_Lives+= Quantity;
        }

        OnUpdateLivesDisplay();
    }

    public void OnUpdateLivesDisplay()
    {
        Text_Lives.text = Game_GlobalInfo.singleton.Player_Lives.ToString();
    }
}
