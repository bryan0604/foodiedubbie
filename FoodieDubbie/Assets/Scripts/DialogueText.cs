using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour
{
    public Text myText;

    public bool isEmpty=true;

    public void OnAddingTextToDialogue(string _context)
    {
        //foreach (var item in DialogueTexts)
        //{
        //    if (item.GetComponent<Text>().text == "")
        //    {
        //        item.GetComponent<Text>().text = _context;
        //        return;
        //    }
        //}
        if(myText.text == "")
        {
            isEmpty = false;

            myText.text = _context;

            StartCoroutine(ArrangementText());

            return;
        }

        //Debug.LogWarning("Please wait for the next text to disppear");
    }

    IEnumerator ArrangementText()
    {
        yield return new WaitForSeconds(5f);

        transform.SetAsLastSibling();

        myText.text = "";

        isEmpty = true;
    }
}
