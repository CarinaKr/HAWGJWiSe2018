using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDead : MonoBehaviour {

    public float maxCooldown;
    public Image kolben;

    //private GameManager gameManager;
    private PlayerManager playerManager;
    private int playerNumber;
    private int actionsUsedNum;
    private float cooldownTimer;

    private void Start()
    {
        //gameManager = GameManager.self;
        playerManager=GetComponent<PlayerManager>();
        playerNumber = playerManager.playerNumber;
    }

    // Update is called once per frame
    void Update () {

        
        if (playerManager.isAlive) 
            return;
        UpdateTimer();

        if (playerManager.numberCollected <= 0 || cooldownTimer > 0)
            return;


            if (Input.GetButtonDown("Fire"+playerNumber))
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
        playerManager.numberCollected--;
        cooldownTimer = maxCooldown;
    }

    private void UpdateTimer()
    {
        cooldownTimer -= Time.deltaTime;
        kolben.fillAmount = (((maxCooldown-cooldownTimer) / maxCooldown) * 0.63f);
    }
}
