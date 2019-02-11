using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public Text MyText;
    private static int jump;

    private void Awake()
    {
        MyText = GetComponent<Text>();
    }

    public void OnDebugging(string code)
    {
        jump++;

        MyText.text = MyText.text + "\n " + jump + " " + code;

    }
}
