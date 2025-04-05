using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourTracker : MonoBehaviour
{
    public List<Transform> neighbours = new List<Transform>();
    public float checkLOSInterval = 1.0f; // How often to check Line of Sight (in seconds)
    public float detectionRadius;

    private void Start()
    {
        StartCoroutine(CheckLineOfSightRoutine());
        GetComponent<SphereCollider>().radius = detectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.isTrigger || other.transform == transform) return;

        neighbours.Add(other.transform);
        Debug.Log($"Neighbour added: {other.name}");

    }

    private void OnTriggerExit(Collider other)
    {
        if (neighbours.Contains(other.transform))
        {
            neighbours.Remove(other.transform);
            Debug.Log($"Neighbour removed: {other.name}");
        }
    }

    // Checking if they are behind wall
    private IEnumerator CheckLineOfSightRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkLOSInterval);

            for (int i = neighbours.Count - 1; i >= 0; i--)
            {
                if (!HasLineOfSight(neighbours[i]))
                {
                    //Debug.Log($"Lost sight of: {neighbours[i].name}");
                    neighbours.RemoveAt(i);
                }
            }
        }
    }

    private bool HasLineOfSight(Transform target)
    {
        RaycastHit hit;
        Vector3 direction = (target.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            return hit.transform == target;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Set Gizmo color
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, sphereCollider.radius);
        }
    }
}
