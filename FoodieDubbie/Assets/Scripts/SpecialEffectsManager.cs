using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    public ParticleSystem selfSE;

    public float LifeTime;

    public void OnEndPlayingSpecialEffects(Vector3 targetPos)
    {
        selfSE.Stop();

        selfSE.transform.position = targetPos;

        selfSE.gameObject.SetActive(true);

        selfSE.Play();

        LifeTime = selfSE.main.startLifetimeMultiplier;

        StartCoroutine(SendBackToGameManager(LifeTime));
    }

    IEnumerator SendBackToGameManager(float time)
    {
        yield return new WaitForSeconds(time);

        GameManager.singleton.OnSpecialEffectsEnded(selfSE);
    }
}
