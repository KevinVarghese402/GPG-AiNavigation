using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KV
{
    public class Spawner : MonoBehaviour
    {
        public GameObject playerPrefab;
        public int amount;

        void Start()
        {
            InvokeRepeating(nameof(SpawnObject), 0f, 15f); // Repeat spawning every 20 seconds
        }

        private void SpawnObject()
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(playerPrefab, transform.position, transform.rotation);
            }
        }
    }
}
