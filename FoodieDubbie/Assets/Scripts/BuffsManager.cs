﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsManager : MonoBehaviour
{
    public Transform InternalRing;
    public Transform ExternalRing;
    public float InternalSpinSpeed = 10f;
    public float ExternalSpinSpeed = 8f;
    public float ExpiresTime=5f;
    public float DbuffFollowSpeed = 2f;
    public bool isAdvantageBuff;
    public bool isDisadvantageBuff;
    public Transform TargetedPlayer;
    public SphereCollider _SphereCollider;

    private void FixedUpdate()
    {
        InternalRing.Rotate(Vector3.right, InternalSpinSpeed* Time.deltaTime);
        ExternalRing.Rotate(Vector3.forward, ExternalSpinSpeed * Time.deltaTime);
    }

    public void OnBeingPlaced(Vector3 TargetPoint)
    {
        if(_SphereCollider!=null)
        {
            _SphereCollider.radius = 8f;
        }

        gameObject.SetActive(true);

        transform.position = new Vector3(TargetPoint.x, TargetPoint.y + 0.5f, TargetPoint.z); ;

        StartCoroutine(ExpiresTiming());
    }

    IEnumerator ExpiresTiming()
    {
        yield return new WaitForSeconds(ExpiresTime);

        OnDeactivation();
    }

    public void OnDeactivation()
    {
        TargetedPlayer = null;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(TargetedPlayer!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetedPlayer.position, Time.deltaTime * DbuffFollowSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TargetedPlayer = other.transform;

            if (_SphereCollider != null)
            {
                _SphereCollider.radius = 12f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(TargetedPlayer == other.transform)
            {
                TargetedPlayer = null;

                if (_SphereCollider != null)
                {
                    _SphereCollider.radius = 8f;
                }
            }
        }
    }
}
