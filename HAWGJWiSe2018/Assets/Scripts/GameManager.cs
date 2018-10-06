using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager self;

    public int maxCollectedItems;
    public bool randomColors;

    private int _numCollected;
    private bool[] _playersAlife;
    public bool monsterAlife {  get;  set; }

    private void Awake()
    {
        if(!self)
        {
            self = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        _playersAlife = new bool[]{ true,true,true};
	}
	
	// Update is called once per frame
	void Update () {
		
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
