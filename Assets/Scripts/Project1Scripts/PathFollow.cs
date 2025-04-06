using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathFollow : MonoBehaviour
{
    public NavigationScript navigation;
    private Vector3[] pathPoints;
    private int pathindex;
    public event Action pathFinished; 


    //                  \\
    private Rigidbody rb;
    public float movementSpeed;
    public TurnTowards turntowards;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        turntowards = GetComponent<TurnTowards>();
    }

    // Update is called once per frame
    void Update()
    {
        pathPoints = navigation.pathPoints;

        if (pathPoints.Length > 0)
        {
            /*
            if (Vector3.Distance(transform.position, pathPoints[pathPoints.Length - 1]) > 1)
            {
                if (Vector3.Distance(transform.position, pathPoints[pathindex]) > 1)
                {
                    Vector3 direction = (pathPoints[pathindex] - transform.position).normalized;
                    rb.MovePosition(transform.position + (direction * movementSpeed *  Time.deltaTime));
                }
                else
                {
                    pathindex++;
                }
            }
            else
            {
                pathFinished?.Invoke();
            }
            */
            if (pathPoints.Length > 1)
            {
                turntowards.SetTarget(pathPoints[1]);

            }


                for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                Debug.DrawLine(pathPoints[i], pathPoints[i + 1], Color.green);
            }
        }

    }
    
}