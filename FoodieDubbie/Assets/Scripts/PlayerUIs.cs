using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIs : MonoBehaviour
{
    public bool HealthBarMoves;
    public int DefaultHealthPoints;
    public int HealthPoints = 100;
    public float LerpSpeed = 0.5f;
    public Transform TargetPlayer;
    public Transform Healthbar_Original;
    public Transform Healthbar_Background;
    public Text PlayerName_Text;
    private float Axe; 

    private void Start()
    {
        DefaultHealthPoints = HealthPoints;
    }
    private void LateUpdate()
    {
        OnLerpingToPlayerPosition();
    }

    private void Update()
    {
        if(HealthBarMoves)
        {
            float X = Healthbar_Background.localScale.x;

            X -= Time.deltaTime * (LerpSpeed/2f);

            Healthbar_Background.transform.localScale = new Vector3(X, Healthbar_Background.localScale.y, Healthbar_Background.localScale.z); 

            if(Healthbar_Background.transform.localScale.x <= Axe )
            {
                HealthBarMoves = false;
            }
        }
    }

    public void OnHealthPointsChanged()
    {
        float CurrentValue = HealthPoints;
        float MaximumValue = DefaultHealthPoints;
        float value;

        value = CurrentValue / MaximumValue;

        Healthbar_Original.transform.localScale = new Vector3(Mathf.Clamp(value, 0f, 1f), Healthbar_Original.transform.localScale.y, Healthbar_Original.transform.localScale.z);

        HealthBarMoves = true;

        Axe = Healthbar_Original.transform.localScale.x;
    }

    void OnLerpingToPlayerPosition()
    {
        if (TargetPlayer != null)
        {
            Vector3 smoothedPos = Vector3.Lerp(transform.position, TargetPlayer.transform.position, LerpSpeed);

            transform.position = smoothedPos;
        }
        else
        {
            Debug.LogWarning("TargetPlayer is null");
        }
    }
}
