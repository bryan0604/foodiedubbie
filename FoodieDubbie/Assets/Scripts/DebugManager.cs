using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public Text MyText;
    public Button ClearButton;
    private static int jump;

    private void Awake()
    {
        MyText = GetComponent<Text>();
        ClearButton.onClick.AddListener(ClearMessageLog);
    }

    public void OnDebugging(string code)
    {
        jump++;

        MyText.text = MyText.text + "\n " + jump + " " + code;

    }

    void ClearMessageLog()
    {
        MyText.text = "<size=50><b>DEBUG LOG</b> : -</size>";
    }
}
