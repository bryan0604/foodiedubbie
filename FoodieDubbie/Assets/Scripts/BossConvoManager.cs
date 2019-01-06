using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossConvoManager : MonoBehaviour
{
    public static BossConvoManager singleton;

    [System.Serializable]
    public class TotalPhrasesInPhase
    {
        public List<string> TotalPhrases = new List<string>();
    }

    public List<TotalPhrasesInPhase> Phrases = new List<TotalPhrasesInPhase>();

    [SerializeField]
    private int _currentPhrasesIsAt = 0;

    private void Awake()
    {
        singleton = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            OnActivateBossConvo();
        }
    }

    public void OnActivateBossConvo()
    {
        int _Time;
        bool isAnInteger = false ;

        //Debug.Log(Phrases[BossPhaseManager.singleton.CurrentPhaseMain].TotalPhrases.Count);

        if (_currentPhrasesIsAt >= Phrases[BossPhaseManager.singleton.CurrentPhaseMain].TotalPhrases.Count)
        {
            _currentPhrasesIsAt = 0;
            return;
        }

        if (int.TryParse(Phrases[BossPhaseManager.singleton.CurrentPhaseMain].TotalPhrases[_currentPhrasesIsAt],out _Time))
        {
            isAnInteger = true;   
        }
        else
        {
            isAnInteger = false;
        }

        if(isAnInteger)
        {
            StartCoroutine(CountDownTimer((float)_Time));
        }
        else
        {
            StartCoroutine(OutPutPhrase());
        }

         _currentPhrasesIsAt++;
    }

    IEnumerator CountDownTimer(float _timeInSec)
    {
        Debug.Log("Wait for " + _timeInSec);

        yield return new WaitForSeconds(_timeInSec);

        OnActivateBossConvo();
    }

    IEnumerator OutPutPhrase()
    {
        yield return new WaitForSeconds(0.1f);

        Debug.Log(Phrases[BossPhaseManager.singleton.CurrentPhaseMain].TotalPhrases[_currentPhrasesIsAt - 1]);

        ConvoArrangeManager.singleTon.AddPhraseToText(Phrases[BossPhaseManager.singleton.CurrentPhaseMain].TotalPhrases[_currentPhrasesIsAt - 1]);

        OnActivateBossConvo();
    }

}
