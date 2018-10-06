using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {

    public float maxCooldown;

    private GameManager gameManager;
    private PlayerManager playerManager;
    private int playerNumber;
    private int actionsUsedNum;
    private float cooldownTimer;

    private void Start()
    {
        gameManager = GameManager.self;
        playerManager=GetComponent<PlayerManager>();
        playerNumber = playerManager.playerNumber;
    }

    // Update is called once per frame
    void Update () {
        if (playerManager.isAlive || playerManager.numberCollected<=0 || cooldownTimer>0)
            return;
        

        if(Input.GetButtonDown("Fire"+playerNumber))
        {
            TriggerAction(Enums.PlatformType.FEUER);
        }
        else if(Input.GetButtonDown("Water" + playerNumber))
        {
            TriggerAction(Enums.PlatformType.WASSER);
        }
        else if(Input.GetButtonDown("Storm" + playerNumber))
        {
            TriggerAction(Enums.PlatformType.STURM);
        }

        cooldownTimer -= Time.deltaTime;
	}

    private void TriggerAction(Enums.PlatformType type)
    {
        foreach (Platform platform in FindObjectsOfType<Platform>())
        {
            if (platform.platformtype == type)
            {
                platform.TriggerAction();
            }
        }
        gameManager.numberCollected--;
        cooldownTimer = maxCooldown;
    }
}
