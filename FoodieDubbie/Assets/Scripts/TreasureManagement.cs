using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureManagement : MonoBehaviour
{
    [System.Serializable]
    public class TreasuresRewards
    {
        //[System.Serializable]
        public List<int> RewardsList = new List<int>();
    }
    public static TreasureManagement TM;
    public ParticleSystem ChestSE;
    public Button TreasuresButton;
    public Button ClosedButton;
    public DebugManager DebugMaster;
    public GameObject ChestGroup;
    public GameObject ChestContent;
    public List<TreasuresRewards> Rewards = new List<TreasuresRewards>();
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
        //PointsQuantity = Rewards[code].RewardsList[code];
        for (int i = 0; i < code; i++)
        {
            DebugMaster.OnDebugging(Rewards[code].RewardsList[i].ToString());
        }

        ShowChest();
    }

    void ShowChest()
    {
        ChestGroup.SetActive(true);

    }

    void TreasureOpen()
    {
        DebugMaster.OnDebugging("Treasure Opening...");

        ChestSE.Play();

        StartCoroutine(ShowContentDelay());
    }

    IEnumerator ShowContentDelay()
    {
        yield return new WaitForSeconds(0.5f);

        // add rewards

        Game_GlobalInfo.singleton.Player_Lives += PointsQuantity;

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
