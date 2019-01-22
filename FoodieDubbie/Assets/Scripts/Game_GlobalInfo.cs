using UnityEngine;

public class Game_GlobalInfo : MonoBehaviour
{
    public static Game_GlobalInfo singleton;
    public string Player_Username = "AnduinLothar";
    public int Player_Lives = 5;
    public int Player_NextLevel = 0;
    public int Player_LatestDefeatedLevel = 0;
    public float Hours;
    public float Minutes;
    public float Seconds;
    public float Milliseconds;
    public float TotalTimeInMillisecondsInGame;

    private void Awake()
    {
        Hours = Mathf.Round((TotalTimeInMillisecondsInGame / 60f)*100)/100;
        float _hours = Mathf.Round((Hours % 1)*100);
        Hours = Mathf.Round(Hours);
        Debug.Log(_hours);


        //Minutes = Mathf.Round(((Hours % 1)* 60));
        //Seconds = Mathf.Round(((Minutes % 1) * 60));
        //Milliseconds = (Seconds % 1) * 60;


        if (PlayerPrefs.HasKey("TimeOutTiming"))
        {
            Debug.Log("Key found!");

            TotalTimeInMillisecondsInGame = PlayerPrefs.GetInt("TimeOutTiming");

            // S = 20000
            // S = 20000 / 60 = 333.33minutes
            // S = 0.33 * 60 = 19.8 seconds
            // S = 0.8 * 60 = 48 milliseconds
            // Time = 333 minutes 19 seconds 48 milliseconds



            PlayerPrefs.DeleteKey("TimeOutTiming");
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
        //if(Milliseconds >= 60)
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
        //1 day = 24 hours
        //1 hour = 60 Minutes
        //1 minutes = 60 seconds
        //1 seconds = 60 milliseconds
        //TotalTimeInMillisecondsInGame = (Hours * 60 * 60 * 60) + (Minutes * 60 * 60) + (Seconds * 60) + Milliseconds;

        //PlayerPrefs.SetInt("TimeOutTiming", TotalTimeInMillisecondsInGame);

        //Debug.Log("Saving timeOut timing = " );
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
