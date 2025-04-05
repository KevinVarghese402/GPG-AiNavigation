using System.Collections.Generic;
using UnityEngine;

public class LocationTeleporter : MonoBehaviour
{
    public List<Transform> teleportLocations; // List of locations to teleport to
    private int currentLocationIndex = 0; // Track the current index of the teleport location

    public Transform fireObject; 
    public Transform vendor; 
    public Transform customer; 
    public float teleportRadius = 5f; 

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("AiPlayer"))
        {
            Debug.Log("AI touched the End Object.");

            // Check if both the vendor and customer are within range of the fire
            if (IsWithinRadius(vendor, fireObject, teleportRadius) && IsWithinRadius(customer, fireObject, teleportRadius))
            {
                Teleport();
            }
            else
            {
                Debug.Log("Vendor or Customer is not within the teleportation radius of the fire.");
            }
        }
        else
        {
            Debug.Log("Touched: " + other.name);
        }
    }

    private bool IsWithinRadius(Transform obj, Transform target, float radius)
    {
        return Vector3.Distance(obj.position, target.position) <= radius;
    }

    private void Teleport()
    {
        if (teleportLocations != null && teleportLocations.Count > 0)
        {
            // Get a random index to teleport to a random location
            currentLocationIndex = Random.Range(0, teleportLocations.Count);
            Transform teleportLocation = teleportLocations[currentLocationIndex];

            // Teleport the AI to the selected location
            transform.position = teleportLocation.position;
            transform.rotation = teleportLocation.rotation;
            Debug.Log("AI Teleported to: " + teleportLocation.position);

            // Optionally: log the index of the new location
            Debug.Log("Current teleport location index: " + currentLocationIndex);
        }
        else
        {
            Debug.LogError("Teleport locations list is not assigned or empty!");
        }
    }
}
