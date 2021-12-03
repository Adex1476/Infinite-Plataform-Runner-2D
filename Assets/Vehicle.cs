using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    Player player;

    float speed = 50;
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
            pos.x -= (player.velocity.x + speed) * Time.fixedDeltaTime;
            if (pos.x < -50)
            {
                pos.x = Random.Range(300f, 500f);
                pos.y = Random.Range(30f, 40f);
            }
            transform.position = pos;
        }
    }
}
