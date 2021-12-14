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

    private Vector3 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
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
        GameObject goBoss = Instantiate(boss, spawnPos, Quaternion.identity);
        canSpawn = false;
    }
}
