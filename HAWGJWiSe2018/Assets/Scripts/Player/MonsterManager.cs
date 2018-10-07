using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : PlayerManager {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Player")
        {
            Debug.Log("starts Eating");
            if(collision.transform.position.x < transform.position.x)
            {
                gameObject.transform.eulerAngles = new Vector3(
                                gameObject.transform.eulerAngles.x,
                                gameObject.transform.eulerAngles.y + 180,
                                gameObject.transform.eulerAngles.z);

            }
            StartCoroutine("Eat");
            collision.transform.GetComponent<PlayerManager>().Die();
        }
    }

    public IEnumerator Eat()
    {
        isEating = true;
        animator.SetTrigger("startsEating");
        print(Time.time);
        yield return new WaitForSeconds(2);
        print(Time.time);
        isEating = false;
    }

    public override void OutOfFrame()
    {
        Die();
    }

    public override void Die()
    {
        gameManager.monsterAlife = false;
        isAlive = false;
        GetComponent<SpriteRenderer>().color = Color.grey;
    }
    public override void Revive()
    {
        gameManager.monsterAlife = true;
        isAlive = true;
        GetComponent<SpriteRenderer>().color = clr;
    }

    public override int numberCollected
    {
        get
        {
            return _numberCollected;
        }
        set
        {
            _numberCollected = value;
        }
    }
}
