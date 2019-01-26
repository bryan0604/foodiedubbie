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

        //Debug.Log(singleton, gameObject);
    }

    public void LoadingScreen(bool activate)
    {
        //Debug.Log(singleton + " " + LoadingScreen_Panel,gameObject);
        
        LoadingScreen_Panel.SetActive(activate);
    }
}
