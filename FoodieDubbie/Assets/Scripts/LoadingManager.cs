using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager singleton;
    public GameObject LoadingScreen_Panel;
    public GameObject Prefab;

    private void Awake()
    {
        singleton = this;
    }

    public void LoadingScreen(bool activate)
    {
        LoadingScreen_Panel.SetActive(activate);
    }
}
