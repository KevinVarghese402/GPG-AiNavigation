using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KV
{
    public class Align : MonoBehaviour
    {
        private KV.NeighbourTracker neighboursTracker;
        public Rigidbody rb;
        public float force = 100f;

        void Start()
        {
            // Find the NeighbourTracker script on this GameObject
            neighboursTracker = GetComponent<KV.NeighbourTracker>();
            if (neighboursTracker == null)
            {
                Debug.LogError("NeighbourTracker not found on " + gameObject.name);
            }

            // Find the Rigidbody
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
                if (rb == null)
                {
                    Debug.LogError("Rigidbody not assigned on " + gameObject.name);
                }
            }
        }

        void FixedUpdate()
        {
            if (neighboursTracker == null || rb == null) return;

            // Get alignment direction
            Vector3 targetDirection = CalculateMove(neighboursTracker.neighbours);

            // Calculate torque
            Vector3 cross = Vector3.Cross(transform.forward, targetDirection);
            rb.AddTorque(cross * force);

            // Where I WANT to face
            Debug.DrawRay(transform.position, targetDirection * 10f, Color.blue);

            // Where I'm facing right now
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.green);
        }

        public Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
                return transform.forward; // Keep current direction if no neighbors

            Vector3 alignmentDirection = Vector3.zero;

            foreach (Transform item in neighbours)
            {
                alignmentDirection += item.forward;
            }

            alignmentDirection /= neighbours.Count;
            return alignmentDirection.normalized;
        }
    }
}