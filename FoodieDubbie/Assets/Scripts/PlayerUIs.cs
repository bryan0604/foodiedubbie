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
    private bool isRecovers;

    private void LateUpdate()
    {
        OnLerpingToPlayerPosition();
    }

    private void Update()
    {
        if(HealthBarMoves)
        {
            if (Healthbar_Background.transform.localScale.x == Axe)
            {
                HealthBarMoves = false;
            }
            else if(Healthbar_Background.transform.localScale.x >= Axe)
            {
                float X = Healthbar_Background.localScale.x;

                X -= Time.deltaTime * (LerpSpeed / 2f);

                Healthbar_Background.transform.localScale = new Vector3(X, Healthbar_Background.localScale.y, Healthbar_Background.localScale.z);
            }
            else
            {
                float X = Healthbar_Background.localScale.x;

                X += Time.deltaTime * (LerpSpeed / 2f);

                Healthbar_Background.transform.localScale = new Vector3(X, Healthbar_Background.localScale.y, Healthbar_Background.localScale.z);
            }

            //if(isRecovers)
            //{
            //    float X = Healthbar_Background.localScale.x;

            //    X += Time.deltaTime * (LerpSpeed / 2f);

            //    Healthbar_Background.transform.localScale = new Vector3(X, Healthbar_Background.localScale.y, Healthbar_Background.localScale.z);

            //    if (Healthbar_Background.transform.localScale.x >= Axe)
            //    {
            //        HealthBarMoves = false;
            //    }
            //}
            //else
            //{
            //    float X = Healthbar_Background.localScale.x;

            //    X -= Time.deltaTime * (LerpSpeed / 2f);

            //    Healthbar_Background.transform.localScale = new Vector3(X, Healthbar_Background.localScale.y, Healthbar_Background.localScale.z);

            //    if (Healthbar_Background.transform.localScale.x <= Axe)
            //    {
            //        HealthBarMoves = false;
            //    }
            //}
        }
    }

    public void OnHealthPointsChanged(int Current, int Max, bool isRecovering)
    {
        isRecovers = isRecovering;

        float CurrentValue = Current;
        float MaximumValue = Max;
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
