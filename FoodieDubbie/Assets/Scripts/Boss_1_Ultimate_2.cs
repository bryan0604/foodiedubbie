using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Ultimate_2 : MonoBehaviour
{
    public List<Transform> Points = new List<Transform>();
    public float RotateSpeed=10f;
    public float LoopSpawning = 0.5f;
    public float ScalingSpeed = 1f;

    void Start()
    {
        InvokeRepeating("MarkingSpots", LoopSpawning, LoopSpawning);
    }

    void MarkingSpots()
    {
        foreach (var item in Points)
        {
            Debug.DrawLine(transform.position, item.position, Color.yellow, 60f);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * RotateSpeed);
        transform.localScale += new Vector3(ScalingSpeed*Time.deltaTime, ScalingSpeed * Time.deltaTime, ScalingSpeed * Time.deltaTime); 
    }
}
