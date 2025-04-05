using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SounEmiiter : MonoBehaviour
{
    [SerializeField] private float radiusSound = 10f;

    public void Emit()
    {
        Collider[] results = new Collider[50];

        Physics.OverlapSphereNonAlloc(transform.position, radiusSound, results);  

        foreach(Collider result in results)
        {
            if(result != null)
            {
                SoundReciver soundReceiver = result.GetComponent<SoundReciver>();
                if (soundReceiver != null)
                {
                    soundReceiver.HearingSound();
                }
            }
      
        }
    }
} 
