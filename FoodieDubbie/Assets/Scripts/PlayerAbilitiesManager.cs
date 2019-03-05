using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilitiesManager : MonoBehaviour
{
    public static PlayerAbilitiesManager PAM;
    public GameObject AbilityUIs;
    public List<Button> AbilitiesButton = new List<Button>();
    public DebugManager DebugMaster;

    private void Awake()
    {
        if (PAM == null) PAM = this;

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
}
