using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip fire, water, storm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayElement(Enums.PlatformType element)
    {
        switch(element)
        {
            case Enums.PlatformType.FEUER:
                audioSource.clip = fire;
                break;
            case Enums.PlatformType.WASSER:
                audioSource.clip = water;
                break;
            case Enums.PlatformType.STURM:
                audioSource.clip = storm;
                break;
        }
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
