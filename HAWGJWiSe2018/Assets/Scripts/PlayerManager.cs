using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Camera mainCamera;
    public Color clr;
    public Enums.Colors mainColor;
    public int playerNumber;

    private bool _isAlive;
    private Plane[] planes;
    private Collider2D objCollider;
    private int _numCollected;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Collectable" && other.gameObject.GetComponent<Collectable>().mainColor==mainColor)
        {
            numberCollected++;
            GameManager.self.numberCollected++;
            other.gameObject.SetActive(false);
        }
    }

    //private void OnBecameInvisible()
    //{
    //    OutOfFrame();
    //}

    public int numberCollected
    {
        get
        {
            return _numCollected;
        }
        set
        {
            _numCollected = value;
        }
    }

    public void OutOfFrame()
    {
        _isAlive = false;
        GameManager.self.playersAlife[playerNumber] = false;
        GetComponent<SpriteRenderer>().color = Color.grey;

        //TODO: Trigger Feuer-Wasser-Sturm
    }
    public void InFrame()
    {
        _isAlive = true;
        GameManager.self.playersAlife[playerNumber] = true;
        GetComponent<SpriteRenderer>().color = clr;
    }
}
