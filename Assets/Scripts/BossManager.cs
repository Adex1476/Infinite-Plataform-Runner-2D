using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossFase
{
    Fase0,
    Fase1,
    Fase2
}
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
    [SerializeField]
    private BossFase bossFase;
    [SerializeField]
    private float bulletForce = 30;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        moveCounter = 0;

        health = 30;


        bossFase = BossFase.Fase0;

        StartCoroutine(ShootCooldown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Debug.Log(bossFase);
        if (!player.isDead)
        {
            switch (bossFase)
            {
                case BossFase.Fase0:
                    BossMovement();
                    BossShooting();
                    ChangeFase(20, BossFase.Fase1);
                    break;
                case BossFase.Fase1:
                    BossMovement();
                    GetComponent<SpriteRenderer>().color = Color.cyan;
                    //Spawn flying enemies
                    ChangeFase(10, BossFase.Fase2);
                    break;
                case BossFase.Fase2:
                    //Last fase
                    GetComponent<SpriteRenderer>().color = Color.red;
                    CheckHealth();
                    break;
            }
        }
    }

    private void ChangeFase(int limitHealth, BossFase faseToChange)
    {
        if (health <= limitHealth)
        {
            bossFase = faseToChange;
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
            shoot.GetComponent<Rigidbody2D>().AddForce((GameObject.Find("Player").transform.position - transform.position).normalized * bulletForce, ForceMode2D.Impulse);
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
