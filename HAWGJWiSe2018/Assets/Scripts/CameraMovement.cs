using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float speed;
    public Transform mainCameraTransform;
    public Vector2 bottomLeft, topRight;
    public float minDistanceToRim;

    private PlayerManager playerManager;
    private Vector2 movement;
    private bool cameraLockedX,cameraLockedY;
    private float offsetX, offsetY;
    private Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = mainCameraTransform.gameObject.GetComponent<Camera>();
        playerManager = GetComponent<MonsterManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Move(Input.GetAxis("CameraHorizontal"+playerManager.playerMoveNumber), -1*Input.GetAxis("CameraVertical"+ playerManager.playerMoveNumber));

        CheckPlayerPosition();
        if (cameraLockedX)
        {
            mainCameraTransform.position =new Vector3( transform.position.x + offsetX,mainCamera.transform.position.y,mainCamera.transform.position.z);
        }   
        if(cameraLockedY)
        {
            mainCameraTransform.position = new Vector3(mainCamera.transform.position.x, transform.position.y + offsetY, mainCamera.transform.position.z);
        }
        CheckInFrame();
    }

    public void Move(float inputX,float inputY)
    {
        movement = new Vector2(inputX, inputY) * speed*Time.deltaTime;
        if(movement.magnitude>0.05 && CheckInFrame())
        {
            mainCameraTransform.Translate(movement);
        }
            
    }

    private bool CheckInFrame()
    {
        if(mainCameraTransform.transform.position.x>=bottomLeft.x && mainCameraTransform.transform.position.x<=topRight.x 
            && mainCameraTransform.transform.position.y>=bottomLeft.y && mainCameraTransform.transform.position.y<=topRight.y)
        {
            return true;
        }
        else
        {
            if (mainCameraTransform.transform.position.x < bottomLeft.x) mainCameraTransform.transform.position = new Vector3(bottomLeft.x, mainCameraTransform.transform.position.y, mainCameraTransform.transform.position.z);
            if (mainCameraTransform.transform.position.x > topRight.x) mainCameraTransform.transform.position = new Vector3(topRight.x, mainCameraTransform.transform.position.y, mainCameraTransform.transform.position.z);
            if (mainCameraTransform.transform.position.y < bottomLeft.y) mainCameraTransform.transform.position = new Vector3(mainCameraTransform.transform.position.x, bottomLeft.y, mainCameraTransform.transform.position.z);
            if (mainCameraTransform.transform.position.y > topRight.y) mainCameraTransform.transform.position = new Vector3(mainCameraTransform.transform.position.x, topRight.y, mainCameraTransform.transform.position.z);
        }
        return false;
    }

    private void CheckPlayerPosition()
    {
        Vector2 relPosition = mainCamera.WorldToViewportPoint(transform.position);
        if ((relPosition.x<minDistanceToRim ||  relPosition.x>1-minDistanceToRim ))
        {
            if(!cameraLockedX)
            {
                offsetX = mainCameraTransform.position.x-transform.position.x;
                cameraLockedX = true;
            }
        }
        else
        {
            cameraLockedX = false;
        }

        if (relPosition.y < minDistanceToRim || relPosition.y > 1 - minDistanceToRim)
        {
            if (!cameraLockedY)
            {
                offsetY = mainCameraTransform.position.y - transform.position.y;
                cameraLockedY = true;
            }
        }
        else
        {
            cameraLockedY = false;
        }
    }
}
