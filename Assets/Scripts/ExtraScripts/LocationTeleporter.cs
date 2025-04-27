using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KV
{
    public class RandomLocation : MonoBehaviour
    {
        public List<Transform> teleportLocations; // List of locations to teleport to
        private int currentLocationIndex = 0; // Track the current index of the teleport location

        private void OnTriggerEnter(Collider other)
        {

            Debug.Log($"Collision detected with: {other.name} (Tag: {other.tag})");

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
                Debug.Log("Teleporting AI...");


                Transform teleportLocation = teleportLocations[currentLocationIndex];
                Debug.Log($"Teleporting to location index: {currentLocationIndex} at position: {teleportLocation.position}");


                transform.position = teleportLocation.position;
                transform.rotation = teleportLocation.rotation;
                Debug.Log("AI Teleported to: " + teleportLocation.position);


                currentLocationIndex++;


                if (currentLocationIndex >= teleportLocations.Count)
                {
                    Debug.Log("No more teleport locations. AI has finished teleporting.");
                    currentLocationIndex = 0; // Reset to the first location
                }
                else
                {
                    Debug.Log($"Next teleport location index: {currentLocationIndex}");
                }
            }
            else
            {
                Debug.LogError("Teleport locations list is not assigned or empty!");
            }
        }
    }
}
