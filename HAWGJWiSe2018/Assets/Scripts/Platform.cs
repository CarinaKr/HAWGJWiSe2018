using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public Enums.PlatformType platformtype;
    public float riseSpeed;
    public float riseDistance;
    public float waterTime;
    public float fireTimer;
    public GameObject storm, water, fire;
    public GameObject glazeOverlay;

    private float startHeight;
    public bool fireActive {  get; private set; }

    public void TriggerAction()
    {
        switch(platformtype)
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
    }

    public IEnumerator WaterPlatform()
    {
        water.SetActive(true);
        fire.SetActive(true);
        BoxCollider2D collider= GetComponent<BoxCollider2D>();
        collider.enabled = false;
        float timer = 0;
        while (timer<waterTime)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
        collider.enabled = true;
        water.SetActive(true);
    }

    public IEnumerator StartFire()
    {
        fire.SetActive(true);
        fireActive = true;
        float timer = 0;
        while (timer < fireTimer)
        {
            yield return new WaitForSeconds(0.5f);
            timer += 0.5f;
        }
        fireActive = false;
        fire.SetActive(false);
    }
}
