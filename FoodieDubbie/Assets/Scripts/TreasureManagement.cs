using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureManagement : MonoBehaviour
{
    public Button TreasuresButton;
    public DebugManager DebugMaster;

    private void Awake()
    {
        if(DebugMaster==null)
        {
            DebugMaster = Behaviour.FindObjectOfType<DebugManager>();
        }
    }
    private void Start()
    {
        TreasuresButton.onClick.AddListener(TreasureOpen);
    }

    void TreasureOpen()
    {
        DebugMaster.OnDebugging("Treasure Opening...");
    }
}
