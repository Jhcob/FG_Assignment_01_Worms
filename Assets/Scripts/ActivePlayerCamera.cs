using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivePlayerCamera : MonoBehaviour
{
    [SerializeField] private ActivePlayerManager manager;
    [SerializeField] private Vector3 distanceFromThePlayer;
    [SerializeField] private float speed;



    private void Start()
    {
    }

    void Update()
    {
        Vector3 targetPosition = manager.GetCurrentPlayer().transform.position + distanceFromThePlayer;

        transform.position = manager.GetCurrentPlayer().transform.position + distanceFromThePlayer;
         float step = speed * Time.deltaTime;
         transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
