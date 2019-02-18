﻿using UnityEngine;
using System.Collections.Generic;

public class Game_GlobalInfo : MonoBehaviour
{
    public static Game_GlobalInfo singleton;
    public string Player_Username = "AnduinLothar";
    public int Player_Lives = 1000;
    public int Player_NextLevel = 0;
    public int Player_LatestDefeatedLevel = 0;
    public int Player_SelectedCharacter;
    public List<bool> AvatarsList = new List<bool>();
    public static bool AppStarted;
    public bool ThisIsMain;

    private void Awake()
    {
        if(AppStarted == false)
        {
            AppStarted = true;
            if (PlayerPrefs.HasKey("PlayerData_Lives"))
            {
                #region UNUSED
                //Debug.Log("Key found!");

                //TotalTimeInGame = PlayerPrefs.GetFloat("TimeOutTiming");

                //Seconds = TotalTimeInGame;

                //Debug.Log("Current time = " + Seconds);

                //currentDate = System.DateTime.Now;

                //long temp = Convert.ToInt64(PlayerPrefs.GetString("TimeOutTiming"));

                //DateTime oldDate = DateTime.FromBinary(temp);

                //print("old Date: " + oldDate);
                //print("new Date: " + currentDate);

                //TimeSpan difference = currentDate.Subtract(oldDate);

                //print("Difference - Minutes:" + difference.Minutes + " Seconds:" + difference.Seconds + " Total:" + difference.TotalSeconds);

                //PlayerPrefs.DeleteKey("TimeOutTiming");
                #endregion

                Player_Lives = PlayerPrefs.GetInt("PlayerData_Lives");

                Debug.Log("Loading Lives = " + Player_Lives);
            }
            else
            {
                Debug.Log("New player no Live found!");

                Player_Lives = 100;
            }

            ThisIsMain = true;

            singleton = this;

            DontDestroyOnLoad(gameObject);

            //Debug.Log(Game_GlobalInfo.singleton + " is main = " + ThisIsMain, gameObject);

            OnNextLevelCheck();
        }
        else
        {
            
        }
    }

    private void Start()
    {
        if(!ThisIsMain)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Player_Lives = 0;
            PlayerPrefs.DeleteKey("PlayerData_Lives");
        }
    }

    private void OnApplicationQuit()
    {
        //TotalTimeInGame = Seconds;

        PlayerPrefs.SetInt("PlayerData_Lives", Player_Lives);

        Debug.Log("Saving Lives = " + Player_Lives);

        //Debug.Log("Saving timeOut timing = " + TotalTimeInGame);

        //Savee the current system time as a string in the player prefs class
        //PlayerPrefs.SetString("TimeOutTiming", System.DateTime.Now.ToBinary().ToString());

        //print("Saving this date to prefs: " + System.DateTime.Now);
    }

    public void OnDefeatedLevel(int Level)
    {
        if(Level <= Player_LatestDefeatedLevel)
        {
            Debug.Log("Player defeated a level that was defeated earlier ago");
        }
        else
        {
            Player_LatestDefeatedLevel = Level;

            GooglePlayManager.singletonGooglePlay.OnUpdateClearedLevel(Level);

            //Player_Achievement.Add(Level);
        }

        OnNextLevelCheck();
    }

    void OnNextLevelCheck()
    {
        Player_NextLevel = Player_LatestDefeatedLevel + 1;
    }

    public void OnUpdatePlayerInfo(string _username, int _level)
    {
        Player_Username = _username;
        Player_LatestDefeatedLevel = _level;

        OnNextLevelCheck();
    }

    public void ClearAllData()
    {
        Player_Username = "";
        Player_NextLevel = 0;
        //Player_Achievement = new List<int>(0);
        Player_LatestDefeatedLevel = 0;
    }
}
