using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private GameObject bullet;

    private int moveCounter;

    [SerializeField]
    private int health;

    float direction = -0.1f;
    [SerializeField]
    private float velocity = 1000;
    private int maxCounter = 100;

    private float shootCD = 4;
    private bool canShoot;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        moveCounter = 0;

        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (!player.isDead)
        {
            BossMovement();

            CheckHealth();
        }

    }

    private void CheckHealth()
    {
        if (health < 1)
        {
            DestroyBoss();
        }

    }

    public void DecreaseHealth(int damage)
    {
        health -= damage;
        
    }

    private void DestroyBoss()
    {
        Destroy(gameObject);
    }

    

    void BossMovement()
    {
        Vector2 bossPos = transform.position;
        moveCounter++;
        if (moveCounter >= maxCounter)
        {
            direction = -direction;
            moveCounter = 0;
        }
        bossPos.y += (velocity * Time.fixedDeltaTime * direction) / 10;
        transform.position = bossPos;
    }

    void BossShooting()
    {
        if (canShoot)
        {
            GameObject shoot = Instantiate(bullet, transform.position, Quaternion.identity);
            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
    }
}
