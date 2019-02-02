using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public float Height_Y=5f;
    public Transform MyCam;

    private void FixedUpdate()
    {
        OnLookAtCam();
    }

    public void OnLookAtCam()
    {
        //Vector3 _pos = new Vector3(0,,0);

        transform.LookAt(MyCam);
    }
}
