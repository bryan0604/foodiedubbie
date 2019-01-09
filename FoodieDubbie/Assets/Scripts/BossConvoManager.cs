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
    public List<string> EndPhrase = new List<string>();

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
            OnSendingEndGamePhrase();
        }
    }

    public IEnumerator ClearQueue()
    {
        StopCoroutine(CountDownTimer(0, false));
        StopCoroutine(OutPutPhrase(false));

        yield return new WaitForSeconds(2f);

        OnActivateBossConvo();
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
            StartCoroutine(CountDownTimer((float)_Time, false));
        }
        else
        {
            StartCoroutine(OutPutPhrase(false));
        }

         _currentPhrasesIsAt++;
    }

    IEnumerator CountDownTimer(float _timeInSec, bool isEndingPhrase)
    {
        Debug.Log("Wait for " + _timeInSec);

        yield return new WaitForSeconds(_timeInSec);

        if(isEndingPhrase)
        {
            OnSendingEndGamePhrase();
        }
        else
        {
            OnActivateBossConvo();
        }
    }

    IEnumerator OutPutPhrase(bool isEndingPhrase)
    {
        yield return new WaitForSeconds(0.1f);

        //Debug.Log(Phrases[BossPhaseManager.singleton.CurrentPhaseMain].TotalPhrases[_currentPhrasesIsAt - 1]);

        if (isEndingPhrase)
        {
            ConvoArrangeManager.singleTon.AddPhraseToText(EndPhrase[_currentPhrasesIsAt-1]);

            OnSendingEndGamePhrase();
        }
        else
        {
            ConvoArrangeManager.singleTon.AddPhraseToText(Phrases[BossPhaseManager.singleton.CurrentPhaseMain].TotalPhrases[_currentPhrasesIsAt - 1]);

            OnActivateBossConvo();
        }
    }

    #region End Phrase Management
    public void OnSendingEndGamePhrase()
    {
        int _Time;
        bool isAnInteger = false;

        if(_currentPhrasesIsAt >= EndPhrase.Count)
        {
            _currentPhrasesIsAt = 0;

            return;
        }


        if (int.TryParse(EndPhrase[_currentPhrasesIsAt], out _Time))
        {
            isAnInteger = true;
        }
        else
        {
            isAnInteger = false;
        }

        if (isAnInteger)
        {
            StartCoroutine(CountDownTimer((float)_Time, true));
        }
        else
        {
            StartCoroutine(OutPutPhrase(true));
        }

        _currentPhrasesIsAt++;
    }
    #endregion

}
