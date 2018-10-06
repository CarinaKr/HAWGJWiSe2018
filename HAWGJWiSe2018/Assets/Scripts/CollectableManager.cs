using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {

    public float respawnCounter;

    private Collectable[] children;
    private float counter;

	// Use this for initialization
	void Start () {
        counter = respawnCounter+1;
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if(counter>=respawnCounter)
        {
            foreach(Collectable child in GetComponentsInChildren<Collectable>(true))
            {
                if(!child.inFrame || !child.isActiveAndEnabled)
                {
                    child.Respawn();
                    counter = 0;
                    return;
                }
            }
            Debug.Log("all collectable visible");
        }
	}
}
