using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : PlayerManager {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerManager>().Die();
        }
    }

    public override void OutOfFrame()
    {
        Die();
    }

    public override void Die()
    {
        gameManager.monsterAlife = false;
        _isAlive = false;
        GetComponent<SpriteRenderer>().color = Color.grey;
    }
    public override void Revive()
    {
        gameManager.monsterAlife = true;
        _isAlive = true;
        GetComponent<SpriteRenderer>().color = clr;
    }
}
