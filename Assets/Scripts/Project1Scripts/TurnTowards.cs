using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KV
{
    public class TurnTowards : MonoBehaviour
    {
        public Transform target;
        public Rigidbody playerCapsule;

        public void SetTarget(Vector3 targetPoint)
        {
            if (target == null)
            {
                target = new GameObject("TempTarget").transform; // Create a temporary target
            }
            target.position = targetPoint;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 targetDir = (target.position - transform.position).normalized;
                float angle = Vector3.SignedAngle(transform.forward, targetDir, transform.up);

                // Adjust torque based on angle for smoother rotation
                float torqueStrength = Mathf.Clamp(angle, -30f, 30f); // Limit turning speed
                playerCapsule.AddRelativeTorque(new Vector3(0, torqueStrength, 0));

            }
        }
    }
}

