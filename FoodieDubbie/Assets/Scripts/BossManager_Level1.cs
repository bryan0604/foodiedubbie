using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager_Level1 : MonoBehaviour
{
    public static BossManager_Level1 singleton;
    public List<int> Health_ThresholdPercent = new List<int>();
    public List<int> Health_ThresholdCheck = new List<int>();
    [Space]
    [Header("Current Phase")]
    public int CurrentPhase = -1;
    public int TotalPhase;
    public int BuffCycles=4;
    [Tooltip("Cast Every Sec")]
    public float _aoeSmall = 5f;
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

    private void Start()
    {
        singleton = this;

        DefaultHealthPoints = HealthPoints;

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

        if(HealthPoints <= 0)
        {
            GameManager.singleton.Game_RoundsEnd(true);
            StopAllCoroutines();

            return;
        }

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
                        PhaseOne();
                    }
                    else if (CurrentPhase == 2)
                    {
                        PhaseTwo();
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
        //Debug.Log("Commencing Basic Phase");

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

    //Single Random > Single Target > Multi Random > Single Target > x 6 Buffs(6,7) 
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
        yield return new WaitForSeconds(5f);

        if (CurrentPhase != 0)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[3].Invoke();

            BuffsCycles_PhaseZero();

            StartCoroutine(PhaseZero_S3());
        }
    }

    IEnumerator PhaseZero_S3()
    {
        yield return new WaitForSeconds(5f);

        if (CurrentPhase != 0)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[2].Invoke();

            BuffsCycles_PhaseZero();

            StartCoroutine(PhaseZero_S4());
        }
    }

    IEnumerator PhaseZero_S4()
    {
        yield return new WaitForSeconds(5f);

        if (CurrentPhase != 0)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[3].Invoke();

            BuffsCycles_PhaseZero();

            StartCoroutine(PhaseZero_S1());
        }
    }

    void BuffsCycles_PhaseZero()
    {
        if (_BuffCycles == 1)
        {
            GameManager.singleton.BossAbilities[6].Invoke();

            _BuffCycles -= 1;
        }
        else if(_BuffCycles == 3)
        {
            GameManager.singleton.BossAbilities[7].Invoke();

            _BuffCycles -= 1;
        }
        else if(_BuffCycles == 0)
        {
            _BuffCycles = BuffCycles;
        }
        else
        {
            _BuffCycles -= 1;
        }
    }
    #endregion

    //3,0,2,2,0,4,0
    //Single Target > 2s > Normal Attack > 2s > Multi Random > 0.5s
    //Multi Random > 2s > Normal Attack > 2s > Multi Target > 2s > Normal Attack > 2s
    //4 buffs in 8 cycles(5,5,7,6) @ 2,4,6,8
    #region Phase One
    void PhaseOne()
    {
        Debug.Log("Commencing Phase One");
        BuffCycles = 8;
        _BuffCycles = BuffCycles;

        StartCoroutine(PhaseOne_S1());
    }

    IEnumerator PhaseOne_S1()
    {
        yield return new WaitForSeconds(2f);

        if (CurrentPhase != 1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[3].Invoke();

            BuffsCycles_PhaseOne();

            StartCoroutine(PhaseOne_S2());
        }
    }
    IEnumerator PhaseOne_S2()
    {
        yield return new WaitForSeconds(2f);

        if (CurrentPhase != 1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[0].Invoke();

            BuffsCycles_PhaseOne();

            StartCoroutine(PhaseOne_S3());
        }
    }

    IEnumerator PhaseOne_S3()
    {
        yield return new WaitForSeconds(2f);

        if (CurrentPhase != 1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[2].Invoke();

            BuffsCycles_PhaseOne();

            StartCoroutine(PhaseOne_S4());
        }
    }

    IEnumerator PhaseOne_S4()
    {
        yield return new WaitForSeconds(0.5f);

        if (CurrentPhase != 1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[2].Invoke();

            StartCoroutine(PhaseOne_S5());
        }
    }

    IEnumerator PhaseOne_S5()
    {
        yield return new WaitForSeconds(2f);

        if (CurrentPhase != 1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[0].Invoke();

            StartCoroutine(PhaseOne_S6());
        }
    }

    IEnumerator PhaseOne_S6()
    {
        yield return new WaitForSeconds(2f);

        if (CurrentPhase != 1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[4].Invoke();

            BuffsCycles_PhaseOne();

            StartCoroutine(PhaseOne_S7());
        }
    }

    IEnumerator PhaseOne_S7()
    {
        yield return new WaitForSeconds(2f);

        if (CurrentPhase != 1)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[0].Invoke();

            BuffsCycles_PhaseOne();

            StartCoroutine(PhaseOne_S1());
        }
    }

    void BuffsCycles_PhaseOne()
    {
        if (_BuffCycles == 2)
        {
            GameManager.singleton.BossAbilities[5].Invoke();

            _BuffCycles -= 1;
        }
        else if (_BuffCycles == 4)
        {
            GameManager.singleton.BossAbilities[5].Invoke();

            _BuffCycles -= 1;
        }
        else if (_BuffCycles == 5)
        {
            GameManager.singleton.BossAbilities[7].Invoke();

            _BuffCycles -= 1;
        }
        else if (_BuffCycles == 8)
        {
            GameManager.singleton.BossAbilities[6].Invoke();

            _BuffCycles -= 1;
        }
        else if(_BuffCycles == 0)
        {
            _BuffCycles = BuffCycles;
        }
        else
        {
            _BuffCycles -= 1;
        }
    }
    #endregion

    // 3,3,0,0 
    // Single Target > 0.5 > Single Target > 0.5 > Normal Attack > 0.5 > Normal Attack
    // 1 buffs in 4 cycles, 5 
    #region Phase Two(last)
    void PhaseTwo()
    {
        Debug.Log("Commencing Phase Two");
        BuffCycles = 4;
        _BuffCycles = BuffCycles;

        StartCoroutine(PhaseTwo_S1());
    }

    IEnumerator PhaseTwo_S1()
    {
        yield return new WaitForSeconds(0.5f);

        if (CurrentPhase != 2)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[3].Invoke();

            //BuffsCycles_PhaseTwo();

            StartCoroutine(PhaseTwo_S2());
        }
    }

    IEnumerator PhaseTwo_S2()
    {
        yield return new WaitForSeconds(0.5f);

        if (CurrentPhase != 2)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[3].Invoke();

            BuffsCycles_PhaseTwo();

            StartCoroutine(PhaseTwo_S3());
        }
    }

    IEnumerator PhaseTwo_S3()
    {
        yield return new WaitForSeconds(0.5f);

        if (CurrentPhase != 2)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[0].Invoke();

            //BuffsCycles_PhaseTwo();

            StartCoroutine(PhaseTwo_S4());
        }
    }

    IEnumerator PhaseTwo_S4()
    {
        yield return new WaitForSeconds(0.5f);

        if (CurrentPhase != 2)
        {

        }
        else
        {
            GameManager.singleton.BossAbilities[0].Invoke();

            BuffsCycles_PhaseTwo();

            StartCoroutine(PhaseTwo_S1());
        }
    }

    void BuffsCycles_PhaseTwo()
    {
        if (_BuffCycles == 4)
        {
            GameManager.singleton.BossAbilities[5].Invoke();

            _BuffCycles -= 1;
        }
        else if (_BuffCycles == 0)
        {
            _BuffCycles = BuffCycles;
        }
        else
        {
            _BuffCycles -= 1;
        }
    }
        #endregion

        #endregion
}