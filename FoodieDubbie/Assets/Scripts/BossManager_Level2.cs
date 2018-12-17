using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager_Level2 : MonoBehaviour
{
    [Header("Version 1.5.0 - 17122018")]
    public static BossManager_Level2 singleton;
    public BossPhaseManager BossPhaseManaging;
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
    public bool isLatestUpdated;
    private int DefaultHealthPoints;
    private float Axe;
    private int _BuffCycles;
 
    private void OnValidate()
    {
        if (isLatestUpdated) 
        {
            isLatestUpdated = false;
            return;
        }
        singleton = this;
    }

    public void OnUpdateData()
    {
        Health_ThresholdCheck.Clear();

        for (int i = 0; i < Health_ThresholdPercent.Count; i++)
        {
            Health_ThresholdCheck.Add((HealthPoints / 100) * Health_ThresholdPercent[i]); ;
        }

        isLatestUpdated = true;

        BossPhaseManaging.AddPhase(Health_ThresholdCheck.Count);
    }

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

        //OnHealthPointsChanged();

        CheckThreshold();
    }

    #region HealthBar UI
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
    #endregion

    #region Threshold Design
    void CheckThreshold()
    {
        Debug.Log("=====Checking Threshold=====");
        for (int i = 0; i < Health_ThresholdCheck.Count; i++)
        {
            if (HealthPoints <= Health_ThresholdCheck[i])
            {
                if (i > CurrentPhase)
                {
                    CurrentPhase = i;

                    Debug.Log("=====Changing Phase=====");

                    BossPhaseManager.singleton.CurrentPhaseMain ++;
                    BossPhaseManager.singleton.CurrentPhaseisAt = 0;

                    if (CurrentPhase == 0)
                    {
                        
                    }
                    else if (CurrentPhase == 1)
                    {
                        
                    }
                    else if (CurrentPhase == 2)
                    {
                        
                    }
                    else if(CurrentPhase==3)
                    {
                        
                    }
                }
            }
        }
    }
    #endregion

    #region Boss Fight Pattern - Design 

    #endregion
}