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
    public List<Transform> AllCharacterCanvas = new List<Transform>();
    public Transform MainRotateObj;
    public int PageNo=0;
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
            Game_GlobalInfo.singleton.AvatarsList = new List<bool>(AllCharacterCanvas.Count);

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

            Game_GlobalInfo.singleton.Player_SelectedCharacter = 0;
        }
        else if (_ButtonObj.transform.parent.parent.parent.GetSiblingIndex() == 1)
        {
            Debug.Log("Avatar 00");

            Game_GlobalInfo.singleton.Player_SelectedCharacter = 1;
        }
        else if (_ButtonObj.transform.parent.parent.parent.GetSiblingIndex() == 2)
        {
            Debug.Log("Avatar 01");

            Game_GlobalInfo.singleton.Player_SelectedCharacter = 2;
        }
        else if (_ButtonObj.transform.parent.parent.parent.GetSiblingIndex() == 3)
        {
            Debug.Log("Avatar 02");

            Game_GlobalInfo.singleton.Player_SelectedCharacter = 3;
        }

        GetAvatarSelection();
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

        CheckPage();

        _angle += CurrentAngle;

        RotateNow = true;

    }

    void CheckPage()
    {
        PageNo++;

        if(PageNo >= AllCharacterCanvas.Count)
        {
            PageNo = 0;
        }

        if(Game_GlobalInfo.singleton.AvatarsList.Count!=0)
        {
            if(Game_GlobalInfo.singleton.AvatarsList[PageNo] == true)
            {
                Debug.Log("Unlocked");
            }
            else
            {
                Debug.Log("Locked");
            }
        }

    }

    public void GetAvatarSelection()
    {
        if(Game_GlobalInfo.singleton.Player_SelectedCharacter == 0)
        {
            GameManager_MainMenu.Text_ChosenAvatar.text = "Avatar default selected";
        }
        else if (Game_GlobalInfo.singleton.Player_SelectedCharacter == 1)
        {
            GameManager_MainMenu.Text_ChosenAvatar.text = "Avatar 00 selected";
        }
        else if (Game_GlobalInfo.singleton.Player_SelectedCharacter == 2)
        {
            GameManager_MainMenu.Text_ChosenAvatar.text = "Avatar 01 selected";

        }
        else if (Game_GlobalInfo.singleton.Player_SelectedCharacter == 3)
        {
            GameManager_MainMenu.Text_ChosenAvatar.text = "Avatar 02 selected";
        }
    }
}
