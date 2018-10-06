using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Camera mainCamera;
    public Color clr;
    public Enums.Colors mainColor;
    public int playerNumber;
    public float maxFireTime;

    //private Animator animator;
    public bool isAlive {  get; protected set; }
    private Plane[] planes;
    private Collider2D objCollider;
    private int _numCollected;
    protected GameManager gameManager;
    private bool touchesFire;

	// Use this for initialization
	void Start () {
        //animator = GetComponent<Animator>();
        planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        objCollider = GetComponent<Collider2D>();
        gameManager = GameManager.self;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAlive)
            return;

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

    void OnTriggerEnter2D(Collider2D other)
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag=="Platform" && collision.gameObject.GetComponent<Platform>().fireActive && !touchesFire)
        {
            StartCoroutine("SurviveFire");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Platform" && collision.gameObject.GetComponent<Platform>().fireActive && touchesFire)
        {
            StopCoroutine("SurviveFire");
        }
    }

    public IEnumerator SurviveFire()
    {
        touchesFire = true;
        float timer = 0;
        while (timer < maxFireTime)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
        Die();
    }

    public virtual void OutOfFrame()
    {
        Die();

        //TODO: Trigger Feuer-Wasser-Sturm
    }
    public void InFrame()
    {
        Revive();
    }

    public virtual void Die()
    {
        gameManager.playersAlife[playerNumber - 1] = false; // -1 for players start counting at 1
        isAlive = false;
        GetComponent<SpriteRenderer>().color = Color.grey;
    }
    public virtual void Revive()
    {
        gameManager.playersAlife[playerNumber - 1] = true; // -1 for players start counting at 1
        isAlive = true;
        GetComponent<SpriteRenderer>().color = clr;
    }

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

}
