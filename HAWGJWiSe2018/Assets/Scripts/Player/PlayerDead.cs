using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDead : MonoBehaviour {

    public float maxCooldown;
    public Image kolben;
    public Audio audio;

    //private GameManager gameManager;
    private PlayerManager playerManager;
    private int playerNumber;
    private int actionsUsedNum;
    private float cooldownTimer;

    private void Start()
    {
        //gameManager = GameManager.self;
        playerManager=GetComponent<PlayerManager>();
        playerNumber = playerManager.playerMoveNumber;
    }

    // Update is called once per frame
    void Update () {

        
        if (playerManager.isAlive) 
            return;
        UpdateTimer();

        if (playerManager.numberCollected <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if ( cooldownTimer > 0)
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
        StartCoroutine("PlaySound",type);
        
    }

    private void UpdateTimer()
    {
        cooldownTimer -= Time.deltaTime;
        kolben.fillAmount = (((maxCooldown-cooldownTimer) / maxCooldown) * 0.63f);
    }

    public IEnumerator PlaySound(Enums.PlatformType type)
    {
        audio.PlayElement(type);
        float waitTime = 0.2f;
        switch(type)
        {
            case Enums.PlatformType.FEUER:
                waitTime = GameManager.self.fireTime;
                break;
            case Enums.PlatformType.WASSER:
                waitTime = GameManager.self.waterTime;
                break;
        }
        yield return new WaitForSeconds(waitTime);
        audio.Stop();
    }
}
