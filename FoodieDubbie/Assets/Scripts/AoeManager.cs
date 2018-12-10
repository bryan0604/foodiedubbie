using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeManager : MonoBehaviour
{
    public bool isBeingCast;

    public void OnBeingCast(Vector3 pos)
    {
        isBeingCast = true;

        transform.position = pos;

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
            }
        }

        GameManager.singleton.OnExplode(gameObject);
    }
}
