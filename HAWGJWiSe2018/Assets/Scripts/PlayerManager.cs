using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Camera mainCamera;
    public Color clr;
    public Enums.Colors mainColor;
    public int playerNumber;
    public bool isMonster;

    private bool _isAlive;
    private Plane[] planes;
    private Collider2D objCollider;
    private int _numCollected;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        objCollider = GetComponent<Collider2D>();
        gameManager = GameManager.self;
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
        if(other.tag=="Collectable")
        {
            if (!GameManager.self.randomColors && other.gameObject.GetComponent<Collectable>().mainColor != mainColor)
                return;

            numberCollected++;
            gameManager.numberCollected++;
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
        if(isMonster)
        {
            gameManager.monsterAlife = false;
        }
        else
        {
            gameManager.playersAlife[playerNumber - 1] = false; // -1 for players start counting at 1
        }
        
        GetComponent<SpriteRenderer>().color = Color.grey;

        //TODO: Trigger Feuer-Wasser-Sturm
    }
    public void InFrame()
    {
        _isAlive = true;
        if (isMonster)
        {
            gameManager.monsterAlife = true;
        }
        else
        {
            gameManager.playersAlife[playerNumber - 2] = true; // -1 for players start counting at 1, and -1 for Monster being number 1
        }
        GetComponent<SpriteRenderer>().color = clr;
    }
}
