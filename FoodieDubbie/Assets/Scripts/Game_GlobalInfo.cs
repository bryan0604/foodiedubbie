using UnityEngine;
using System;

public class Game_GlobalInfo : MonoBehaviour
{
    public static Game_GlobalInfo singleton;
    public string Player_Username = "AnduinLothar";
    public int Player_Lives = 5;
    public int Player_NextLevel = 0;
    public int Player_LatestDefeatedLevel = 0;
    //public float Hours;
    //public float Minutes;
    //public float Seconds;
    //public float Milliseconds;
    //public float TotalTimeInGame;

    DateTime currentDate;
    DateTime oldDate;

    private void Awake()
    {
        //PlayerPrefs.DeleteKey("TimeOutTiming");

        if (PlayerPrefs.HasKey("TimeOutTiming"))
        {
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
        }
        else
        {
            Debug.Log("No key found!");
        }

        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this);

        OnNextLevelCheck();
    }

    private void Update()
    {
        //Debug.Log(Time.realtimeSinceStartup);
        //if (Milliseconds >= 60)
        //{
        //    Seconds++;
        //    Milliseconds = 0;

            //    if(Seconds >= 60)
            //    {
            //        Minutes++;
            //        Seconds = 0;

            //        if(Minutes >= 60)
            //        {
            //            Hours++;
            //            Minutes = 0;

            //            if(Hours >= 24)
            //            {
            //                //Days ++;
            //                //Hours=0;
            //            }
            //        }
            //    }
        //}
        //else
        //{
        //    Milliseconds++;
        //}
    }

    private void OnApplicationQuit()
    {
        //TotalTimeInGame = Seconds;

        //PlayerPrefs.SetFloat("TimeOutTiming", TotalTimeInGame);

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
