using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTeleporter : MonoBehaviour
{
    public List<Transform> teleportLocations; // List of locations to teleport to
    private int currentLocationIndex = 0; // Track the current index of the teleport location

    private void OnTriggerEnter(Collider other)
    {
        // Check if the AI collides with the end object
        if (other.CompareTag("AiPlayer"))
        {
            Debug.Log("AI touched the End Object.");
            Teleport();
        }
        else
        {
            Debug.Log("Touched: " + other.name);
        }
    }

    private void Teleport()
    {
        
        if (teleportLocations != null && teleportLocations.Count > 0)
        {
            // Teleport the AI to the current location in the list
            Transform teleportLocation = teleportLocations[currentLocationIndex];
            transform.position = teleportLocation.position;
            transform.rotation = teleportLocation.rotation; 
            Debug.Log("AI Teleported to: " + teleportLocation.position);

            // Increment the index to the next location
            currentLocationIndex++;

            //Checks if theres anymore to go to 
            if (currentLocationIndex >= teleportLocations.Count)
            {
                Debug.Log("No more teleport locations. AI has finished teleporting.");
                currentLocationIndex = 0; 
            }
        }
        else
        {
            Debug.LogError("Teleport locations list is not assigned or empty!");
        }
    }
}