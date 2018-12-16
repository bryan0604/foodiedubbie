using UnityEngine;

public class UI_Billboard : MonoBehaviour
{
    public Camera Player_Camera;

	void FixedUpdate ()
    {
        if (Player_Camera != null)
        {
            transform.LookAt(this.transform.position + Player_Camera.transform.rotation * Vector3.forward, Player_Camera.transform.rotation * Vector3.up);
            //transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            //transform.LookAt(new Vector3(0, 0, 90));
            //transform.Rotate(new Vector3(0, 0, 0));

            Player_Camera = Camera.main;
        }
    }
}
