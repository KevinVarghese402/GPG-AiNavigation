using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReciver : MonoBehaviour
{
    //greenmonster holds this script
    public delegate void SoundHandler();
    public event SoundHandler HeardSound_Event;
    
    internal void HearingSound()
    {
        HeardSound_Event?.Invoke(); 
    }
}
  