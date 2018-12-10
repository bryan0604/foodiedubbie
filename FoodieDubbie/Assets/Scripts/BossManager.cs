using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Tooltip("Cast Every Sec")]
    public float _aoeSmall=5f;
    public float _aoeMedium;
    public int CastAmount = 5;

    public GameManager GameManagerr;

    private void Start()
    {
        GameManagerr.NumberHit_MultipleTarget = CastAmount;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MultipleTarget();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomTarget();
        }
    }

    void SingleTarget()
    {
        GameManager.singleton.OnCastingAbilitiesAtRandomPosition(CastAmount, 1);
    }

    void RandomTarget()
    {
        GameManager.singleton.OnCastingAbilitiesAtRandomPosition(CastAmount,4);
    }

    void MultipleTarget()
    {
        GameManagerr.NumberHit_MultipleTarget = CastAmount;

        GameManager.singleton.OnCastingAbilitiesAtRandomPosition(CastAmount, 2);
    }
}
