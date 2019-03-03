using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConvoManager : MonoBehaviour
{
    public List<string> AllPlayersConvo = new List<string>();
    public Text _TextContent;
    public Transform Player;
    public float FloatSpeed = 2f;
    public float ExpireTime = 1f;
    public bool ActivateConvo;
    private Transform _textContent;
    private Vector3 _defaultPositionText;

    private void Start()
    {
        _textContent = _TextContent.transform;
        _defaultPositionText = transform.position;
        Player.GetComponent<PlayerManager>().PlayerConvoM = this;
    }


    private void Update()
    {
        if(ActivateConvo)
        {
            _textContent.Translate(new Vector3(0,FloatSpeed*Time.deltaTime)) ;
        }
    }

    public void OnPlayerConvo()
    {
        int No = Random.Range(0, AllPlayersConvo.Count);

        PlayerConvo(AllPlayersConvo[No]);
    }

    public void PlayerConvo(string _content)
    {
        if(ActivateConvo==false)
        {
            transform.position = Player.position;
            _defaultPositionText = transform.position;

            ActivateConvo = true;

            _textContent.gameObject.SetActive(true);

            _TextContent.text = _content;

            StartCoroutine(TextExpires());
        }
    }

    IEnumerator TextExpires()
    {
        yield return new WaitForSeconds(ExpireTime);

        ActivateConvo = false;

        _textContent.position = _defaultPositionText;

        _textContent.gameObject.SetActive(false);
    }
}
