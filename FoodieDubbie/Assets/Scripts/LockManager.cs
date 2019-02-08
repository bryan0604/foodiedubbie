using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public GameObject LockImage;

    public void SetLockOrUnlock(bool toLock)
    {
        if(toLock)
        {
            LockImage.SetActive(toLock);
        }
        else
        {
            LockImage.SetActive(toLock);
        }
    }
}
