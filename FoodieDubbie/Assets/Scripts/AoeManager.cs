using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeManager : MonoBehaviour
{
    public bool isSmall;
    public bool isBeingCast;
    public int Damage;
    public int Level;
    public float ExplodeTiming = 5f;
    public PlayerManager PlayerInArea;

    public void OnBeingCast(Vector3 pos)
    {
        isBeingCast = true;

        //transform.position = new Vector3(pos.x, 0.2f, pos.z);

        gameObject.SetActive(true);

        transform.parent.position = new Vector3(pos.x, 0.5f, pos.z);

        StartCoroutine(CastAoe());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInArea = other.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInArea = null;
        }
    }

    public IEnumerator CastAoe()
    {
        yield return new WaitForSeconds(ExplodeTiming);

        isBeingCast = false;

        if(PlayerInArea && !GameManager.singleton.isGameEnd)
        {
            Debug.Log("Hit!");

            PlayerInArea.OnTakenDamage(Damage);

            PlayerInArea = null;
        }
        

        #region SE
        SpecialEffectsManager _se;

        foreach (var item in GameManager.singleton.SpecialEffects_Pools)
        {
            if(!item.gameObject.activeInHierarchy)
            {
                _se = item.GetComponent<SpecialEffectsManager>();

                if(isSmall)
                {
                    if (_se.SpecialEffectsCode == 1)
                    {
                        _se.OnEndPlayingSpecialEffects(transform.position);

                        break;
                    }
                }
                else
                {
                    if (_se.SpecialEffectsCode == 6)
                    {
                        _se.OnEndPlayingSpecialEffects(transform.position);

                        break;
                    }
                }
          
            }
        }

        #endregion

        GameManager.singleton.OnExplode(transform.parent.gameObject);
    }
}
