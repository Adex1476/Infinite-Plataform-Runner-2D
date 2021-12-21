using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!player.isDead)
        {
            Vector2 pos = transform.position;
            
            pos.x -= MinionMove();

            DestroyMinionIfOut(pos); 

            transform.position = pos;
        }
    }

    private void DestroyMinionIfOut(Vector2 pos)
    {
        if (pos.x < -50)
            Destroy(gameObject);
    }

    private float MinionMove() { return player.velocity.x * Time.fixedDeltaTime; }
}
