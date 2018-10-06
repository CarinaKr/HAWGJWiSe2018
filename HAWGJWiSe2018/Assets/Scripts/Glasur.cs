using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasur : MonoBehaviour {

    public Enums.PlatformType platformtypes;
    [Tooltip("order: Feuer, Wasser, Sturm")]
    public Sprite[] glasuren;

    private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {

        renderer = GetComponent<SpriteRenderer>();
        platformtypes = GetComponentInParent<Platform>().platformtype;

        if(platformtypes == Enums.PlatformType.DEFAULT)
        {
            gameObject.SetActive(false);
            return;
        }

        renderer.sprite = glasuren[(int)platformtypes-1];

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
