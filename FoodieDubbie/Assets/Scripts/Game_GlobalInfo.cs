﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_GlobalInfo : MonoBehaviour
{
    public static Game_GlobalInfo singleton;
    public string Player_Username = "AnduinLothar";
    public List<int> Player_Achievement;
    public int Player_NextLevel = 0;
    public int Player_LatestDefeatedLevel = 0;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
        //singleton = this;
        DontDestroyOnLoad(this);

        OnNextLevelCheck();
    }

    public void OnDefeatedLevel(int Level)
    {
        if(Level == Player_LatestDefeatedLevel)
        {
            Debug.Log("Player defeated a level that was defeated earlier ago");
        }
        else
        {
            Player_LatestDefeatedLevel = Level;

            Player_Achievement.Add(Level);
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
        Player_Achievement = new List<int>(0);
        Player_LatestDefeatedLevel = 0;
    }
}
