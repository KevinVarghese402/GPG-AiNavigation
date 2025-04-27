using System.Collections.Generic;
using UnityEngine;

namespace KV
{
    public class Separation : MonoBehaviour
    {
        public NeighbourTracker neighbourTracker;
        public float separationStrength = 10f;   // Strength of the separation force

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
            ApplySeparationForce();
        }

        private void ApplySeparationForce()
        {
            if (neighbourTracker == null || neighbourTracker.neighbours.Count == 0)
                return;

            Vector3 separationForce = Vector3.zero;

            foreach (Transform neighbour in neighbourTracker.neighbours)
            {
                Vector3 directionToNeighbour = (transform.position - neighbour.position).normalized;
                float distance = Vector3.Distance(transform.position, neighbour.position);

                float invertedDistance = Mathf.Clamp(1.0f / distance, 0f, 1f); // repeling force distance to strengthen force when close
                Vector3 force = directionToNeighbour * invertedDistance;

                separationForce += force;
            }

            separationForce /= neighbourTracker.neighbours.Count;
            rb.AddForce(separationForce * separationStrength, ForceMode.Acceleration);
        }
    }
}
