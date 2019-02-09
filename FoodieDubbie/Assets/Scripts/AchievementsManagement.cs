using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManagement : MonoBehaviour
{
    public Game_GlobalInfo _globalInfo;

    public static AchievementsManagement singletion;

    public float Level1_Timer = 10f;

    public bool Achievement_Level1;

    private void Awake()
    {
        singletion = this;
    }

    public void Activate_Level1_Achievement_Tracking(bool activate)
    {
        if(activate && _globalInfo.AvatarsList[0] != true)
        {
            StartCoroutine(Level1_Tracking());
        }
        else
        {
            Debug.Log("Rejecting Activation Level 1 Tracking");

            StopAllCoroutines();
        }
    }

    IEnumerator Level1_Tracking()
    {
        yield return new WaitForSeconds(Level1_Timer);

        Debug.Log("Achievement Increased");
    }
}
