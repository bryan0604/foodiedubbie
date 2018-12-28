using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Ultimate : MonoBehaviour
{
    public float ZapTiming=1f;
    public float RotationSpeed = 3f;
    public float PlatformRotationSpeed = 4f;
    public Transform Platforms_1;
    public Transform Platforms_2;
    public Transform Platforms_3;
    public Transform Platforms_4;
    public Transform Player;
    public bool isActivate;

    void ElectricShock()
    {
        if(Player)
        {
            Debug.Log("Zap player = " + Player);
        }
        else
        {
            Debug.Log("Zap nothing.");
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Player == other.transform)
            {
                Player = null;
            }
        }
    }

    public void OnActivate()
    {
        if (isActivate) return;

        gameObject.SetActive(true);

        StartCoroutine(StartCasting());
    }

    IEnumerator StartCasting()
    {
        yield return new WaitForSeconds(5f);

        isActivate = true;

        InvokeRepeating("ElectricShock", ZapTiming, ZapTiming);

        StartCoroutine(EndCasting());
    }

    IEnumerator EndCasting()
    {
        yield return new WaitForSeconds(15f);

        isActivate = false;
    }
}
