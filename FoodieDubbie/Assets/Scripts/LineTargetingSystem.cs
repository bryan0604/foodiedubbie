using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargetingSystem : MonoBehaviour
{
    public Transform TargetedPlayer;
    public Transform PlayerObject;
    public Transform AoeLine;
    public List<Transform> SpecialEffectPoints = new List<Transform>();
    public List<Transform> SpecialEffects_Explosion = new List<Transform>();
    //public List<LineTargetingSystem> SameBehaviour = new List<LineTargetingSystem>();
    public float ExplodeEffectTiming=0.2f;
    public float ChannelTiming=3f;
    public int Damage = 65;
    public bool TargetAtPlayerDirection = true;
    private float _endSEtiming;
    private int _amountOfExplodesSE;


    private void Start()
    {
        _endSEtiming = SpecialEffects_Explosion[0].GetComponent<ParticleSystem>().main.startLifetimeMultiplier;    
    }

    public void CastLineAoe()
    {
        if (TargetAtPlayerDirection)
        {
            transform.LookAt(PlayerObject.position);
        }

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

        if(TargetedPlayer)
        {
            TargetedPlayer.GetComponent<PlayerManager>().OnTakenDamage(Damage);
        }

        AoeLine.gameObject.SetActive(false);

        StartCoroutine(SpecialEffectsExplode());
    }

    IEnumerator SpecialEffectsExplode()
    {
        yield return new WaitForSeconds(ExplodeEffectTiming);

        if(_amountOfExplodesSE >= SpecialEffectPoints.Count)
        {
            _amountOfExplodesSE = 0;

            Invoke("EndSpecialEffects", 3f);   
        }
        else
        {
            //Debug.Log("Exploding at " + SpecialEffectPoints[_amountOfExplodesSE], gameObject);

            SpecialEffects_Explosion[_amountOfExplodesSE].gameObject.SetActive(true);

            SpecialEffects_Explosion[_amountOfExplodesSE].transform.position = SpecialEffectPoints[_amountOfExplodesSE].transform.position;

            _amountOfExplodesSE++;

            StartCoroutine(SpecialEffectsExplode());
        }
    }

    void EndSpecialEffects()
    {
        for (int i = 0; i < SpecialEffects_Explosion.Count; i++)
        {
            SpecialEffects_Explosion[i].gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TargetedPlayer = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TargetedPlayer = null;
        }
    }
}
