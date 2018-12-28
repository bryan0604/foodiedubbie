using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Ultimate_2 : MonoBehaviour
{
    public List<Transform> Points = new List<Transform>();
    public float RotateSpeed=10f;
    public float LoopSpawning = 0.5f;
    public float ScalingSpeed = 1f;
    public int NumberSpawn = 5;
    private Vector3 DefaultScale;
    private int DefaultNumberSpawn;
    public bool isActivate;

    void Start()
    {
        DefaultNumberSpawn = NumberSpawn;
        DefaultScale = transform.localScale;
    }

    void MarkingSpots()
    {
        if(NumberSpawn > 0)
        {
            foreach (var item in Points)
            {
                NumberSpawn--;
                Debug.DrawLine(transform.position, item.position, Color.yellow, 0.5f);

                GameManager.singleton.TargetOnSpecificSpotsPattern(item.position,2);
            }
        }
        else
        {
            CancelInvoke("MarkingSpots");
            NumberSpawn = DefaultNumberSpawn;
            transform.localScale = DefaultScale;
            isActivate = false;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(isActivate)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * RotateSpeed);
            transform.localScale += new Vector3(ScalingSpeed * Time.deltaTime, ScalingSpeed * Time.deltaTime, ScalingSpeed * Time.deltaTime);
        }
    }

    public void OnActivate()
    {
        isActivate = true;
        gameObject.SetActive(true);
        InvokeRepeating("MarkingSpots", LoopSpawning, LoopSpawning);
    }
}
