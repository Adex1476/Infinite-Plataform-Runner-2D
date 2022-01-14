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
    GameManager gameManager;
    [SerializeField]
    private GameObject bullet;

    private int moveCounter;
    float direction = -0.1f;
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
    [SerializeField]
    private int health;
    [SerializeField]
    private int healthToChangeToSecondFase;
    [SerializeField]
    private int healthToChangeToThirdFase;
    private bool didSpawnMinion = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        moveCounter = 0;

        bossFase = BossFase.Fase0;

        StartCoroutine(ShootCooldown(shootCD - 2));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!player.isDead && !gameManager.isPaused)
        {
            switch (bossFase)
            {
                case BossFase.Fase0:
                    BossMovement();
                    BossShooting(shootCD);
                    ChangeFase(healthToChangeToSecondFase, BossFase.Fase1);
                    break;
                case BossFase.Fase1:
                    GetComponent<SpriteRenderer>().color = Color.cyan; //DEBUG
                    //Spawn flying enemies
                    BossMovement();
                    SpawnFlyingMinion();
                    ChangeFase(healthToChangeToThirdFase, BossFase.Fase2);
                    break;
                case BossFase.Fase2:
                    //Last fase
                    if (health > 0)
                    {
                        BossMovement();
                        SpawnFlyingMinion();
                        BossShooting(shootCD - 2);
                        GetComponent<SpriteRenderer>().color = Color.red; //DEBUG
                    }
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
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Animator>().SetBool("isDead", true);
            gameObject.layer = 0;
            //DestroyBoss();
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

    void BossShooting(float cooldown)
    {
        if (canShoot)
        {
            GameObject shoot = Instantiate(bullet, transform.position, Quaternion.identity);
            shoot.GetComponent<Rigidbody2D>().AddForce((GameObject.Find("Player").transform.position - transform.position).normalized * bulletForce, ForceMode2D.Impulse);
            EffectAudioController.PlaySound("shootboss");
            StartCoroutine(ShootCooldown(cooldown));
        }
    }

    private IEnumerator ShootCooldown(float cooldown)
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
