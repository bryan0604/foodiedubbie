using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager singleton;
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

    private void Start()
    {
        singleton = this;

        DefaultHealthPoints = HealthPoints;
    }

    private void Update()
    {
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
}