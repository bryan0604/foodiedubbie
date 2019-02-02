using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameManager_MainMenu GameManager_MainMenu;
    public Button Button_Back;
    public Button Button_Prev;
    public Button Button_Next;
    public List<Button> Button_AvatarSelected = new List<Button>();
    public Transform MainRotateObj;
    public List<Transform> AllCharacterCanvas = new List<Transform>();
    public float RotateSpeed=50f;
    public float CurrentAngle = 90;
    public float _angle;
    public bool RotateNow;

    private void Start()
    {
        if(AllCharacterCanvas.Count == 0)
        {

        }
        else
        {
            CurrentAngle = 90;
        }

        Button_Back.onClick.AddListener(GameManager_MainMenu.OnLeavingAvatarPage);
        Button_Next.onClick.AddListener(NextAvatar);
        //Button_Next.onClick.AddListener

        for (int i = 0; i < Button_AvatarSelected.Count; i++)
        {
            Button_AvatarSelected[i].onClick.AddListener(delegate
            {
                OnSelectingAvatar(EventSystem.current.currentSelectedGameObject);
            });
        }
        
    }

    void OnSelectingAvatar(GameObject _ButtonObj)
    {
        //Debug.Log(_ButtonObj.transform.parent.parent.parent.name);
        if(_ButtonObj.transform.parent.parent.parent.GetSiblingIndex() == 0)
        {
            Debug.Log("Avatar Default");
        }
        else if (_ButtonObj.transform.parent.parent.parent.GetSiblingIndex() == 1)
        {
            Debug.Log("Avatar 00");
        }
    }

    void PreviousAvatar()
    {

    }

    void NextAvatar()
    {
        RotateTheCharacters();
    }

    private void Update()
    {
        if(RotateNow)
        {
            //MainRotateObj.eulerAngles = Vector3.Lerp(MainRotateObj.rotation.eulerAngles, new Vector3(0, _angle, 0), RotateSpeed * Time.deltaTime);

            Vector3 _myRot = new Vector3(0,_angle,0);

            Quaternion _myEuler = Quaternion.Euler(_myRot);

            MainRotateObj.localRotation = Quaternion.Lerp(MainRotateObj.rotation, _myEuler, RotateSpeed * Time.smoothDeltaTime);

            if (Mathf.Round( MainRotateObj.localRotation.eulerAngles.y) >= _angle)
            {
                Debug.Log("A");
                RotateNow = false;
            }

            if (_angle > 360)
            {
                MainRotateObj.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                _angle = 90;
                Debug.Log("Max");
            }
        }
    }

    void RotateTheCharacters()
    {
        if (RotateNow) return;

        _angle += CurrentAngle;

        RotateNow = true;

    }
}
