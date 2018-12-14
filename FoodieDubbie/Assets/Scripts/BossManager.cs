using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager singleton;
    public List<int> Health_ThresholdPercent = new List<int>();
    public List<int> Health_ThresholdCheck = new List<int>();
    [Header("Boss 1 Phase Timings")]
    public List<float> SkillsTiming_B1P1 = new List<float>();
    [Header("Boss 1 Phase boolean")]
    public List<bool> PhaseBool = new List<bool>();
    [Space]
    [Header("Boss 1 Phase Cycles to drop buffs")]
    public List<int> Cycles = new List<int>();
    public int Phase;
    public int BossLevel;
    [Tooltip("Cast Every Sec")]
    public float _aoeSmall=5f;
    public float _aoeMedium;
    public float LerpSpeed;
    public int CastAmount_Aoe = 5;
    public int CastAmount_Buffs = 4;
    public int HealthPoints = 1000;
    public Transform Healthbar_Original;
    public Transform Healthbar_Background;
    public bool isHealthbarMoving;
    private int DefaultHealthPoints;
    private float Axe;
    private int _currentCycles;
    private int _currentPhase;

    private void Start()
    {
        singleton = this;

        DefaultHealthPoints = HealthPoints;

        for (int i = 0; i < Health_ThresholdPercent.Count; i++)
        {
            Health_ThresholdCheck.Add((HealthPoints / 100) * Health_ThresholdPercent[i]); ;
        }

        BasicPhase();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnTakingDamage(75);
        }

        if(isHealthbarMoving)
        {
            float X = Healthbar_Background.localScale.x;

            X -= Time.deltaTime * (LerpSpeed / 2f);

            Healthbar_Background.transform.localScale = new Vector3(X, Healthbar_Background.localScale.y, Healthbar_Background.localScale.z);

            if(Healthbar_Background.transform.localScale.x <= Axe)
            {
                isHealthbarMoving = false;
            }
        }
    }

    public void OnTakingDamage(int Amount)
    {
        HealthPoints -= Amount;

        OnHealthPointsChanged();

        CheckThreshold();
    }

    void OnHealthPointsChanged()
    {
        float CurrentValue = HealthPoints;
        float MaximumValue = DefaultHealthPoints;
        float value;

        value = CurrentValue / MaximumValue;

        Healthbar_Original.transform.localScale = new Vector3(Mathf.Clamp(value, 0f, 1f), Healthbar_Original.transform.localScale.y, Healthbar_Original.transform.localScale.z);

        Axe = Healthbar_Original.transform.localScale.x;

        isHealthbarMoving = true;
    }

    #region Threshold Design
    void CheckThreshold()
    {
        for (int i = 0; i < Health_ThresholdCheck.Count; i++)
        {
            if(HealthPoints <= Health_ThresholdCheck[i])
            {
                if(BossLevel == 1)
                {
                    if(_currentPhase==i+1)
                    {
                        
                    }
                    else
                    {
                    
                        Debug.Log(i + 1);

                        _currentPhase = i + 1;
                    }
                    //PhaseManagement(i + 1);
                }
            }
        }
    }
    #endregion

    #region Boss Fight Pattern - Design Level 1
    void PhaseManagement(int _phase)
    {
        if(Phase!=_phase)
        {
            Phase = _phase;
            if (Phase == 1)
            {
                PhaseOne();
            }
            else if(Phase == 2)
            {
                PhaseTwo();
            }
            else
            {
                PhaseThree();
            }
        }
    }

    void BasicPhase()
    {
        StartCoroutine(Boss_1_Phase_0_Skill_1());
    }

    IEnumerator Boss_1_Phase_0_Skill_1()
    {
        yield return new WaitForSeconds(SkillsTiming_B1P1[0]);
        
        if(Phase != 0)
        {
            Debug.Log("Cancel");
        }

        GameManager.singleton.SkillThree_SingleTarget();

        CyclesManagement_B1P0();

        StartCoroutine(Boss_1_Phase_0_Skill_2());
    }

    IEnumerator Boss_1_Phase_0_Skill_2()
    {
        yield return new WaitForSeconds(SkillsTiming_B1P1[1]);

        if (Phase != 0)
        {
            Debug.Log("Cancel");
        }
        else
        {
            GameManager.singleton.NormalAttack();

            CyclesManagement_B1P0();

            StartCoroutine(Boss_1_Phase_0_Skill_1());
        }
    }

    void CyclesManagement_B1P0()
    {
        if(_currentCycles >= Cycles[0])
        {
            _currentCycles = 0;

            GameManager.singleton.DropAdvantageBuff();
        }
        else
        {
            _currentCycles++;
        }
    }

    void PhaseOne()
    {
        Debug.Log("Activating Phase One!");


    }

    void PhaseTwo()
    {
        Debug.Log("Phase Two!");
    }

    void PhaseThree()
    {
        Debug.Log("Phase Three!");
    }

    #endregion
}