using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip EasterEggClip;

    void Update()
    { 
        // If the left mouse button is pressed down...
        if(Input.GetMouseButtonDown(0) == true)
        {
            AudioSource.pitch = Random.Range(0.85f, 1.1f);
            AudioSource.PlayOneShot(EasterEggClip, 0.4f);
            Debug.Log("Spider");
        }
    }
}
