using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    Player player;

    private bool canSpawn;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private float minRange = 500;

    [SerializeField]
    private float maxRange = 550;

    private Vector2 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (canSpawn == true)
        {
            if (!player.isDead && (player.distance > minRange && player.distance < maxRange))
            {
                spawnBoss();
            }
        }
    }

    void spawnBoss()
    {
        Instantiate(boss, spawnPos, Quaternion.identity);
        canSpawn = false;
    }
}
