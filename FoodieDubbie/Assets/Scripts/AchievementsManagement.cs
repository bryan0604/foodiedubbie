using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManagement : MonoBehaviour
{
    public Game_GlobalInfo _globalInfo;

    public GooglePlayManager _googleplay;

    public static AchievementsManagement singletion;
 
    public float Level1_Timer = 10f;

    public bool Achievement_Level1;

    private void Awake()
    {
        singletion = this;
    }

    public void AchievementTracking(int Level, bool Activate)
    {
        if(Level == 1)
        {
            //if (Activate && _globalInfo.AvatarsList[0] != true)
            if (Activate )
            {
                StartCoroutine(Level1_Tracking());
            }
            else
            {
                Debug.Log("Rejecting Activation Level 1 Tracking");

                StopAchievementsTracking();
            }
        }
    }

    IEnumerator Level1_Tracking()
    {
        Debug.Log("Activating achievement tracking system level 1");

        yield return new WaitForSeconds(Level1_Timer);

        Debug.Log("Achievement Increased");

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
