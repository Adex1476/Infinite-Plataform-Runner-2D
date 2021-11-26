using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float maxXVelocity = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxMaxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float jumpGroundThreshold = 1;

    public int health;
    public bool isDead = false;

    //Animator
    public Animator animator;

    //BugFix
    public LayerMask groundLayerMask;
    public LayerMask minionLayerMask;

    public bool isStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        health = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            Vector2 pos = transform.position;
            float groundDistance = Mathf.Abs(pos.y - groundHeight);

            if (isGrounded || groundDistance <= jumpGroundThreshold)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isGrounded = false;
                    velocity.y = jumpVelocity;
                    isHoldingJump = true;
                    holdJumpTimer = 0;
                    Debug.Log("KEY_DOWN");
                }

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isHoldingJump = false;
                Debug.Log("KEY_RELEASE");
            }

            animator.SetFloat("VelocityY", velocity.y);
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("VelocityX", velocity.x);
        } else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStarted = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isStarted)
        {

            Vector2 pos = transform.position;

            if (isDead)
            {
                return;
            }

            if (pos.y < -20)
            {
                isDead = true;
            }

            //Salt
            if (!isGrounded)
            {
                if (isHoldingJump)
                {
                    holdJumpTimer += Time.fixedDeltaTime;
                    if (holdJumpTimer >= maxHoldJumpTime)
                    {
                        isHoldingJump = false;
                    }
                }
                pos.y += velocity.y * Time.fixedDeltaTime;


                //La gravetat afecta quan no tapejem
                if (!isHoldingJump)
                {
                    velocity.y += gravity * Time.fixedDeltaTime;
                }
                animator.SetFloat("VelocityY", velocity.y);

                //Caiguda collisio vertical
                Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
                Vector2 rayDirection = Vector2.up;
                float rayDistance = velocity.y * Time.fixedDeltaTime;
                RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
                Debug.DrawRay(rayOrigin, rayDirection * -10, Color.blue);
                if (hit2D.collider != null)
                {
                    Ground ground = hit2D.collider.GetComponent<Ground>();
                    if (ground != null)
                    {
                        if (pos.y >= ground.groundHeight)
                        {
                            groundHeight = ground.groundHeight;
                            pos.y = groundHeight;
                            velocity.y = 0;
                            isGrounded = true;
                        }
                    }
                }

                //Colision horizontal
                Vector2 wallOrigin = new Vector2(pos.x, pos.y);
                Vector2 wallDir = Vector2.right;
                RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, wallDir, velocity.x * Time.fixedDeltaTime, groundLayerMask);
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);


                if (wallHit.collider != null)
                {
                    Ground ground = wallHit.collider.GetComponent<Ground>();
                    if (ground != null)
                    {
                        if (pos.y < ground.groundHeight)
                        {
                            velocity.x = 0;
                        }
                    }
                }
            }

            //Colision enemy
            Vector2 playerOrigin = new Vector2(transform.position.x - 3, transform.position.y - 2);
            Vector2 playerDir = Vector2.right;
            float playerRayDistance = 6;
            RaycastHit2D playerHit = Physics2D.Raycast(playerOrigin, playerDir, playerRayDistance * Time.fixedDeltaTime, minionLayerMask);
            Debug.DrawRay(playerOrigin, playerDir * playerRayDistance, Color.green);

            if (playerHit.collider != null)
            {
                health--;
                Destroy(playerHit.collider.gameObject);
                checkIfDead();
            }

            distance += velocity.x * Time.fixedDeltaTime;

            //Correr
            if (isGrounded)
            {
                float velocityRatio = velocity.x / maxXVelocity;
                acceleration = maxAcceleration * (1 - velocityRatio);
                maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;

                velocity.x += acceleration * Time.fixedDeltaTime;
                if (velocity.x >= maxXVelocity)
                {
                    velocity.x = maxXVelocity;
                }


                Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
                Vector2 rayDirection = Vector2.up;
                float rayDistance = velocity.y * Time.fixedDeltaTime;
                RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
                if (hit2D.collider == null)
                {
                    isGrounded = false;
                }
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
            }

            transform.position = pos;
        }
    }

    private void checkIfDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            isDead = true;
        }
    }
    /*
private void OnTriggerEnter2D(Collider2D collision)
{
   if (collision.CompareTag("EnemyTest"))
   {
       health--;
       Destroy(gameObject);
       if (health == 0)
       {
           Destroy(collision.gameObject);
           isDead = true;
       }
   }
   if (collision.CompareTag("Player"))
   {
       Destroy(this.gameObject);
       isDead = true;
   }
}*/
}
