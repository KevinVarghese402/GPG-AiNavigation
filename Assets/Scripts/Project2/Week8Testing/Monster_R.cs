using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_R : MonoBehaviour
{
    public AudioSource audioSource;
    public SounEmiiter soundEmitter;
     
    void FixedUpdate()
    {
        if (Random.Range(0,200)==0)
        {
            audioSource.Play();
            soundEmitter.Emit();
            
        }
    }
}
