using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager self;

    public int maxCollectedItems;
    public bool randomColors;

    public bool monsterAlife {  get;  set; }

    public PlayerManager[] players;
    private GameObject[] playerObjects;
    private int _numCollected;
    private bool[] _playersAlife;
    private Enums.Scene currentScene;
    private SelectPlayers selectPlayers;
    private int playerOrder;
    

    private void Awake()
    {
        if(!self)
        {
            self = this;
            currentScene = Enums.Scene.TITLE_SCREEN;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        _playersAlife = new bool[]{ true,true,true};
        //players = new PlayerManager[4];
        playerObjects = new GameObject[4];
	}

    private void OnLevelWasLoaded(int level)
    {
        if (self!=this) return;

        if(level==(int)Enums.Scene.MENU)
        {
            selectPlayers = FindObjectOfType<SelectPlayers>();
        }
        else if(level==(int)Enums.Scene.GAMEPLAY)
        {
            playerObjects = new GameObject[4];
            players = new PlayerManager[4];
            players[0] = GameObject.Find("Player1").GetComponent<PlayerManager>();
            players[1] = GameObject.Find("Player2").GetComponent<PlayerManager>();
            players[2] = GameObject.Find("Player3").GetComponent<PlayerManager>();
            players[3] = GameObject.Find("Monster").GetComponent<PlayerManager>();
            for (int i=0;i<players.Length;i++)
            {
                int pMN = ((i + 2) + playerOrder + (playerOrder % 2) * 2)%players.Length;
                //players[i].playerMoveNumber = (playerOrder + 3) % players.Length==0 ? 4: (playerOrder + 3) % players.Length;
                players[i].playerMoveNumber = pMN == 0 ? 4 : pMN;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		if(Input.anyKeyDown && currentScene==Enums.Scene.TITLE_SCREEN)
        {
            currentScene = Enums.Scene.MENU;
            SceneManager.LoadScene((int)Enums.Scene.MENU);
        }
        else if(Input.GetButtonDown("Confirm")&&currentScene==Enums.Scene.MENU)
        {
            playerOrder = selectPlayers.playerOrder;
            currentScene = Enums.Scene.GAMEPLAY;
            SceneManager.LoadScene((int)Enums.Scene.GAMEPLAY);
        }
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
            if(_numCollected<=maxCollectedItems)
            {
                //TODO: players win
                Debug.Log("players win");
            }
        }
    }

    public bool[] playersAlife
    {
        get
        {
            return _playersAlife;
        }
        set
        {
            _playersAlife = value;
            int counter=0;
            for(int i=0;i<_playersAlife.Length;i++)
            {
                if (!_playersAlife[i])
                    counter++;
            }
            if(counter==_playersAlife.Length)
            {
                //TODO Monster wins
            }
        }
    }
}
