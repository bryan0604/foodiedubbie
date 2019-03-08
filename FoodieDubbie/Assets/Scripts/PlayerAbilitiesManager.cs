using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilitiesManager : MonoBehaviour
{
    public static PlayerAbilitiesManager PAM;
    [Header("Normal Attack")]
    public float Ability1_Cooldown = 5f;
    public Image Ability1_CooldownImg;
    [Space]
    [Header("Ability Two")]
    public float Ability2_Cooldown = 5f;
    public Image Ability2_CooldownImg;
    //public GameObject Ability2_UI;

    public GameObject AbilityUIs;
    public List<Button> AbilitiesButton = new List<Button>();
    public DebugManager DebugMaster;

    private float Ability1_Cooldown_Default;
    private float Ability2_Cooldown_Default;

    private void Awake()
    {
        if (PAM == null) PAM = this;

        Ability1_Cooldown_Default = Ability1_Cooldown;
        Ability2_Cooldown_Default = Ability2_Cooldown;

        AbilitiesButton[0].onClick.AddListener(NormalAttack);
        AbilitiesButton[1].onClick.AddListener(AbilityTwo);
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

        if(BossManager_Level2.singleton != null)
        {
            BossManager_Level2.singleton.OnTakingDamage(5);

        }
        else
        {
            DebugMaster.OnDebugging("Boss Manager script not found!");
        }

        Ability1_CooldownImg.raycastTarget = true;

        Ability1_Cooldown = Ability1_Cooldown_Default;

        Ability1_CooldownImg.fillAmount = Ability1_Cooldown_Default;
    }

    void AbilityTwo()
    {
        DebugMaster.OnDebugging("Skill One! GEBABOOM!");

        if (BossManager_Level2.singleton != null)
        {
            BossManager_Level2.singleton.OnTakingDamage(25);

        }
        else
        {
            DebugMaster.OnDebugging("Boss Manager script not found!");
        }

        Ability2_CooldownImg.raycastTarget = true;

        Ability2_Cooldown = Ability2_Cooldown_Default;

        Ability2_CooldownImg.fillAmount = Ability2_Cooldown_Default;
    }

    private void Update()
    {
        A1_CooldownManagement();
        A2_CooldownManagement();
    }

    void A1_CooldownManagement()
    {
        if(Ability1_Cooldown > 0)
        {
            Ability1_Cooldown -= Time.deltaTime/Ability1_Cooldown_Default;
            Ability1_Cooldown = Mathf.Clamp(Ability1_Cooldown, 0, 1);

            Ability1_CooldownImg.fillAmount = Ability1_Cooldown;
        }
        else
        {
            Ability1_CooldownImg.raycastTarget = false;
        }
    }

    void A2_CooldownManagement()
    {
        if(Ability2_Cooldown > 0)
        {
            Ability2_Cooldown -= Time.deltaTime/Ability2_Cooldown_Default;
            Ability2_Cooldown = Mathf.Clamp(Ability2_Cooldown, 0, 1);

            Ability2_CooldownImg.fillAmount = Ability2_Cooldown;
        }
        else
        {
            Ability2_CooldownImg.raycastTarget = false;
        }
    }
}
