using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class BossPhaseManager : MonoBehaviour
{
    [System.Serializable]
    public class TotalPhases
    {
        public List<string> BossCommands = new List<string>();

        public TotalPhases(string _bossCom)
        {
            BossCommands.Add(_bossCom);
        }
    }

    [Header("Version 0.1.5 - 17122018")]
    [Header("Compatible to any boss script(ForNow)")]
    public Behaviour BossManager;
    public static BossPhaseManager singleton;
    //public List<string> BossCommands = new List<string>();
    //public List<TotalPhases> listTotalPhase = new List<TotalPhases>();
    public List<TotalPhases> TotalOfPhase = new List<TotalPhases>();
    public int CurrentPhaseMain = 0;
    public int CurrentPhaseisAt = 0;
    public bool isNumber;
    public bool isSkills;
    public bool isLatestUpdated;

    private void OnValidate()
    {
        if (isLatestUpdated)
        {
            isLatestUpdated = false;
            return;
        }
        singleton = this;
    }

    public void AddPhase(int _int)
    {
        isLatestUpdated = true;

        Debug.Log("Total Phase will be "+ _int);

        TotalOfPhase.Clear();

        for (int i = 0; i < _int; i++)
        {
            TotalOfPhase.Add(new TotalPhases(""));
        }
    }

    public void AddSkill(string _skill)
    {
        for (int i = 0; i < TotalOfPhase.Count; i++)
        {
            //Debug.Log(TotalOfPhase[i].BossCommands.Count);

            for (int k = 0; k < TotalOfPhase[i].BossCommands.Count; k++)
            {
                //Debug.Log(TotalOfPhase[i].BossCommands[k]);

                if (TotalOfPhase[i].BossCommands[k] == null || TotalOfPhase[i].BossCommands[k] == "")
                {
                    TotalOfPhase[i].BossCommands[k] = _skill;

                    return;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OnCheckingPhase();
        }
    }

    #region Ability Management
    public void OnCheckingPhase()
    {
        int _tempInt = 0;

        Debug.LogWarning("Checking Phase");

        //BossManager_Level2.singleton.OnTakingDamage(150);

        for (int i = 0; i < TotalOfPhase.Count; i++)
        {
            //Debug.Log(TotalOfPhase[i].BossCommands.Count);

            if(CurrentPhaseMain == TotalOfPhase.Count)
            {
                Debug.Log("=====End of Phases=====");
                return;
            }

            for (int k = 0; k < TotalOfPhase[i+CurrentPhaseMain].BossCommands.Count; k++)
            {
                //Debug.Log(TotalOfPhase[i].BossCommands[k]);

                if(CurrentPhaseisAt == k)
                {
                    Debug.Log(TotalOfPhase[i+CurrentPhaseMain].BossCommands[k]);

                    if(int.TryParse(TotalOfPhase[i + CurrentPhaseMain].BossCommands[k],out _tempInt))
                    {
                        Debug.Log("Is an Integer!");

                        StartCoroutine(WaitForAmountofSeconds((float)_tempInt));
                    }
                    else
                    {
                        //Debug.Log("Is a String!");

                        AbilitiesChosen(TotalOfPhase[i + CurrentPhaseMain].BossCommands[k]);
                    }

                    CurrentPhaseisAt++;

                    if (CurrentPhaseisAt == TotalOfPhase[i+CurrentPhaseMain].BossCommands.Count)
                    {
                        CurrentPhaseisAt = 0;
                    }
                    else
                    {

                    }
                    return;
                }
            }

        }
    }

    IEnumerator WaitForAmountofSeconds(float _time)
    {
        Debug.Log("Wait for " + _time + " seconds");

        yield return new WaitForSeconds(_time);

        Debug.Log("===== Next Step ===== ");

        OnCheckingPhase();
    }

    void AbilitiesChosen(string _skill)
    {
        Debug.Log("Casting - " + _skill);

        OnCheckingPhase();
    }

    #endregion

    #region Clear Management
    public void ClearBossCommandsList()
    {
        for (int i = 0; i < TotalOfPhase.Count; i++)
        {
            //Debug.Log(TotalOfPhase[i].BossCommands.Count);

            for (int k = 0; k < TotalOfPhase[i].BossCommands.Count; k++)
            {
                //Debug.Log(TotalOfPhase[i].BossCommands[k]);

                TotalOfPhase[i].BossCommands[k] = null;

            }

        }
    }

    public void ClearBossCommndOne()
    {
        for (int i = 0; i < TotalOfPhase.Count; i++)
        {
            for (int k = 0; k < TotalOfPhase[i].BossCommands.Count; k++)
            {
                if(TotalOfPhase[i].BossCommands[k] == null || TotalOfPhase[i].BossCommands[k] == "")
                {
                    TotalOfPhase[i].BossCommands.Remove(TotalOfPhase[i].BossCommands[k]);
                }
            }

        }
    }

    public void ResetPhaseCount()
    {
        CurrentPhaseisAt = 0;
        CurrentPhaseMain = 0;

        BossManager_Level2.singleton.HealthPoints = 4500;
        BossManager_Level2.singleton.CurrentPhase = -1;

        Debug.ClearDeveloperConsole();
    }
    #endregion

}



#region Editor
[CustomEditor(typeof(BossPhaseManager))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BossPhaseManager myScript = (BossPhaseManager)target;
        if (GUILayout.Button("Clear Entire List"))
        {
            myScript.ClearBossCommandsList();
        }

        if (GUILayout.Button("Clear One from below"))
        {
            myScript.ClearBossCommndOne();
        }

        if(GUILayout.Button("DryRun Phases"))
        {
            myScript.OnCheckingPhase();
        }

        if(GUILayout.Button("Reset Phase count"))
        {
            myScript.ResetPhaseCount();
        }

        if(GUILayout.Button("FullRun Test"))
        {

        }
    }
}
#endregion
