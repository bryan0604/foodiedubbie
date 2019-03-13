using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManagement : MonoBehaviour
{
    public Game_GlobalInfo _globalInfo;

    public GooglePlayManager _googleplay;

    public static AchievementsManagement singletion;
 
    public List<float> Level_Unlock_Timer = new List<float>();

    public bool Achievement_Level1;

    private void Awake()
    {
        singletion = this;
    }

    public void AchievementTracking(int Level, bool Activate)
    {
        if (Activate)
        {
            StartCoroutine(Level_Tracking_Timer(Level));
        }
        else
        {
            StopAchievementsTracking();
        }
    }

    IEnumerator Level_Tracking_Timer(int CurrentLevel)
    {
        Debug.Log("Activating achievement tracking system level 1");

        yield return new WaitForSeconds(Level_Unlock_Timer[CurrentLevel]);

        //Debug.Log("Achievement Increased");

        _googleplay.UnlockAchievement(1, 1, false);
    }

    public void StopAchievementsTracking()
    {
        StopAllCoroutines();
    }

    private void OnApplicationQuit()
    {
        StopAchievementsTracking();
    }
}
