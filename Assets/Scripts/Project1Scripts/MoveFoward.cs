using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoward : MonoBehaviour
{
    //setting values
    public float speed;
    public int distance;

    public Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * speed);

        bool hitSomething = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance); 

        if (hitSomething)
        {
            Debug.DrawRay(transform.position, transform.forward * 15, Color.red);
           // Debug.Log("Obstacle detected!");
        }

    }
}
