using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager singleton;
    [Tooltip("Cast Every Sec")]
    public float _aoeSmall=5f;
    public float _aoeMedium;
    public int CastAmount_Aoe = 5;
    public int CastAmount_Buffs = 4;
    public int HealthPoints = 100;

    private void Start()
    {
        singleton = this;
    }

    //private void Update()
    //{
    //if (Input.GetKeyDown(KeyCode.Escape))
    //{
    //    DropBuffs(true, true, 2,1);
    //}
    //if (Input.GetKeyDown(KeyCode.Space))
    //{
    //    DropBuffs(false, true, 2, 1);
    //}
    //}

    //void SingleTarget()
    //{
    //    GameManager.singleton.OnCastingAbilitiesAtRandomPosition(CastAmount_Aoe, 1);
    //}

    //void RandomTarget()
    //{
    //    GameManager.singleton.OnCastingAbilitiesAtRandomPosition(CastAmount_Aoe, 4);
    //}

    //void MultipleTarget()
    //{
    //    GameManager.singleton.NumberHit_MultipleTarget = CastAmount_Aoe;

    //    GameManager.singleton.OnCastingAbilitiesAtRandomPosition(CastAmount_Aoe, 2);
    //}

    //void SingleRandom()
    //{
    //    GameManager.singleton.OnCastingAbilitiesAtRandomPosition(CastAmount_Aoe, 3);
    //}

    //void DropBuffs(bool isGoodBuffs, bool isRandomPlaced, int Quantity, int Level)
    //{
    //    if(isRandomPlaced)
    //    {
    //        if (isGoodBuffs)
    //        {
    //            for (int i = 0; i < Quantity; i++)
    //            {
    //                GameManager.singleton.AdvantageBuffsRandomPosition(Level);
    //            }
    //        }
    //        else
    //        {
    //            for (int i = 0; i < Quantity; i++)
    //            {
    //                GameManager.singleton.DisadvantageBuffsRandomPosition(Level);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (isGoodBuffs)
    //        {
    //            for (int i = 0; i < Quantity; i++)
    //            {

    //            }
    //        }
    //        else
    //        {
    //            for (int i = 0; i < Quantity; i++)
    //            {

    //            }
    //        }
    //    }  
    //}

    public void OnTakingDamage(int Amount)
    {
        HealthPoints -= Amount;
    }
}
