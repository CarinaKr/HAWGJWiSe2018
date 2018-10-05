using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Camera mainCamera;
    public Color mainColor;

    private bool _isAlive;
    private Plane[] planes;
    private Collider2D objCollider;

	// Use this for initialization
	void Start () {
        planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        objCollider = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 relPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (!(relPosition.x>=0 && relPosition.x<=1 && relPosition.y>=0 && relPosition.y<=1))
        {
            OutOfFrame();
        }
        else
        {
            InFrame();
        }
    }

    //private void OnBecameInvisible()
    //{
    //    OutOfFrame();
    //}

    public void OutOfFrame()
    {
        _isAlive = false;
        GetComponent<SpriteRenderer>().color = Color.grey;
        //TODO: Trigger Feuer-Wasser-Sturm
    }
    public void InFrame()
    {
        _isAlive = true;
        GetComponent<SpriteRenderer>().color = mainColor;
    }
}
