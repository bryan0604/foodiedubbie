using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager singleton;
    public List<int> Health_ThresholdPercent = new List<int>();
    public List<int> Health_ThresholdCheck = new List<int>();
    [Space]
    [Header("Current Phase")]
    public int CurrentPhase = -1;
    public int TotalPhase;
    public int BuffCycles=4;
    [Tooltip("Cast Every Sec")]
    public float _aoeSmall = 5f;
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
    private int _BuffCycles;
    private int _currentPhase;

    private void Start()
    {
        singleton = this;

        DefaultHealthPoints = HealthPoints;

        _currentPhase = CurrentPhase;
        _BuffCycles = BuffCycles;

        TotalPhase = Health_ThresholdPercent.Count-1;

        for (int i = 0; i < Health_ThresholdPercent.Count; i++)
        {
            Health_ThresholdCheck.Add((HealthPoints / 100) * Health_ThresholdPercent[i]); ;
        }

        BasicPhase();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnTakingDamage(75);
        }

        if (isHealthbarMoving)
        {
            float X = Healthbar_Background.localScale.x;

            X -= Time.deltaTime * (LerpSpeed / 2f);

            Healthbar_Background.transform.localScale = new Vector3(X, Healthbar_Background.localScale.y, Healthbar_Background.localScale.z);

            if (Healthbar_Background.transform.localScale.x <= Axe)
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
            if (HealthPoints <= Health_ThresholdCheck[i])
            {
                if (i > CurrentPhase)
                {
                    CurrentPhase = i;

                    if (CurrentPhase == 0)
                    {
                        PhaseZero();
                    }
                    else if (CurrentPhase == 1)
                    {

                    }
                    else if (CurrentPhase == 2)
                    {

                    }
                }
                //Debug.Log("Standard - " +i);
            }
        }
    }
    #endregion

    #region Boss Fight Pattern - Design 

    //Normal Attack > Single Target x 4 Buffs(7 only)
    #region Basic Phase
    void BasicPhase()
    {
        Debug.Log("Commencing Basic Phase");

        StartCoroutine(BasicPhase_S1());
    }

    IEnumerator BasicPhase_S1()
    {
        yield return new WaitForSeconds(2f);

        if (CurrentPhase != -1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[0].Invoke();

            BuffsCycles_BasicPhase();

            StartCoroutine(BasicPhase_S2());
        }     
    }

    IEnumerator BasicPhase_S2()
    {
        yield return new WaitForSeconds(8f);

        if (CurrentPhase != -1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[3].Invoke();

            BuffsCycles_BasicPhase();

            StartCoroutine(BasicPhase_S1());
        }
    }

    void BuffsCycles_BasicPhase()
    {
        if (_BuffCycles <= 0)
        {
            GameManager.singleton.BossAbilities[7].Invoke();

            _BuffCycles = BuffCycles;
        }
        else
        {
            _BuffCycles -= 1;
        }
    }

    #endregion

    //Single Random > Multi Random x 6 Buffs(6,7) 
    #region Phase Zero
    void PhaseZero()
    {
        Debug.Log("Commencing Phase Zero");
        BuffCycles = 6;
        _BuffCycles = BuffCycles;

        StartCoroutine(PhaseZero_S1());
    }

    IEnumerator PhaseZero_S1()
    {
        yield return new WaitForSeconds(5f);

        if (CurrentPhase != 0)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[1].Invoke();

            BuffsCycles_PhaseZero();

            StartCoroutine(PhaseZero_S2());
        }
    }
    IEnumerator PhaseZero_S2()
    {
        yield return new WaitForSeconds(8f);

        if (CurrentPhase != 0)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[2].Invoke();

            BuffsCycles_PhaseZero();

            StartCoroutine(PhaseZero_S1());
        }
    }

    void BuffsCycles_PhaseZero()
    {
        if (_BuffCycles == 0)
        {
            GameManager.singleton.BossAbilities[6].Invoke();

            _BuffCycles = BuffCycles;
        }
        else if(_BuffCycles == 3)
        {
            GameManager.singleton.BossAbilities[7].Invoke();

            _BuffCycles -= 1;
        }
        else
        {
            _BuffCycles -= 1;
        }
    }
    #endregion

    #region Phase One

    #endregion

    #endregion
}