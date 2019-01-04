using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameVictory : MonoBehaviour
{
    public string Scene_MainMenu;
    public Button Button_MainMenu;

    public void ShowButton(string Button_Tag)
    {
        Button_MainMenu.onClick.AddListener(OnBacktoMainMenu);

        StartCoroutine(ShowDelay(Button_Tag));
    }

    IEnumerator ShowDelay(string _tag)
    {
        yield return new WaitForSeconds(5f);

        if(_tag == "Button Main Menu")
        {
            Button_MainMenu.gameObject.SetActive(true);
        } 
    }

    void OnBacktoMainMenu()
    {
        SceneManager.LoadScene(Scene_MainMenu,LoadSceneMode.Single);
    }

}
