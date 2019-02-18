using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager singleton;
    public GameObject LoadingScreen_Panel;
    public GameObject Prefab;
    //public bool isDelaying;

    private void Awake()
    {
        singleton = this;

        //Debug.Log(singleton, gameObject);
    }

    public void LoadingScreen(bool activate)
    {
        //Debug.Log(singleton + " " + LoadingScreen_Panel,gameObject);
        if(activate)
        {
            LoadingScreen_Panel.SetActive(activate);
        }
        else
        {
            StartCoroutine(Delays(activate));
        }
        
    }

    IEnumerator Delays(bool activate)
    {
        yield return new WaitForSeconds(1f);

        LoadingScreen_Panel.SetActive(activate);
    }
}
