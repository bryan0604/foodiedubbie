using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilitiesManager : MonoBehaviour
{
    public static PlayerAbilitiesManager PAM;
    public PlayerManager PlayerScript;
    [Header("Ability 1")]
    public int Ability1_Quantity_Start = 0;
    //public int Ability1_Quantity = 0;
    public int Ability1_Quantity_Requirement = 5;
    public float Ability1_Cooldown = 5f;
    public Image Ability1_CooldownImg;
    public Text Ability1_TextCount;
    [Space]
    //[Header("Ability Two")]
    //public float Ability2_Cooldown = 5f;
    //public Image Ability2_CooldownImg;

    public GameObject AbilityUIs;
    public List<Button> AbilitiesButton = new List<Button>();
    public DebugManager DebugMaster;

    private float Ability1_Cooldown_Default;
    //private float Ability2_Cooldown_Default;

    private void Awake()
    {
        if (PAM == null) PAM = this;

        Ability1_Cooldown_Default = Ability1_Cooldown;

        AbilitiesButton[0].onClick.AddListener(AbilityOne);
    }

    public void OnEnteringGameLevels()
    {
        AbilityUIs.SetActive(true);

        if (PlayerScript == null) PlayerScript = Behaviour.FindObjectOfType<PlayerManager>();

        PlayerScript.ManaPoints = Ability1_Quantity_Start;
    }

    public void OnQuittingGameLevels()
    {
        AbilityUIs.SetActive(false);
    }

    void AbilityOne()
    {
        if(PlayerScript.ManaPoints >= Ability1_Quantity_Requirement)
        {
            DebugMaster.OnDebugging("Pass skill one!");
        }
        else
        {
            DebugMaster.OnDebugging("Not enough points for skill one!");
            return;
        }

        DebugMaster.OnDebugging("Skill One! GEBABOOM!");

        if (BossManager_Level2.singleton != null)
        {
            BossManager_Level2.singleton.OnTakingDamage(25);

        }
        else
        {
            DebugMaster.OnDebugging("Boss Manager script not found!");
        }

        Ability1_CooldownImg.raycastTarget = true;

        Ability1_Cooldown = Ability1_Cooldown_Default;

        Ability1_CooldownImg.fillAmount = Ability1_Cooldown_Default;
    }

    private void Update()
    {
        A1_CooldownManagement();
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

    public void OnUpdateQuantityText()
    {
        Ability1_TextCount.text = PlayerScript.ManaPoints.ToString();
    }
}
