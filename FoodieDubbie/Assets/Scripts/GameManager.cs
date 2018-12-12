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
    public int SkillOne_Quantity;
    public int SkillTwo_Quantity;
    public int SkillThree_Quantity;
    public int SkillFour_Quantity;
    public List<AoeManager> Aoes_Pools = new List<AoeManager>();
    public List<ParticleSystem> SpecialEffects_Pools = new List<ParticleSystem>();
    public List<BuffsManager> AdvantageBuff_Pools = new List<BuffsManager>();
    public List<BuffsManager> DisadvantageBuff_Pools = new List<BuffsManager>();
    public List<Transform> Buffs_SpecificPosition = new List<Transform>();
    public Vector3 SingleTarget_Position;
    private bool HitPlatform;
    private int _skillfourquantity;

    private void Awake()
    {
        singleton = this;

        _skillfourquantity = SkillFour_Quantity;

        //Damage Buffs
        for (int i = 0; i < MaxBuffs_Quantity; i++)
        {
            //Advantage buff
            BuffsManager DB = Instantiate(AdvantageBuffs.GetComponent<BuffsManager>());

            DB.transform.SetParent(transform);

            AdvantageBuff_Pools.Add(DB);

            //Disadvantage buff
            BuffsManager DB2 = Instantiate(DisadvantageBuffs.GetComponent<BuffsManager>());

            DB2.transform.SetParent(transform);

            DisadvantageBuff_Pools.Add(DB2);
        }

        //AOEs
        for (int i = 0; i < MaxSmallAoe; i++)
        {
            AoeManager _aoe = Instantiate(AoeSmall.GetComponent<AoeManager>());

            _aoe.gameObject.SetActive(false);

            _aoe.transform.SetParent(transform);

            Aoes_Pools.Add(_aoe);

            ParticleSystem _se = Instantiate(ExplosionEffects_Lv1);

            _se.gameObject.SetActive(false);

            _se.transform.SetParent(transform);

            SpecialEffects_Pools.Add(_se);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SkillOne_SingleRandom();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillTwo_MultiRandom();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkillThree_SingleTarget();
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SkillFour_MultiTarget();
        }       
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            DropAdvantageBuff();
        }
    }

    public void SkillOne_SingleRandom()
    {
        TargetManagement(true, SkillOne_Quantity, false, 1, false, true);
    }

    public void SkillTwo_MultiRandom()
    {
        TargetManagement(true, SkillTwo_Quantity, true, 1, false, true);
    }

    public void SkillThree_SingleTarget()
    {
        TargetManagement(false, SkillThree_Quantity, false, 1, false, true);
    }

    public void SkillFour_MultiTarget()
    {
        StartCoroutine(Subs_SkillFour());
    }

    IEnumerator Subs_SkillFour()
    {
        yield return new WaitForSeconds(0.5f);

        if(_skillfourquantity <= 0)
        {
            _skillfourquantity = SkillFour_Quantity;
        }
        else
        {
            TargetManagement(false, 1, false, 1, false, true);

            _skillfourquantity -= 1;

            SkillFour_MultiTarget();
        }

    }

    public void DropAdvantageBuff()
    {
        BuffsManagement(2, true, true);
    }

    public void DropDisadvantageBuff()
    {

    }

    void BuffsManagement(int Quantity, bool IsOnSpecificLocations, bool isAdvantageBuff)
    {
        if(IsOnSpecificLocations)
        {
            if (isAdvantageBuff)
            {
                int i = 0;
                foreach (var item in AdvantageBuff_Pools)
                {
                    if(i >= Buffs_SpecificPosition.Count)
                    {
                        return;
                    }
                    else
                    {
                        if (!item.gameObject.activeInHierarchy)
                        {
                            item.OnBeingPlaced(Buffs_SpecificPosition[i].position);

                            i++;
                        }
                    }
                }
            }
            else
            {

            }
        }
    }

    void TargetManagement(bool isRandom, int Quantity, bool isTheSameTimeSpawning, int Level, bool isBuff ,bool isAbility)
    {
        if(isRandom)
        {
            for (int i = 0; i < Quantity; i++)
            {
                float X, Z, RandomX, RandomZ;
                X = (Platform.transform.localScale.x / 2) - 0.5f;
                Z = (Platform.transform.localScale.z / 2) - 0.5f;

                RandomX = Random.Range(-X, X);
                RandomZ = Random.Range(-Z, Z);

                SingleTarget_Position = new Vector3(RandomX, 1.55f, RandomZ);

                SingleTarget_Position = Platform.transform.TransformPoint(SingleTarget_Position / 2f);

                Collider[] Objects = Physics.OverlapSphere(SingleTarget_Position, 0.1f);

                foreach (var item in Objects)
                {
                    if (item.gameObject.tag == "Platforms")
                    {
                        HitPlatform = true;
                    }
                }

                if (HitPlatform)
                {
                    Debug.DrawLine(transform.position, SingleTarget_Position, Color.green, 5f);

                    if(isAbility)
                    {
                        AbilitiesManagement(Level);
                    }
                }
                else
                {
                    Debug.DrawLine(transform.position, SingleTarget_Position, Color.red, 5f);
                }
            }            
        }
        else
        {
            for (int i = 0; i < Quantity; i++)
            {
                if(isAbility)
                {
                    SingleTarget_Position = MainPlayer.transform.position;

                    AbilitiesManagement(Level);
                }
            }
        }
    }

    void AbilitiesManagement(int Level)
    {
        AoeManager Aoe;

        if(Level == 1)
        {
            foreach (AoeManager item in Aoes_Pools)
            {
                if(item.Level == Level && !item.gameObject.activeInHierarchy)
                {
                    Aoe = item;

                    Aoe.OnBeingCast(SingleTarget_Position);

                    return;
                }
                else
                {

                }
            }
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
}
