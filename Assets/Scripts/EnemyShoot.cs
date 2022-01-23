using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        tempPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.position - tempPos, 3f, layerMask: 10);
        Debug.DrawLine(transform.position, Vector2.left * 10f, Color.blue) ;
        if (raycastHit.collider != null)
        {
            Destroy(gameObject);
            if (raycastHit.collider.gameObject)
            {
/*                raycastMinionHit.collider.gameObject.GetComponent<Animator>().SetBool("isDead", true);
                raycastMinionHit.collider.gameObject.layer = 0;*/
            }


        }
/*        else if (raycastBossHit.collider != null)
        {
            if (raycastBossHit.collider.gameObject)
            {
                boss.GetComponent<BossManager>().DecreaseHealth(damage);
                Destroy(gameObject);
            }
        }*/
        tempPos = transform.position;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Player>().health--;
        }
    }*/
}
