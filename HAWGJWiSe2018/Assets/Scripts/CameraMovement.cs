using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float speed;
    public Transform mainCamera;

    private Vector2 movement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move(Input.GetAxis("CameraHorizontal"), -1*Input.GetAxis("CameraVertical"));
	}

    public void Move(float inputX,float inputY)
    {
        movement = new Vector2(inputX, inputY) * speed*Time.deltaTime;
        if(movement.magnitude>0.05)
            mainCamera.Translate(movement);
    }
}
