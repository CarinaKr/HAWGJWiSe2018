using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    
    public Enums.Colors mainColor;
    public Camera mainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 relPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (!(relPosition.x >= -0.1 && relPosition.x <= 1.1 && relPosition.y >= -0.1 && relPosition.y <= 1.1))
        {
            OutOfFrame();
        }
    }

    public void OutOfFrame()
    {
        Respawn();
    }

    public void Respawn()
    {
        //bool[] pA = GameManager.self.playersAlife;
        //int randColor=0;
        //do
        //{
        //    randColor=Random.R
        //} while (pA[randColor]);
    }
}

