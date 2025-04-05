using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_G : MonoBehaviour
{
    public GameObject view;
    public SoundReciver soundReciver;


    private void OnEnable()
    {
        soundReciver.HeardSound_Event += SoundReciverOnHeardSound_Event; 
    }

    private void OnDisable()
    {
        soundReciver.HeardSound_Event -= SoundReciverOnHeardSound_Event;
    }



    private void SoundReciverOnHeardSound_Event()
    {
        GotScared(); 
    }

    public void GotScared()
    {
        view.SetActive(false);
    }

   

}
