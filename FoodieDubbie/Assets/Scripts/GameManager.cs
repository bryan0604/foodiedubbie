using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    
    public GameObject MainPlayer;
    public GameObject Platform;
    public Button Button_Dismount;
    public GameVictory UI_GameRoundEnd;
    public Boss_1_Ultimate Boss1Ultimate;
    public Boss_1_Ultimate_2 Boss1UltimateTwo;
    public LineTargetingSystem AoeLineTargetingSystem;
    [Range(0,50)]
    public int MaxSmallAoe;
    [Range(0, 25)]
    public int MaxBuffs_Quantity;
    [Header("AOE's Quantity")]
    public int SkillOne_Quantity;
    public int SkillTwo_Quantity;
    public int SkillThree_Quantity;
    public int SkillFour_Quantity;
    [Header("All Aoes here!")]
    public List<GameObject> AllAoes = new List<GameObject>();
    [Header("Put all special effects here!")]
    public List<GameObject> AllSpecialEffects = new List<GameObject>();
    [Header("Put all buffs here!")]
    public List<GameObject> AllBuffTypes = new List<GameObject>();
    [HideInInspector]
    public List<AoeManager> Aoes_Pools = new List<AoeManager>();
    [HideInInspector]
    public List<ParticleSystem> SpecialEffects_Pools = new List<ParticleSystem>();
    [HideInInspector]
    public List<BuffsManager> Buff_Pools = new List<BuffsManager>();
    public List<Transform> Buffs_SpecificPosition = new List<Transform>();
    public List<Behaviour> BehaviourScripts_toDisable = new List<Behaviour>();
    [HideInInspector]
    public Vector3 SingleTarget_Position;
    public bool isGameEnd;
    public int CurrentGameLevel;
    private bool HitPlatform;
    private int _skillfourquantity;
    private int _normalHits;

    public delegate void BossSkills();
    public List<BossSkills> BossAbilities = new List<BossSkills>();

    private void Awake()
    {
        singleton = this;

        //Test - Void call from other script
        BossAbilities.Add(NormalAttack);//0
        BossAbilities.Add(SkillOne_SingleRandom);//1
        BossAbilities.Add(SkillTwo_MultiRandom);//2
        BossAbilities.Add(SkillThree_SingleTarget);//3
        BossAbilities.Add(SkillFour_MultiTarget);//4
        BossAbilities.Add(DropBuffsAtRandomSpot);//5
        BossAbilities.Add(DropDisadvantageBuff);//6
        BossAbilities.Add(DropAdvantageBuff);//7

        _skillfourquantity = SkillFour_Quantity;

        //Damage Buffs
        for (int i = 0; i < MaxBuffs_Quantity; i++)
        {
            for (int j = 0; j < AllBuffTypes.Count; j++)
            {
                BuffsManager DB = Instantiate(AllBuffTypes[j].GetComponent<BuffsManager>());

                DB.transform.SetParent(transform);

                Buff_Pools.Add(DB);
            }
        }

        //AOEs
        for (int i = 0; i < MaxSmallAoe; i++)
        {
            for (int j = 0; j < AllAoes.Count; j++)
            {
                AoeManager _aoe = Instantiate(AllAoes[j].GetComponent<AoeManager>());

                _aoe.gameObject.SetActive(false);

                _aoe.transform.SetParent(transform);

                Aoes_Pools.Add(_aoe);
            }


            for (int o = 0; o < AllSpecialEffects.Count; o++)
            {
                ParticleSystem _se = Instantiate(AllSpecialEffects[o].GetComponent<ParticleSystem>()); ;

                _se.gameObject.SetActive(false);

                _se.transform.SetParent(transform);

                SpecialEffects_Pools.Add(_se);
            }
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SkillOne_SingleRandom();
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    SkillTwo_MultiRandom();
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SkillThree_SingleTarget();
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    SkillFour_MultiTarget();
        //}       
        //if(Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    DropAdvantageBuff();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    DropDisadvantageBuff();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    DropBuffsAtRandomSpot();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    NormalAttack();
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    BossOne_UltimateOne();
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    BossOne_UltimateTwo();
        //}
        //if(Input.GetKeyDown(KeyCode.Q))
        //{
        //    SkillFive_SingleSpot();
        //}

        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    SkillOne_Upgraded();
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    SkillTwo_Upgraded();
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SkillThree_Upgraded();
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    SkillFour_Upgraded();
        //}
    }

    #region Ability/Skill Management
    public void SkillOne_Upgraded()
    {
        TargetManagement(true, SkillOne_Quantity, false,2, false, true, false, false);
    }

    public void SkillTwo_Upgraded()
    {
        TargetManagement(true, SkillTwo_Quantity, true, 2, false, true, false, false);
    }

    public void SkillThree_Upgraded()
    {
        TargetManagement(false, SkillThree_Quantity, false, 2, false, true, false, false);
    }

    public void SkillFour_Upgraded()
    {
        StartCoroutine(Subs_SkillFour_Upgraded());
    }

    public void SkillFive_SingleSpot()
    {
        TargetOnSpots();
    }

    public void BossOne_UltimateOne()
    {
        Boss1Ultimate.OnActivate();
    }

    public void BossOne_UltimateTwo()
    {
        Boss1UltimateTwo.OnActivate();
    }

    public void NormalAttack()
    {
        StartCoroutine(CastingNormalAttack(3, 1, 5f, 2f));
    }

    public void SkillOne_SingleRandom()
    {
        
        TargetManagement(true, SkillOne_Quantity, false, 1, false, true,false,false);
    }

    public void SkillTwo_MultiRandom()
    {
        TargetManagement(true, SkillTwo_Quantity, true, 1, false, true, false, false);
    }

    public void SkillThree_SingleTarget()
    {
        TargetManagement(false, SkillThree_Quantity, false, 1, false, true, false, false);
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
            TargetManagement(false, 1, false, 1, false, true, false, false);

            _skillfourquantity -= 1;

            SkillFour_MultiTarget();
        }
    }

    IEnumerator Subs_SkillFour_Upgraded()
    {
        yield return new WaitForSeconds(0.5f);

        if (_skillfourquantity <= 0)
        {
            _skillfourquantity = SkillFour_Quantity;
        }
        else
        {
            TargetManagement(false, 1, false, 2, false, true, false, false);

            _skillfourquantity -= 1;

            SkillFour_Upgraded();
        }
    }

    public void SkillSix_AoeLine_Small()
    {
        AoeLineTargetingSystem.CastLineAoe();
    }

    public void DropAdvantageBuff()
    {
        BuffsManagement(1, true, true, false);
    }

    public void DropDisadvantageBuff()
    {
        BuffsManagement(1, true, false, false);
    }

    public void DropBuffsAtRandomSpot()
    {
        BuffsManagement(2, false, false, true);
    }

    IEnumerator CastingNormalAttack(int Hits, int Damage, float Cooldowns, float CastingTime)
    {
        yield return new WaitForSeconds(CastingTime);

        if(Hits>0)
        {
            StartCoroutine(HitManagement(Hits));
        }
    }

    IEnumerator HitManagement(int h)
    {
        _normalHits = h;

        yield return new WaitForSeconds(0.25f);

        SpecialEffectsManager SpecialEffect;

        if(_normalHits > 0)
        {
            _normalHits -= 1;

            MainPlayer.GetComponent<PlayerManager>().OnTakenDamage(1); 

            foreach (var item in SpecialEffects_Pools)
            {
                if(!item.gameObject.activeInHierarchy)
                {
                    SpecialEffect = item.GetComponent<SpecialEffectsManager>();

                    if(SpecialEffect.SpecialEffectsCode == 2)
                    {
                        Vector3 _playerPos = MainPlayer.transform.position;

                        float _dice = Random.Range(-0.35f, 0.35f);

                        Vector3 Pos = new Vector3(_playerPos.x+_dice, _playerPos.y+_dice, _playerPos.z+_dice);

                        SpecialEffect.OnEndPlayingSpecialEffects(Pos);

                        break;
                    }
                }
            }

            StartCoroutine(HitManagement(_normalHits));
        }
        else
        {

        }

    }

    void BuffsManagement(int Quantity, bool IsOnSpecificLocations, bool isAdvantageBuff, bool isRandom)
    {
        if (IsOnSpecificLocations)
        {
            if (isAdvantageBuff)
            {
                int i = 0;
                foreach (var item in Buff_Pools)
                {
                    if (i >= Buffs_SpecificPosition.Count)
                    {
                        return;
                    }
                    else
                    {
                        if (isRandom)
                        {
                            if (!item.gameObject.activeInHierarchy)
                            {
                                if (Buffs_SpecificPosition[i].tag != "buffpt") return;

                                item.OnBeingPlaced(Buffs_SpecificPosition[i].position);

                                i++;
                            }
                        }
                        else
                        {
                            if (!item.gameObject.activeInHierarchy)
                            {
                                if (item.isAdvantageBuff)
                                {
                                    if (Buffs_SpecificPosition[i].tag != "buffpt") return;

                                    item.OnBeingPlaced(Buffs_SpecificPosition[i].position);

                                    i++;
                                }
                            }
                        }
                    }
                }
            }
            else // Disadvantage
            {
                int i = 0;
                foreach (var item in Buff_Pools)
                {
                    if (i >= Buffs_SpecificPosition.Count)
                    {
                        return;
                    }
                    else
                    {
                        if (isRandom)
                        {
                            if (!item.gameObject.activeInHierarchy)
                            {
                                if (Buffs_SpecificPosition[i].tag != "buffpt") return;

                                item.OnBeingPlaced(Buffs_SpecificPosition[i].position);

                                i++;
                            }
                        }
                        else
                        {
                            if (!item.gameObject.activeInHierarchy)
                            {
                                if (item.isDisadvantageBuff)
                                {
                                    if (Buffs_SpecificPosition[i].tag != "buffpt") return;

                                    item.OnBeingPlaced(Buffs_SpecificPosition[i].position);

                                    i++;
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < Quantity; i++)
            {
                float X, Z, RandomX, RandomZ;
                X = (Platform.transform.localScale.x / 2) - 0.5f;
                Z = (Platform.transform.localScale.z / 2) - 0.5f;

                RandomX = Random.Range(-X, X);
                RandomZ = Random.Range(-Z, Z);

                SingleTarget_Position = new Vector3(RandomX, 0.5f, RandomZ);

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

                    if(isRandom)
                    {
                        List<BuffsManager> _availableBuff = new List<BuffsManager>();
                        foreach (var item in Buff_Pools)
                        {
                            if (!item.gameObject.activeInHierarchy)
                                _availableBuff.Add(item);

                        }

                        BuffsManager bf = _availableBuff[Random.Range(0, _availableBuff.Count)];

                        bf.OnBeingPlaced(SingleTarget_Position);
                    }
                }
                else
                {
                    Debug.DrawLine(transform.position, SingleTarget_Position, Color.red, 5f);
                }
            }
        }
    }

    void TargetManagement(bool isRandom, int Quantity, bool isTheSameTimeSpawning, int Level, bool isBuff ,bool isSmall, bool isMedium, bool isLarge)
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

                SingleTarget_Position = new Vector3(RandomX, 1.5f, RandomZ);

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

                    AbilitiesManagement(Level,isSmall,isMedium,isLarge);
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
                SingleTarget_Position = MainPlayer.transform.position;

                AbilitiesManagement(Level, isSmall, isMedium, isLarge);
            }
        }
    }

    public void TargetOnSpots()
    {
        foreach (var item in Buffs_SpecificPosition)
        {
            if(item.CompareTag("aoept"))
            {
                TargetOnSpecificSpotsPattern(item.position, 3);

                Debug.Log(item.name);
            }
        }
    }

    public void TargetOnSpecificSpotsPattern(Vector3 _spot, int Level)
    {
        foreach (AoeManager item in Aoes_Pools)
        {
            if (item.Level == Level && !item.gameObject.activeInHierarchy)
            {
                AoeManager Aoe = item;

                Aoe.OnBeingCast(_spot);

                return;
            }
            else
            {

            }
        }
    }

    void AbilitiesManagement(int Level, bool isSmall, bool isMedium, bool isLarge)
    {
        AoeManager Aoe;
        //Debug.Log(Level);

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

    #endregion

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

    public void OnMountingTurret()
    {
        Button_Dismount.gameObject.SetActive(true);
        
        Button_Dismount.onClick.AddListener(OnDismountTurret);
    }

    public void OnDismountTurret()
    {
        PlayerManager _player = MainPlayer.GetComponent<PlayerManager>();

        _player.DismountTurret();

        Button_Dismount.gameObject.SetActive(false);
    }

    public void Game_RoundsEnd(bool IsaWin)
    {
        if (IsaWin)
        {
            if(!isGameEnd)
            {
                BossConvoManager.singleton.OnSendingEndGamePhrase();
                UI_GameRoundEnd.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Victory LOL!";
                isGameEnd = true;
                Game_GlobalInfo.singleton.OnDefeatedLevel(CurrentGameLevel);
                if(BossPhaseManager.singleton!=null)
                    BossPhaseManager.singleton.StopAllCoroutines();
            }
        }
        else
        {
            UI_GameRoundEnd.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Victory-NOT TODAY!";

            if (BossPhaseManager.singleton != null)
                BossPhaseManager.singleton.StopAllCoroutines();
        }
        UI_GameRoundEnd.gameObject.SetActive(true);

        UI_GameRoundEnd.ShowButton("Button Main Menu");

        GameEnd_DisableItems();
    }

    public void GameEnd_DisableItems()
    {
        for (int i = 0; i < BehaviourScripts_toDisable.Count; i++)
        {
            if(BehaviourScripts_toDisable[i] == null)
            {
                
            }
            else
            {
                BehaviourScripts_toDisable[i].enabled = false;
            }
        }
    }
}
