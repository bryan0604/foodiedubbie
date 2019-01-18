using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class BossPhaseManager : MonoBehaviour
{
    #region Phases Array
    [System.Serializable]
    public class TotalPhases
    {
        public List<string> BossCommands = new List<string>();

        public TotalPhases(string _bossCom)
        {
            BossCommands.Add(_bossCom);
        }
    }
    #endregion

    #region Editor
    //[CustomEditor(typeof(BossPhaseManager))]
    //public class ObjectBuilderEditor : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        DrawDefaultInspector();

    //        BossPhaseManager myScript = (BossPhaseManager)target;
    //        if (GUILayout.Button("Clear Entire List"))
    //        {
    //            myScript.ClearBossCommandsList();
    //        }

    //        if (GUILayout.Button("Clear One from below"))
    //        {
    //            myScript.ClearBossCommndOne();
    //        }

    //    }

    //    public string[] test123 = new[]
    //    {
    //        "1","2","3","4","5","6","7","8","9"
    //        ,"SingleTarget"
    //        ,"MultiTarget"
    //        ,"SingleRandom"
    //        ,"MultiRandom"
    //    };
    //}
    #endregion

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

    private void Start()
    {
        singleton = this;
    }

    public void AddPhase(int _int)
    {
        isLatestUpdated = true;

        Debug.Log("Total Phase will be "+ _int);

        TotalOfPhase.Clear();

        for (int i = 0; i < _int+1; i++)
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

    public void ActivatePhaseManager()
    {
        OnCheckingPhase();
    }

    #region Ability Management
    public void OnCheckingPhase()
    {
        int _tempInt = 0;
        bool _isASkill = false;
        //Debug.LogWarning("Checking Phase");

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
                    //Debug.Log(TotalOfPhase[i+CurrentPhaseMain].BossCommands[k]);

                    if(int.TryParse(TotalOfPhase[i + CurrentPhaseMain].BossCommands[k],out _tempInt))
                    {
                        //Debug.Log("Is an Integer!");

                        //StartCoroutine(WaitForAmountofSeconds((float)_tempInt));
                    }
                    else
                    {
                        //Debug.Log("Is a String!");

                        _isASkill = true;

                        //AbilitiesChosen(TotalOfPhase[i + CurrentPhaseMain].BossCommands[k]);
                    }

                    CurrentPhaseisAt++;

                    if (CurrentPhaseisAt == TotalOfPhase[i+CurrentPhaseMain].BossCommands.Count)
                    {
                        CurrentPhaseisAt = 0;
                    }

                    if(_isASkill)
                    {
                        AbilitiesChosen(TotalOfPhase[i + CurrentPhaseMain].BossCommands[k]);
                    }
                    else
                    {
                        StartCoroutine(WaitForAmountofSeconds((float)_tempInt));
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

        Debug.Log(" ");

        OnCheckingPhase();
    }

    void AbilitiesChosen(string _skill)
    {
        Debug.Log("Casting - " + _skill);

        if (_skill == "SingleTarget")
        {
            GameManager.singleton.SkillThree_SingleTarget();
        }
        else if (_skill == "MultiTarget")
        {
            GameManager.singleton.SkillFour_MultiTarget();
        }
        else if (_skill == "SingleRandom")
        {
            GameManager.singleton.SkillOne_SingleRandom();
        }
        else if (_skill == "NormalAttack")
        {
            GameManager.singleton.NormalAttack();
        }
        else if(_skill == "AdvantageBuffs")
        {
            GameManager.singleton.DropAdvantageBuff();
        }
        else if(_skill == "DisadvantageBuffs")
        {
            GameManager.singleton.DropDisadvantageBuff();
        }
        else if(_skill == "RandomBuffsRandomSpot")
        {
            GameManager.singleton.DropBuffsAtRandomSpot();
        }
        else if(_skill == "MultiRandom")
        {
            GameManager.singleton.SkillTwo_MultiRandom();
        }
        else if(_skill == "BossUltimateOne")
        {
            GameManager.singleton.BossOne_UltimateOne();
        }
        else if(_skill == "BossUltimateTwo")
        {
            GameManager.singleton.BossOne_UltimateTwo();
        }
        else if (_skill == "SingleTargetUpgraded")
        {
            GameManager.singleton.SkillThree_Upgraded();
        }
        else if (_skill == "MultiTargetUpgraded")
        {
            GameManager.singleton.SkillFour_Upgraded();
        }
        else if (_skill == "SingleRandomUpgraded")
        {
            GameManager.singleton.SkillOne_Upgraded();
        }
        else if (_skill == "MultiRandomUpgraded")
        {
            GameManager.singleton.SkillTwo_Upgraded();
        }
        else if(_skill == "SingleSpot")
        {
            GameManager.singleton.SkillFive_SingleSpot();
        }
        else if(_skill == "SmallAoeLine")
        {
            GameManager.singleton.SkillSix_AoeLine(1);
        }

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
