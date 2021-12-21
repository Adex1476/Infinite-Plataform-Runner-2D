using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float depth = 1;

    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!player.isDead)
        {
            //Adapta la velocitat segons la velocitat del player i la profunditat de la capa.
            float realVelocity = PerceivedVelocity();

            //Transforma la posicio del background
            Vector2 pos = transform.position;

            pos.x = PerceivedMove(pos , realVelocity);

            transform.position = pos;
        }
    }

    private float PerceivedMove(Vector2 pos, float realVelocity)
    {
        pos.x -= realVelocity / 2 * Time.fixedDeltaTime;
        if (pos.x <= -100)
            return pos.x = 98;
        return pos.x;
    }

    private float PerceivedVelocity() { return player.velocity.x / depth; }
}
