using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBarrier : MonoBehaviour
{
    public BossManager_Level2 BossManager;
    public BoxCollider myCol;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            BossManager.OnTakingDamage(0);

            myCol.enabled = false;
        }
    }


}
