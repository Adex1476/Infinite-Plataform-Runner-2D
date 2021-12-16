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
    [SerializeField]
    private GameObject flyingMinion;
    private bool didSpawnMinion = false;



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
                    GetComponent<SpriteRenderer>().color = Color.cyan;
                    //Spawn flying enemies
                    BossMovement();
                    SpawnFlyingMinion();
                    ChangeFase(10, BossFase.Fase2);
                    break;
                case BossFase.Fase2:
                    //Last fase
                    BossMovement();
                    SpawnFlyingMinion();
                    BossShooting();
                    GetComponent<SpriteRenderer>().color = Color.red;
                    CheckHealth();
                    break;
            }
        }
    }

    private void SpawnFlyingMinion()
    {
        if (!didSpawnMinion)
        {
            Instantiate(flyingMinion, new Vector3(transform.position.x, UnityEngine.Random.Range(transform.position.y - 10, transform.position.y + 10), transform.position.z), Quaternion.identity);
            StartCoroutine(MinionSpawnCooldown());
        }
    }

    private IEnumerator MinionSpawnCooldown()
    {
        didSpawnMinion = true;
        yield return new WaitForSeconds(3f);
        didSpawnMinion = false;
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
