using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPosition : MonoBehaviour
{
    public float randomMin = -100;
    public float randomMax = 100;

    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject pos4;
    public GameObject pos5;
    public GameObject pos6;

    private int x;
    

    private void Awake()
    {
        // Vector3 randomSpawnPosition =
        //     new Vector3(Random.Range(randomMin, randomMax), (0), Random.Range(randomMin, randomMax));
        //
        // GetComponent<Transform>().position = pos1.GetComponent<Transform>().position;

        RandomPlayerPos();
    }


    private void Update()
    {

    }

    private void RandomPlayerPos()
    {
        x = Random.Range(0, 5);

        switch (x)
        {
            case 0:
                GetComponent<Transform>().position = pos1.GetComponent<Transform>().position;
                break;
            case 1:
                GetComponent<Transform>().position = pos2.GetComponent<Transform>().position;
                break;
            case 2:
                GetComponent<Transform>().position = pos3.GetComponent<Transform>().position;
                break;
            case 3:
                GetComponent<Transform>().position = pos4.GetComponent<Transform>().position;
                break;
            case 4:
                GetComponent<Transform>().position = pos5.GetComponent<Transform>().position;
                break;
            case 5:
                GetComponent<Transform>().position = pos6.GetComponent<Transform>().position;
                break;
        }
    }
}