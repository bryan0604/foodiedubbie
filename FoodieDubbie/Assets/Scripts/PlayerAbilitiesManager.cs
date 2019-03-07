using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilitiesManager : MonoBehaviour
{
    public static PlayerAbilitiesManager PAM;
    public float Ability1_Cooldown = 5f;
    public Image Ability1_CooldownImg;
    public GameObject AbilityUIs;
    public List<Button> AbilitiesButton = new List<Button>();
    public DebugManager DebugMaster;
    private float Ability1_Cooldown_Default;
    private void Awake()
    {
        if (PAM == null) PAM = this;

        Ability1_Cooldown_Default = Ability1_Cooldown;

        AbilitiesButton[0].onClick.AddListener(NormalAttack);
    }

    public void OnEnteringGameLevels()
    {
        AbilityUIs.SetActive(true);
    }

    public void OnQuittingGameLevels()
    {
        AbilityUIs.SetActive(false);
    }

    void NormalAttack()
    {
        DebugMaster.OnDebugging("Normal Attack!");
    }

    private void Update()
    {
        //Debug.Log((Ability1_Cooldown = Mathf.Clamp(Ability1_Cooldoxwn - Time.deltaTime, 0, 1));
        UpdateCooldownImage();   
    }

    void UpdateCooldownImage()
    {
        if(Ability1_Cooldown > 0)
        {

            Ability1_Cooldown = Mathf.Clamp(Ability1_Cooldown - Time.deltaTime, 0, 1);

            Ability1_CooldownImg.fillAmount = Ability1_Cooldown;
        }
    }
}
