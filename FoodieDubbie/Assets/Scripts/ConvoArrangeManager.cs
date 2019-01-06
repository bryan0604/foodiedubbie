using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvoArrangeManager : MonoBehaviour
{
    public static ConvoArrangeManager singleTon;
    //public List<DialogueText> DialogueTexts = new List<DialogueText>();
    public Transform ParentOfDialogueText;
    //public int IncrementAmount;

    private void Awake()
    {
        singleTon = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            AddPhraseToText("123ABC"+Time.deltaTime.ToString());
        }
    }

    public void AddPhraseToText(string _context)
    {
        List<DialogueText> T_DT = new List<DialogueText>();

        for (int i = 0; i < ParentOfDialogueText.childCount; i++)
        {
            T_DT.Add(ParentOfDialogueText.GetChild(i).GetComponent<DialogueText>());
        }

        foreach (var item in T_DT)
        {
            if(item.isEmpty)
            {
                item.OnAddingTextToDialogue(_context);

                return;
            }
        }
    }

    //void OnAddingTextToDialogue(string _context)
    //{
    //    foreach (var item in DialogueTexts)
    //    {
    //        if(item.GetComponent<Text>().text == "")
    //        {
    //            item.GetComponent<Text>().text = _context;
    //            return;
    //        }
    //    }

    //    Debug.LogWarning("Please wait for the next text to disppear");
    //}

    //void ArrangementText()
    //{
    //    if(IncrementAmount >= DialogueTexts.Count)
    //    {
    //        IncrementAmount = 0;
    //    }

    //    DialogueTexts[IncrementAmount].SetSiblingIndex(DialogueTexts.Count);

    //    DialogueTexts[IncrementAmount].GetComponent<Text>().text = "";

    //    IncrementAmount++;

    //}
}
