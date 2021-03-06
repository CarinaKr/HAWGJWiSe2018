﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayers : MonoBehaviour {

    public Sprite[] portraits;
    public Image[] spaces;
    public Image leftArrow, rightArrow;
    public Color activateColor, defaultColor;

    public int playerOrder { private set; get; }
    private bool needsReset;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetAxis("SelectionHorizontal") >=0.5 || Input.GetAxis("SelectionHorizontal") <=-0.5) && !needsReset)
        {
            StartCoroutine("ClickArrow",Input.GetAxis("SelectionHorizontal"));
            needsReset = true;
            playerOrder = (playerOrder-(Mathf.RoundToInt(Input.GetAxis("SelectionHorizontal"))) % spaces.Length);
            if(playerOrder<0)
            {
                playerOrder = spaces.Length - Mathf.Abs(playerOrder);
            }
            for(int i=0;i<spaces.Length;i++)
            {
                spaces[i].sprite = portraits[(i+playerOrder) % spaces.Length];
            }
            Debug.Log(playerOrder);
        }
        if(Input.GetAxis("SelectionHorizontal")>=-0.5 && Input.GetAxis("SelectionHorizontal")<=0.5)
        {
            needsReset = false;
        }
    }

    public IEnumerator ClickArrow(float position)
    {
        if(position<0)
        {
            leftArrow.color = activateColor;
        }
        else if(position>0)
        {
            rightArrow.color = activateColor;
        }
        yield return new WaitForSeconds(0.15f);
        leftArrow.color = defaultColor;
        rightArrow.color = defaultColor;
    }
}
