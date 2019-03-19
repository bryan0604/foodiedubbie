using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestAccountData : IComparable<TestAccountData> 
{
    //[SerializeField]
    //public struct AllDatas
    //{
    //    public string Name;
    //    public int Level;
    //    public float Power;
    //}

    public string Name;
    public int Level;
    public float Power;

    public int CompareTo(TestAccountData other)
    {
        throw new NotImplementedException();
    }

    public TestAccountData(string _Name, int _Level , float _Power)
    {
        //AllDatas tempData;

        //tempData.Name = _Name;
        //tempData.Level = _Level;
        //tempData.Power = _Power;

        Name = _Name;
        Level = _Level;
        Power = _Power;

    }
}
