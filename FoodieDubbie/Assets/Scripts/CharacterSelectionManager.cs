using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public Transform MainRotateObj;
    public List<BillBoard> AllCharacterCanvas = new List<BillBoard>();
    public float RotateSpeed=50f;
    public float CurrentAngle = 90;
    public float _angle;
    public bool RotateNow;

    private void Start()
    {
        CurrentAngle = 360 / AllCharacterCanvas.Count;
        _angle = 360 / AllCharacterCanvas.Count;
    }

    private void Update()
    {
        if(RotateNow)
        {
            MainRotateObj.eulerAngles = Vector3.Lerp(MainRotateObj.rotation.eulerAngles, new Vector3(0, _angle, 0), RotateSpeed * Time.deltaTime);

            if(Vector3.Distance(MainRotateObj.eulerAngles, new Vector3(0, _angle, 0)) > 0.1f)
            {
                
            }
            else
            {
                //CurrentAngle += CurrentAngle;
                _angle += CurrentAngle;

                if (_angle > 360)
                {
                    //Debug.Log("MAx");
                    _angle = CurrentAngle;
                }

                //Debug.Log("Reach");

                RotateNow = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateTheCharacters();
        }
    }

    void RotateTheCharacters()
    {
        RotateNow = true;
    }
}
