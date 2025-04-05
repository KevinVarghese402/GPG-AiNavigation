using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Avoid : MonoBehaviour
{
    public enum AIState { MovingFreely, SlowingDown, TurningLeft, TurningRight, AvoidingCollision }
    public AIState currentState = AIState.MovingFreely;

    [SerializeField]
    public Rigidbody rb;
    public float turnSpeed = 600f;

    public MoveFoward moveFoward;

    float minSpeed = 300f; // Slowest when close
    float maxSpeed = 900f; // Fastest when there is no obstacle

    [SerializeField]
    public float distance = 3.5f;


    //UI TEXT
    public TextMeshProUGUI stateDisplay; 
    public Renderer aiRenderer;
    private Color targetColor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (aiRenderer == null)
            aiRenderer = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        float currentSpeed = rb.velocity.magnitude; // Get movement speed
        bool hitWall = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance);
        bool hitLeft = Physics.Raycast(transform.position, Quaternion.Euler(0, -40, 0) * transform.forward, out RaycastHit hitLeftInfo, distance);
        bool hitRight = Physics.Raycast(transform.position, Quaternion.Euler(0, 40, 0) * transform.forward, out RaycastHit hitRightInfo, distance);

        // Adjust speed based on closest obstacle
        float closestDistance = distance; // Default to max radar range
        if (hitWall) closestDistance = hit.distance;
        if (hitLeft) closestDistance = Mathf.Min(closestDistance, hitLeftInfo.distance);
        if (hitRight) closestDistance = Mathf.Min(closestDistance, hitRightInfo.distance);

        moveFoward.speed = Mathf.Lerp(minSpeed, maxSpeed, closestDistance / distance);

        // State transitions
        if (closestDistance < distance * 0.3f)
        {
            currentState = AIState.AvoidingCollision;
            targetColor = Color.red;
        }
        else if (hitWall || hitLeft || hitRight)
        {
            currentState = AIState.SlowingDown;
            targetColor = Color.yellow;
        }
        else
        {
            currentState = AIState.MovingFreely;
            targetColor = Color.green;
        }

        
        if (hitWall)
        {
            float adjustedTurnSpeed = turnSpeed / (hit.distance + 1);
            rb.AddRelativeTorque(0, adjustedTurnSpeed, 0);
        }
        if (!hitLeft && hitRight)
        {
            float adjustedTurnSpeed = turnSpeed / (hitRightInfo.distance + 1);
            rb.AddRelativeTorque(0, -adjustedTurnSpeed, 0);
        }
        else if (hitLeft && !hitRight)
        {
            float adjustedTurnSpeed = turnSpeed / (hitLeftInfo.distance + 1);
            rb.AddRelativeTorque(0, adjustedTurnSpeed, 0);
        }

        // Color and state display showing what state the AI is
        if (aiRenderer != null)
        {
            aiRenderer.material.color = Color.Lerp(aiRenderer.material.color, targetColor, Time.deltaTime * 5f);
        }

        if (stateDisplay != null)
        {
            stateDisplay.text = "State: " + currentState.ToString();
        }
    }
}
