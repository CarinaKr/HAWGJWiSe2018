using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {

    public float respawnCounter;
    public Camera mainCamera;

    private Collectable[] children;
    private float counter;
    private Transform[] collectables;

	// Use this for initialization
	void Start () {
        counter = respawnCounter+1;
        collectables = GetComponentsInChildren<Transform>(true);
        //FindPositions();
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if(counter>=respawnCounter)
        {
            foreach(Collectable child in GetComponentsInChildren<Collectable>(true))
            {
                if(!child.inFrame || !child.isActiveAndEnabled)
                {
                    child.Respawn();
                    counter = 0;
                    return;
                }
            }
            Debug.Log("all collectable visible");
        }
	}

    public void FindPositions()
    {
        gameObject.SetActive(true);

        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));

        int xMinMultiply = (int)Mathf.Ceil((bottomLeft.x - 2.95f) / 3.92f);
        int xMaxMultiply = (int)Mathf.Floor((topRight.x - 2.95f) / 3.92f);
        int yMinMultiply = (int)Mathf.Ceil((bottomLeft.y - 2.733f) / 3.46f);
        int yMaxMultiply = (int)Mathf.Floor((topRight.y - 2.733f) / 3.46f);

        //float xPosition = 2.95f + Random.Range(xMinMultiply, xMaxMultiply) * 3.92f;
        //float yPosition = 2.733f + Random.Range(yMinMultiply, yMaxMultiply) * 3.46f;
        int k = 0;
        for(int i=xMinMultiply;i<=xMaxMultiply;i++)
        {
            for(int j=yMinMultiply;j<=yMaxMultiply;j++)
            {
                float xPosition = 2.95f + i * 3.92f;
                float yPosition = 2.733f + j * 3.46f;
                collectables[k].position = new Vector2(xPosition, yPosition);
                k++;
                if (k == collectables.Length) return;
            }
        }

        

    }
}
