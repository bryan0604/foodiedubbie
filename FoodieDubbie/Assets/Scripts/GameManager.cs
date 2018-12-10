using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public GameObject AoeSmall;

    [Range(0,10)]
    public int MaxSmallAoe;

    public int NumberHit_MultipleTarget;

    public List<GameObject> BigPool = new List<GameObject>();

    public GameObject MainPlayer;

    private void Start()
    {
        singleton = this;

        for (int i = 0; i < MaxSmallAoe; i++)
        {
            GameObject _aoe = Instantiate(AoeSmall);

            _aoe.SetActive(false);

            _aoe.transform.SetParent(transform);

            BigPool.Add(_aoe);
        }
    }
    // 1. Single 2. Multiple 3. Single Random 4. Multi Random  Large = X x Large
    public void OnCastingAbilitiesAtRandomPosition(int AmountOfCast, int code)
    {
        if(code == 1)
        {
            SingleAimOnPlayerPosition();
        }
        else if(code == 2)
        {
            StartCoroutine(MultiAimOnPlayerPosition());
        }
        else if(code == 3)
        {

        }
        else
        {
            if (AmountOfCast > MaxSmallAoe)
            {
                AmountOfCast = MaxSmallAoe;
            }

            for (int i = 0; i < AmountOfCast; i++)
            {
                RandomMultiple();
            }
        }

    }

    void SingleAimOnPlayerPosition()
    {
        Vector3 pos = MainPlayer.transform.position;

        GameObject _aoe = BigPool[Random.Range(0, BigPool.Count)];

        AoeManager _aoeM = _aoe.GetComponent<AoeManager>();

        if (_aoeM.isBeingCast)
        {
            SingleAimOnPlayerPosition();
        }
        else
        {
            _aoeM.OnBeingCast(pos);
        }
    }

    IEnumerator MultiAimOnPlayerPosition()
    {
        yield return new WaitForSeconds(1f);

        NumberHit_MultipleTarget -= 1;

        if(NumberHit_MultipleTarget <= 0)
        {
            
        }
        else
        {
            //Debug.Log("Spawn");

            SingleAimOnPlayerPosition();

            StartCoroutine(MultiAimOnPlayerPosition());
        }  
    }

    void RandomMultiple()
    {
        float Rand = Random.Range(-5, 5);

        Vector3 pos = new Vector3(Rand,0.5f,Rand);

        GameObject _aoe = BigPool[Random.Range(0, BigPool.Count)];

        AoeManager _aoeM = _aoe.GetComponent<AoeManager>();

        if (_aoeM.isBeingCast)
        {
            RandomMultiple();
        }
        else
        {
            _aoeM.OnBeingCast(pos);
        }
    }

    public void OnExplode(GameObject ObjectToKeep)
    {
        ObjectToKeep.SetActive(false);

        ObjectToKeep.transform.SetParent(transform);
    }
}
