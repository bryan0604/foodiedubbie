using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public GameObject AdvantageBuffs;
    public GameObject DisadvantageBuffs;
    public GameObject AoeSmall;
    public GameObject MainPlayer;
    public GameObject Platform;
    public ParticleSystem ExplosionEffects_Lv1;
    [Range(0,10)]
    public int MaxSmallAoe;
    [Range(0, 10)]
    public int MaxBuffs_Quantity;
    public int Buffs_Quantity;
    public int NumberHit_MultipleTarget;
    public List<GameObject> BigPool = new List<GameObject>();
    public List<ParticleSystem> SpecialEffects_Pools = new List<ParticleSystem>();
    public List<Damage_Buffs> AdvantageBuff_Pools = new List<Damage_Buffs>();
    public List<Damage_Buffs> DisadvantageBuff_Pools = new List<Damage_Buffs>();
    private bool HitPlatform;

    private void Start()
    {
        singleton = this;

        //Damage Buffs
        for (int i = 0; i < MaxBuffs_Quantity; i++)
        {
            Damage_Buffs DB = Instantiate(AdvantageBuffs.GetComponent<Damage_Buffs>());

            DB.transform.SetParent(transform);

            AdvantageBuff_Pools.Add(DB);

            Damage_Buffs DB2 = Instantiate(DisadvantageBuffs.GetComponent<Damage_Buffs>());

            DB2.transform.SetParent(transform);

            DisadvantageBuff_Pools.Add(DB2);
        }

        //AOEs
        for (int i = 0; i < MaxSmallAoe; i++)
        {
            GameObject _aoe = Instantiate(AoeSmall);

            _aoe.SetActive(false);

            _aoe.transform.SetParent(transform);

            BigPool.Add(_aoe);

            ParticleSystem _se = Instantiate(ExplosionEffects_Lv1);

            _se.gameObject.SetActive(false);

            _se.transform.SetParent(transform);

            SpecialEffects_Pools.Add(_se);
        }
    }

    #region Boss Abilities
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
            SingleRandom();
        }
        else
        {
            if (AmountOfCast > MaxSmallAoe)
            {
                AmountOfCast = MaxSmallAoe;
            }

            for (int i = 0; i < AmountOfCast; i++)
            {
                SingleRandom();
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

    void SingleRandom()
    {
        float X, Z, RandomX, RandomZ;
        X = (Platform.transform.localScale.x / 2) - 0.5f;
        Z = (Platform.transform.localScale.z / 2) - 0.5f;

        RandomX = Random.Range(-X, X);
        RandomZ = Random.Range(-Z, Z);

        Vector3 TargetPoint = new Vector3(RandomX, 0.5f, RandomZ);

        TargetPoint = Platform.transform.TransformPoint(TargetPoint / 2f);

        Collider[] Objects = Physics.OverlapSphere(TargetPoint, 0.1f);

        foreach (var item in Objects)
        {
            if (item.gameObject.tag == "Platforms")
            {
                HitPlatform = true;
            }
        }

        if (HitPlatform)
        {
            Debug.DrawLine(transform.position, TargetPoint, Color.cyan, 15f);

            GameObject _aoe = BigPool[Random.Range(0, BigPool.Count)];

            AoeManager _aoeM = _aoe.GetComponent<AoeManager>();

            if (_aoeM.isBeingCast)
            {
                SingleRandom();
            }
            else
            {
                _aoeM.OnBeingCast(TargetPoint);
            }
        }
        else
        {
            //Debug.DrawLine(transform.position, TargetPoint, Color.red, 15f);
        }
    }

    public void OnExplode(GameObject ObjectToKeep)
    {
        ObjectToKeep.SetActive(false);

        ObjectToKeep.transform.SetParent(transform);
    }

    public void OnSpecialEffectsEnded(ParticleSystem _se)
    {
        _se.gameObject.SetActive(false);

        _se.transform.SetParent(transform);
    }

    #endregion

    #region Buffs Sector Managemenet
    public void AdvantageBuffsRandomPosition(int Level)
    {
        if(Level == 1)
        {
            float X, Z, RandomX, RandomZ;
            X = (Platform.transform.localScale.x / 2) - 0.5f;
            Z = (Platform.transform.localScale.z / 2) - 0.5f;

            RandomX = Random.Range(-X, X);
            RandomZ = Random.Range(-Z, Z);

            Vector3 TargetPoint = new Vector3(RandomX, 0.5f, RandomZ);

            TargetPoint = Platform.transform.TransformPoint(TargetPoint / 2f);

            Collider[] Objects = Physics.OverlapSphere(TargetPoint, 0.1f);

            foreach (var item in Objects)
            {
                if (item.gameObject.tag == "Platforms")
                {
                    HitPlatform = true;
                }
            }

            if(HitPlatform)
            {
                Damage_Buffs _db = AdvantageBuff_Pools[Random.Range(0, AdvantageBuff_Pools.Count)];

                _db.OnBeingPlaced(TargetPoint);
            }
            else
            {

            }
        }
    }

    public void DisadvantageBuffsRandomPosition(int Level)
    {
        if (Level == 1)
        {
            float X, Z, RandomX, RandomZ;
            X = (Platform.transform.localScale.x / 2) - 0.5f;
            Z = (Platform.transform.localScale.z / 2) - 0.5f;

            RandomX = Random.Range(-X, X);
            RandomZ = Random.Range(-Z, Z);

            Vector3 TargetPoint = new Vector3(RandomX, 0.5f, RandomZ);

            TargetPoint = Platform.transform.TransformPoint(TargetPoint / 2f);

            Collider[] Objects = Physics.OverlapSphere(TargetPoint, 0.1f);

            foreach (var item in Objects)
            {
                if (item.gameObject.tag == "Platforms")
                {
                    HitPlatform = true;
                }
            }

            if (HitPlatform)
            {
                Damage_Buffs _db = DisadvantageBuff_Pools[Random.Range(0, DisadvantageBuff_Pools.Count)];

                _db.OnBeingPlaced(TargetPoint);
            }
            else
            {

            }
        }
    }
    #endregion
}
