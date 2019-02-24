using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectsManager : MonoBehaviour
{
    public static ScreenEffectsManager SEM;
    public float ChangeSpeed;
    public float ChangeSpeedMultiply;
    public float AlphaMax;
    private float CurrentAlphaAmount;
    private bool isChanging;
    private bool isDecreasing;
    public Image RedScreen;
    Color C;

    private void Awake()
    {
        SEM = this;
        AlphaMax = AlphaMax/255;
        CurrentAlphaAmount = RedScreen.color.a;
        C = RedScreen.color;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    if(isChanging || isDecreasing)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        isChanging = true;
        //    }
        //}

        if(isChanging)
        {
            if (RedScreen.color.a < AlphaMax)
            {
                CurrentAlphaAmount += Mathf.Lerp(0,1,Time.deltaTime*ChangeSpeed*ChangeSpeedMultiply);
                
                C.a =CurrentAlphaAmount;

                RedScreen.color = C;

                
            }
            else
            {
                isChanging = false;
                isDecreasing = true;
            }
        }

        if(isDecreasing)
        {
            if (RedScreen.color.a < 0)
            {
                isDecreasing = false;

            }
            else
            {
                CurrentAlphaAmount -= Mathf.Lerp(0, 1, Time.deltaTime * ChangeSpeed * ChangeSpeedMultiply);

                C.a = CurrentAlphaAmount;

                RedScreen.color = C;
            }
        }
    }

    public void ActivateScreenRed()
    {
        if (isChanging || isDecreasing)
        {
            return;
        }
        else
        {
            isChanging = true;
        }
    }
}
