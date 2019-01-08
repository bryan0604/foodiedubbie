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

    public void OnBeingCast(Vector3 pos)
    {
        isBeingCast = true;

        transform.position = new Vector3(pos.x, pos.y + 0.2f, pos.z);

        gameObject.SetActive(true);

        StartCoroutine(CastAoe());
    }

    public IEnumerator CastAoe()
    {
        yield return new WaitForSeconds(ExplodeTiming);

        isBeingCast = false;
        float radius = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3f;
        Collider[] Objects = Physics.OverlapSphere(transform.position, radius);
        foreach (var item in Objects)
        {
            if(item.gameObject.tag == "Player" && !GameManager.singleton.isGameEnd)
            {
                Debug.Log("Hit!");

                PlayerManager Player = item.GetComponent<PlayerManager>();

                Player.OnTakenDamage(Damage);
            }
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

        GameManager.singleton.OnExplode(gameObject);
    }
}
