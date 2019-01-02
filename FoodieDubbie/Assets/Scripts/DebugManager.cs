using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public static Text MyText;

    private void Awake()
    {
        MyText = GetComponent<Text>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int t = Random.Range(0, 9);
            Debug.Log("adding " + t);
            OnDebugging(t);
        }
    }

    public static void OnDebugging(int code)
    {
        MyText.text = MyText.text + " " + code;
    }
}
