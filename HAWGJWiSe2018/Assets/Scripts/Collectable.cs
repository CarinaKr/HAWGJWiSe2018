using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    
    public Enums.Colors mainColor;
    public Camera mainCamera;
    public bool inFrame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 relPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (!(relPosition.x >= -0.1 && relPosition.x <= 1.1 && relPosition.y >= -0.1 && relPosition.y <= 1.1))
        {
            inFrame = false;
        }
        else
        {
            inFrame = true;
        }
    }

    public void Respawn()
    {
        FindColor();
        FindPosition();
    }

    public void FindPosition()
    {
        gameObject.SetActive(true);

        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(0,0,mainCamera.transform.position.z));
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(1,1, mainCamera.transform.position.z));

        int xMinMultiply = (int)Mathf.Ceil((bottomLeft.x - 2.95f) / 3.92f);
        int xMaxMultiply = (int)Mathf.Floor((topRight.x - 2.95f) / 3.92f);
        int yMinMultiply = (int)Mathf.Ceil((bottomLeft.y - 2.733f) / 3.46f);
        int yMaxMultiply = (int)Mathf.Floor((topRight.y - 2.733f) / 3.46f);

        float xPosition = 2.95f + Random.Range(xMinMultiply, xMaxMultiply)*3.92f;
        float yPosition = 2.733f + Random.Range(yMinMultiply,yMaxMultiply )*3.46f;

        transform.position = new Vector2(xPosition, yPosition);
        
        Debug.Log(transform.position);

    }

    public void FindColor()
    {

        if (!GameManager.self.randomColors)
        {
            bool[] pA = GameManager.self.playersAlife;
            int randColor = 0;
            do
            {
                randColor = Random.Range(0, pA.Length-1);
            } while (!pA[randColor]);
            mainColor = (Enums.Colors)randColor;
        }
        else
        {
            mainColor = (Enums.Colors)Random.Range(0, Enums.Colors.GetNames(typeof(Enums.Colors)).Length);
        }

        //TODO set sprites
    }
}

