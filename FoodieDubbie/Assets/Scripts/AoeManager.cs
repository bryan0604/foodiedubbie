using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeManager : MonoBehaviour
{
    public bool isBeingCast;
    public int Damage;

    public void OnBeingCast(Vector3 pos)
    {
        isBeingCast = true;

        transform.position = new Vector3(pos.x, pos.y + 0.5f, pos.z);

        gameObject.SetActive(true);

        StartCoroutine(CastAoe());
    }

    public IEnumerator CastAoe()
    {
        yield return new WaitForSeconds(5f);

        isBeingCast = false;
        float radius = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3f;
        Collider[] Objects = Physics.OverlapSphere(transform.position, radius);
        foreach (var item in Objects)
        {
            if(item.gameObject.tag == "Player")
            {
                Debug.Log("Hit!");

                PlayerManager Player = item.GetComponent<PlayerManager>();

                Player.OnTakenDamage(Damage);
            }
        }

        SpecialEffectsManager _se;

        foreach (var item in GameManager.singleton.SpecialEffects_Pools)
        {
            if(!item.gameObject.activeInHierarchy)
            {
                _se = item.GetComponent<SpecialEffectsManager>();

                _se.OnEndPlayingSpecialEffects(transform.position);

                break;
            }
        }

        GameManager.singleton.OnExplode(gameObject);
    }
}
