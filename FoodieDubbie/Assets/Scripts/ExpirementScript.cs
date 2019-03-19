using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExpirementScript : MonoBehaviour
{
    
    //public Dictionary<string, TestAccountData> AccountData = new Dictionary<string, TestAccountData>();

    //BadGuy bg1 = new BadGuy("Harvey", 50);
    //BadGuy bg2 = new BadGuy("Magneto", 100);

    ////You can place variables into the Dictionary with the
    ////Add() method.
    //badguys.Add("gangster", bg1);
    //    badguys.Add("mutant", bg2);

    //    BadGuy magneto = badguys["mutant"];

    //BadGuy temp = null;

    //    //This is a safer, but slow, method of accessing
    //    //values in a dictionary.
    //    if(badguys.TryGetValue("birds", out temp))
    //    {
    //        //success!
    //    }
    //    else
    //    {
    //        //failure!
    //    }
    //}
    private void Start()
    {
        Dictionary<string, TestAccountData> AccountData = new Dictionary<string, TestAccountData>();
        TestAccountData AC_1 = new TestAccountData("Anduin Lothar", 1, 0);
        TestAccountData AC_2 = new TestAccountData("Luna Celes", 3, 1);
        TestAccountData AC_3 = new TestAccountData("Chun Han", 5, 0);
        TestAccountData AC_4 = new TestAccountData("Micheal", 10, 2);
        TestAccountData AC_5 = new TestAccountData("David", 4, 5);

        AccountData.Add("Archer", AC_1);
        AccountData.Add("Boxer", AC_2);
        AccountData.Add("Swordsman", AC_3);
        AccountData.Add("Wizard", AC_4);
        AccountData.Add("Archer1", AC_5);

        //TestAccountData AccountDetails_Temp = AccountData["Archer"];

        TestAccountData temp = null;

        //Debug.Log(AccountDetails_Temp.Name);

        foreach (var item in AccountData)
        {
            if(item.Key == "Archer")
            {
                Debug.Log(item + " " + item.Value.Name + " " + item.Value.Level +" " + item.Value.Power);
            }
        }
    }
}
