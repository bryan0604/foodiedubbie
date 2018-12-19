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

    private void Start()
    {
        InvokeRepeating("ElectricShock",ZapTiming,ZapTiming);
    }

    void ElectricShock()
    {
        if(Player)
        {
            Debug.Log("Zap player = " + Player);
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, PlatformRotationSpeed * Time.deltaTime, Space.Self);
        Platforms_1.rotation *= Quaternion.Euler( -Vector3.up *RotationSpeed * RotationSpeed * Time.deltaTime);
        Platforms_2.rotation *= Quaternion.Euler( -Vector3.up *RotationSpeed * RotationSpeed * Time.deltaTime);
        Platforms_3.rotation *= Quaternion.Euler( -Vector3.up *RotationSpeed * RotationSpeed * Time.deltaTime);
        Platforms_4.rotation *= Quaternion.Euler( -Vector3.up *RotationSpeed * RotationSpeed * Time.deltaTime);
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
}
