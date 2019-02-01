using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform MyCam;

    public void OnLookAtCam()
    {
        transform.LookAt(MyCam);
    }
}
