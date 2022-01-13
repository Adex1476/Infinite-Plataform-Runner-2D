using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingMinion : MonoBehaviour
{
    Player player;
    GameManager gameManager;
    public Vector3 position;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!player.isDead && !gameManager.isPaused)
        {
            Vector2 pos = transform.position;
            
            pos.x -= MinionMoveX();
            pos.y -= MinionMoveY();

            DestroyMinionIfOut(pos);

            direction = transform.position - position;
            if (transform.position.x > player.transform.position.x + 8)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, player.velocity.x * Time.fixedDeltaTime);

            else transform.position = pos;
            position = transform.position;
        }
    }

    private float MinionMoveY()
    {
        if (player.transform.position.y > transform.position.y)
            return -0.1f;
        else if (player.transform.position.y < transform.position.y)
            return 0.1f;
        else 
            return 0;
    }

    private void DestroyMinionIfOut(Vector2 pos)
    {
        if (pos.x < -50)
            Destroy(gameObject);
    }

    private float MinionMoveX() { return player.velocity.x * Time.fixedDeltaTime; }
}
