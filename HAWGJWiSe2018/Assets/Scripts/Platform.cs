using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public Enums.PlatformType platformtype;
    public float riseSpeed;
    public float riseDistance;
    public float waterTime;

    private float startHeight;
    public bool fireActive {  get; private set; }

    public void TriggerAction()
    {
        switch(platformtype)
        {
            case Enums.PlatformType.FEUER:

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
        startHeight = transform.position.y;
        float endHeigt = startHeight + riseDistance;
        while(transform.position.y<endHeigt)
        {
            transform.Translate(Vector2.up * riseSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    public IEnumerator WaterPlatform()
    {
        BoxCollider2D collider= GetComponent<BoxCollider2D>();
        collider.enabled = false;
        float timer = 0;
        while (timer<waterTime)
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
        collider.enabled = true;
    }
}
