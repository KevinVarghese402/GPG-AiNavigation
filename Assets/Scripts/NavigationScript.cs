
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Transform player;  // The AI that follows the path
    public Transform target;  // The destination (green object)


    public Vector3[] pathPoints;
    private NavMeshPath paths;

    //visualisation of the corners
    
    private float minimumDistanceToCorner = 1f; 

    private void Update()
    {
        paths = new NavMeshPath();
        SetPath();  // Calculate path each frame
    }

    private void SetPath()
    {
        var pathFound = NavMesh.CalculatePath(player.position, target.position, NavMesh.AllAreas, paths);

        if (pathFound)
        {
            Debug.Log($"Path points: {paths.corners.Length}");
            foreach (Vector3 point in paths.corners)
            {
                Debug.Log($"Point: {point}"); // Showing the points of the AI player
            }

            Debug.Log("Path found!");

            pathPoints = paths.corners;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (paths != null && paths.corners.Length > 0)
        {
            foreach (Vector3 corner in paths.corners)
            {
                Gizmos.DrawSphere(corner, minimumDistanceToCorner / 2f);
            }
        }
    }

}
