using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public Enums.PlatformType platformtype;
    public float riseSpeed;
    public float riseDistance;
    
    public GameObject storm, water, fire;
    public GameObject glazeOverlay;
    public GameObject fence;
    public Audio audio;

    private float startHeight;
    private float waterTime;
    private float fireTime;
    public bool fireActive {  get; private set; }

    private void Start()
    {
        waterTime = GameManager.self.waterTime;
        fireTime = GameManager.self.fireTime;
    }

    public void TriggerAction()
    {
        
        switch (platformtype)
        {
            case Enums.PlatformType.FEUER:
                StartCoroutine("StartFire");
                return;
            case Enums.PlatformType.STURM:
                StartCoroutine("RisePlatform");
                return;
            case Enums.PlatformType.WASSER:
                StartCoroutine("WaterPlatform");
                return;
        }
    }

    public IEnumerator RisePlatform()
    {
        storm.SetActive(true);
        glazeOverlay.SetActive(false);
        startHeight = transform.position.y;
        float endHeigt = startHeight + riseDistance;
        while(transform.position.y<endHeigt)
        {
            transform.Translate(Vector2.up * riseSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        while (transform.position.y >startHeight)
        {
            transform.Translate(Vector2.down * riseSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = new Vector2(transform.position.x,startHeight);
        storm.SetActive(false);
        glazeOverlay.SetActive(true);
    }

    public IEnumerator WaterPlatform()
    {
        SpriteRenderer rendere = GetComponent<SpriteRenderer>();
        water.SetActive(true);
        glazeOverlay.SetActive(false);
        fence.SetActive(false);
        rendere.enabled = false;
        BoxCollider2D collider= GetComponent<BoxCollider2D>();
        collider.enabled = false;
        float timer = 0;
        while (timer<waterTime)
        {
            yield return new WaitForSeconds(0.5f);
            timer += 0.5f;
        }
        collider.enabled = true;
        water.SetActive(false);
        glazeOverlay.SetActive(true);
        rendere.enabled = true;
        fence.SetActive(true);
    }

    public IEnumerator StartFire()
    {
        fire.SetActive(true);
        fireActive = true;
        float timer = 0;
        while (timer < fireTime)
        {
            yield return new WaitForSeconds(1f);
            timer += 1f;
        }
        fireActive = false;
        fire.SetActive(false);
    }
}
