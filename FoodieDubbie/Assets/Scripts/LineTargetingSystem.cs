using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargetingSystem : MonoBehaviour
{
    public Transform PlayerObject;
    public Transform AoeLine;
    public List<Transform> SpecialEffectPoints = new List<Transform>();

    public float ExplodeEffectTiming=0.2f;
    public float ChannelTiming=3f;

    private int _amountOfExplodesSE;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(AoeLine.gameObject.activeInHierarchy)
            {
                return;
            }

            CastLineAoe();
        }
    }

    public void CastLineAoe()
    {
        transform.LookAt(PlayerObject.position);

        StartCoroutine(Channeling());
    }

    IEnumerator Channeling()
    {
        yield return new WaitForSeconds(0.5f);

        AoeLine.gameObject.SetActive(true);

        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(ChannelTiming); // Explode

        AoeLine.gameObject.SetActive(false);

        StartCoroutine(SpecialEffectsExplode());
    }

    IEnumerator SpecialEffectsExplode()
    {
        yield return new WaitForSeconds(ExplodeEffectTiming);

        if(_amountOfExplodesSE >= SpecialEffectPoints.Count)
        {
            _amountOfExplodesSE = 0;
        }
        else
        {
            Debug.Log("Exploding at " + SpecialEffectPoints[_amountOfExplodesSE], gameObject);

            _amountOfExplodesSE++;

            StartCoroutine(SpecialEffectsExplode());
        }
    }
}
