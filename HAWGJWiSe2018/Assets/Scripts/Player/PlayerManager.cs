using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Animator animator;
    public Camera mainCamera;
    public Color clr;
    public Enums.Colors mainColor;
    public int playerNumber;
    public float maxFireTime;
    public Text hudCounter;
    public Image hudPortrait;

    //public int numCollected { get; set; }
    public bool isAlive {  get; protected set; }
    public bool isEating;
    public int playerMoveNumber;//{ get; set; }

    private Plane[] planes;
    private Collider2D objCollider;
    
    protected GameManager gameManager;
    private bool touchesFire;
    protected int _numberCollected;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
        planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        objCollider = GetComponent<Collider2D>();
        gameManager = GameManager.self;
        touchesFire = false;
        isAlive = true;
        isEating = false;
        numberCollected = 5;
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform" && collision.gameObject.GetComponent<Platform>().fireActive && touchesFire)
        {
            StopCoroutine("SurviveFire");
        }
    }

    public IEnumerator SurviveFire()
    {
        float startTime = Time.realtimeSinceStartup;
        touchesFire = true;
        float timer = 0;
        while (timer < maxFireTime)
        {
            yield return new WaitForSeconds(0.5f);
            timer += 0.5f;
        }
        float deltaTime = Time.realtimeSinceStartup - startTime;
        Debug.Log("deltaTime: " + deltaTime);
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
        Destroy(GetComponent("BoxCollider2D"));
        GetComponent<SpriteRenderer>().color = Color.grey;
        GetComponent<Animator>().SetBool("isWalking", false);
        gameObject.SetActive(false);
        hudPortrait.enabled = true;
    }
    public virtual void Revive()
    {
        gameManager.playersAlife[playerNumber - 1] = true; // -1 for players start counting at 1
        isAlive = true;
        GetComponent<SpriteRenderer>().color = clr;
        hudPortrait.enabled = false;
    }

    public virtual int numberCollected
    {
        get
        {
            return _numberCollected;
        }
        set
        {
            _numberCollected = value;
            hudCounter.text = ""+_numberCollected;
        }
    }

}
