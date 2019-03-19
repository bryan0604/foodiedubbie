using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureManagement : MonoBehaviour
{
    [System.Serializable]
    public class TreasuresRewards
    {
        public int Points;
        public int Mana;
    }
    public static TreasureManagement TM;
    public ParticleSystem ChestSE;
    public Button TreasuresButton;
    public Button ClosedButton;
    public Text Text_Content;
    public DebugManager DebugMaster;
    public GameObject ChestGroup;
    public GameObject ChestContent;
    //public List<TreasuresRewards> Rewards = new List<TreasuresRewards>();
    public List<TreasuresRewards> NewRewards = new List<TreasuresRewards>();


    private bool IsTreasureOpened;
    private int PointsQuantity;

    private void Awake()
    {
        TM = this;

        if(DebugMaster==null)
        {
            DebugMaster = Behaviour.FindObjectOfType<DebugManager>();
        }
    }
    private void Start()
    {
        TreasuresButton.onClick.AddListener(TreasureOpen);
        ClosedButton.onClick.AddListener(CloseContent);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnShowingRewards(1); // 250
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            OnShowingRewards(2); // error
        }
    }

    public void OnShowingRewards(int code)
    {
        IsTreasureOpened = false;

        DebugMaster.OnDebugging(NewRewards[code].Points + " " + NewRewards[code].Mana);

        #region unused
        ////for (int i = 0; i < NewRewards[code - 1].; i++)
        //{
        //    //    DebugMaster.OnDebugging(Rewards[i].RewardsList[i].ToString());

        //    //    Game_GlobalInfo.singleton.Player_Lives += Rewards[i].RewardsList[i];

        //    //    Text_Content.text = "Points + " + Rewards[i].RewardsList[i].ToString();

        //}
        #endregion



        ShowChest();
    }

    void ShowChest()
    {
        ChestGroup.SetActive(true);
    }

    void TreasureOpen()
    {
        if (IsTreasureOpened == true) return;

        DebugMaster.OnDebugging("Treasure Opening...");

        IsTreasureOpened = true;

        ChestSE.Play();

        StartCoroutine(ShowContentDelay());
    }

    IEnumerator ShowContentDelay()
    {
        yield return new WaitForSeconds(0.5f);

        // add rewards

        //Game_GlobalInfo.singleton.Player_Lives += PointsQuantity;

        LivesManager.singleTonnie.OnUpdateLivesDisplay();

        ChestContent.SetActive(true);
    }

    void CloseContent()
    {
        ChestGroup.SetActive(false);
        ChestContent.SetActive(false);
        ChestSE.Stop();
    }
}

internal class NamedArrayAttribute : Attribute
{
}