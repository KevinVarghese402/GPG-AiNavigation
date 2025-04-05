using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : MonoBehaviour
{
    public NeighbourTracker neighbourTracker;
    public float cohesionStrength = 5f; // Adjust force strength

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (neighbourTracker == null)
        {
            neighbourTracker = GetComponent<NeighbourTracker>();
        }
    }

    private void FixedUpdate()
    {
        ApplyCohesion();
    }

    private void ApplyCohesion()
    {
        if (neighbourTracker.neighbours.Count == 0) return;

        Vector3 averagePosition = Vector3.zero;
        foreach (Transform neighbour in neighbourTracker.neighbours)
        {
            averagePosition += neighbour.position;
        }
        averagePosition /= neighbourTracker.neighbours.Count;

        Vector3 directionToAverage = (averagePosition - transform.position).normalized;
        rb.AddForce(directionToAverage * cohesionStrength);
    }
}
