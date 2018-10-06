using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float speed;
    public Transform mainCamera;
    public Vector2 bottomLeft, topRight;

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
        if(movement.magnitude>0.05 && CheckInFrame())
            mainCamera.Translate(movement);
    }

    private bool CheckInFrame()
    {
        if(mainCamera.transform.position.x>=bottomLeft.x && mainCamera.transform.position.x<=topRight.x 
            && mainCamera.transform.position.y>=bottomLeft.y && mainCamera.transform.position.y<=topRight.y)
        {
            return true;
        }
        else
        {
            if (mainCamera.transform.position.x < bottomLeft.x) mainCamera.transform.position = new Vector3(bottomLeft.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
            if (mainCamera.transform.position.x > topRight.x) mainCamera.transform.position = new Vector3(topRight.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
            if (mainCamera.transform.position.y < bottomLeft.y) mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, bottomLeft.y, mainCamera.transform.position.z);
            if (mainCamera.transform.position.y > topRight.y) mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, topRight.y, mainCamera.transform.position.z);
        }
        return false;
    }
}
