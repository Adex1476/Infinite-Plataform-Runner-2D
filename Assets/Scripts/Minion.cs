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
            pos.x -= player.velocity.x * Time.fixedDeltaTime;
            if (pos.x < -50)
            {
                Destroy(gameObject);
            }
            transform.position = pos;
        }
    }
}
