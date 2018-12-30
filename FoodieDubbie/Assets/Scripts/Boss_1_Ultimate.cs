using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Ultimate : MonoBehaviour
{
    public float ZapTiming=1f;
    public float RotationSpeed = 3f;
    public float PlatformRotationSpeed = 4f;
    public int ZapDamage=2;
    public Transform Platforms_1;
    public Transform Platforms_2;
    public Transform Platforms_3;
    public Transform Platforms_4;
    public Transform Player;
    public bool isActivate;
    public Material PlatformMaterial;
    private PlayerManager _player;

    void ElectricShock()
    {
        if(Player)
        {
            //Debug.Log("Zap player = " + Player);

            _player.OnTakenDamage(ZapDamage);
            
        }
        else
        {
            //Debug.Log("Zap nothing.");
        }
    }

    void FixedUpdate()
    {
        if(isActivate)
        {
            transform.Rotate(Vector3.up, PlatformRotationSpeed * Time.deltaTime, Space.Self);
            Platforms_1.rotation *= Quaternion.Euler(-Vector3.up * RotationSpeed * RotationSpeed * Time.deltaTime);
            Platforms_2.rotation *= Quaternion.Euler(-Vector3.up * RotationSpeed * RotationSpeed * Time.deltaTime);
            Platforms_3.rotation *= Quaternion.Euler(-Vector3.up * RotationSpeed * RotationSpeed * Time.deltaTime);
            Platforms_4.rotation *= Quaternion.Euler(-Vector3.up * RotationSpeed * RotationSpeed * Time.deltaTime);

        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player = other.transform;

            _player = Player.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Player == other.transform)
            {
                Player = null;

                _player = null;
            }
        }
    }

    public void OnActivate()
    {
        if (isActivate) return;

        for (int i = 0; i < 4; i++)
        {
            foreach (var item in GameManager.singleton.SpecialEffects_Pools)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    SpecialEffectsManager se = item.GetComponent<SpecialEffectsManager>();

                    if (se.SpecialEffectsCode == 4)
                    {
                        if (i == 0)
                        {
                            se.OnEndPlayingSpecialEffects(Platforms_1.position);
                        }
                        else if (i == 1)
                        {
                            se.OnEndPlayingSpecialEffects(Platforms_2.position);
                        }
                        else if (i == 2)
                        {
                            se.OnEndPlayingSpecialEffects(Platforms_3.position);
                        }
                        else if (i == 3)
                        {
                            se.OnEndPlayingSpecialEffects(Platforms_4.position);
                        }

                        break;
                    }
                }
            }
        }

        gameObject.SetActive(true);

        StartCoroutine(StartCasting());
    }

    IEnumerator StartCasting()
    {
        yield return new WaitForSeconds(5f);

        Platforms_1.GetComponent<MeshRenderer>().enabled = true;
        Platforms_2.GetComponent<MeshRenderer>().enabled = true;
        Platforms_3.GetComponent<MeshRenderer>().enabled = true;
        Platforms_4.GetComponent<MeshRenderer>().enabled = true;

        isActivate = true;
        CancelInvoke("ElectricShock");
        InvokeRepeating("ElectricShock", ZapTiming, ZapTiming);

        StartCoroutine(EndCasting());
    }

    IEnumerator EndCasting()
    {
        yield return new WaitForSeconds(15f);

        isActivate = false;
    }
}
