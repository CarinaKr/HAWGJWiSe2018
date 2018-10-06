using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {

    private GameManager gameManager;
    private PlayerManager playerManager;

    private void Start()
    {
        gameManager = GameManager.self;
        playerManager.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update () {
        if (playerManager.isAlive)
            return;


	}
}
