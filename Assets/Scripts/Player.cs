using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    public float gravity;

    public Vector2 velocity;

    [SerializeField]
    private float maxXVelocity = 100;

    [SerializeField]
    private float maxAcceleration = 10;

    [SerializeField]
    private float acceleration = 10;

    public float distance = 0;

    public float jumpVelocity = 20;

    [SerializeField]
    private float groundHeight = 10;

    [SerializeField]
    private bool isGrounded = false;


    [SerializeField]
    private bool isHoldingJump = false;

    public float maxHoldJumpTime = 0.4f;

    [SerializeField]
    private float maxMaxHoldJumpTime = 0.4f;

    [SerializeField]
    private float holdJumpTimer = 0.0f;

    [SerializeField]
    private float jumpGroundThreshold = 1;

    public int health;
    private int maxHealth;

    public bool isDead = false;

    //Animator

    [SerializeField]
    private Animator animator;

    //Camera
    public Camera playerCamera;

    [SerializeField]
    private float initialCameraSize;

    //LayerMasks

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private LayerMask minionLayerMask;


    [SerializeField]
    private bool isStarted = false;

    private bool hurt = false;

    public bool isDone = false;


    // Start is called before the first frame update
    void Start()
    {
        health = 4;
        maxHealth = health;

        playerCamera = Camera.main;

        initialCameraSize = playerCamera.orthographicSize;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            JumpingInput();

            //SetAnimation();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStarted = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isStarted && !gameManager.isPaused)
        {
            Vector2 pos = transform.position;

            CheckPlayerDeath();

            //Player moviemnt eix Y
            CheckFallDeath(pos);

            CameraHeight(pos);

            IncreaseCameraSize();

            //WIP: Player moving back while camera incresing size
            pos.x -= MovePlayerInsideCamera(pos);

            //Salt
            pos.y = JumpMovement(pos);

            //BUG CHEACK IF GROUNDED   CheckIfGrounded(Vector2 pos)      //Debug.Log($"grounded   {isGrounded}   {Time.time}");



            //Colision enemy
            CheckEnemyCollision();

            //Correr
            IcreaseDistance();

            IncreaseVelocity(pos);

            SetAnimation();

            transform.position = pos;
        }
    }

    private void CheckIfBlocked(Vector2 pos)
    {
        //Colision horizontal
        Vector2 wallOrigin = new Vector2(pos.x, pos.y);
        Vector2 wallDir = Vector2.right;
        RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, wallDir, velocity.x * Time.fixedDeltaTime, groundLayerMask);
        Debug.DrawRay(wallOrigin, wallDir * velocity.x * Time.fixedDeltaTime, Color.red);


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

    private void IncreaseVelocity(Vector2 pos)
    {
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
            CheckIfGrounded(pos);
        }
        CheckIfBlocked(pos);
    }

    private void CheckIfGrounded(Vector2 pos)
    {
        Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
        Vector2 rayDirection = Vector2.down;
        float rayDistance = velocity.y * Time.fixedDeltaTime;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
        if (hit2D.collider == null)
        {

            isGrounded = false;
        }
        Debug.DrawRay(rayOrigin, rayDirection * 100, Color.yellow);
    }

    private void IcreaseDistance()
    {
        if (!isDead)
            distance += velocity.x * Time.fixedDeltaTime;
    }

    private float JumpMovement(Vector2 pos)
    {
        if (!isGrounded)
        {
            HoldingJump();

            pos.y += velocity.y * Time.fixedDeltaTime;

            //La gravetat afecta quan no tapejem
            notHoldingJump();

            //Caiguda collisio vertical
            return GroundHeight(pos);
        }
        return pos.y;
    }

    private void CheckEnemyCollision()
    {
        Vector2 playerOrigin = new Vector2(transform.position.x, transform.position.y - 3.5f);
        Vector2 playerDir = Vector2.right;
        float playerRayDistance = velocity.x * Time.fixedDeltaTime;
        RaycastHit2D playerHitDown = Physics2D.Raycast(playerOrigin, playerDir, playerRayDistance, minionLayerMask);
        RaycastHit2D playerHitMid = Physics2D.Raycast(playerOrigin, playerDir, playerRayDistance, minionLayerMask);
        RaycastHit2D playerHitUp = Physics2D.Raycast(playerOrigin + new Vector2(0, 4), playerDir, playerRayDistance, minionLayerMask);
        Debug.DrawRay(playerOrigin, playerDir * playerRayDistance, Color.green);
        Debug.DrawRay(playerOrigin + new Vector2(0, 2), playerDir * playerRayDistance, Color.green);
        Debug.DrawRay(playerOrigin + new Vector2(0, 4), playerDir * playerRayDistance, Color.green);

        CheckHit(playerHitDown);
        if (playerHitDown.collider == null)
            CheckHit(playerHitMid);
        if (playerHitDown.collider == null && playerHitMid.collider == null)
            CheckHit(playerHitUp);
    }

    private void CheckHit(RaycastHit2D playerHit)
    {
        if (playerHit.collider != null)
        {
            if (playerHit.collider.CompareTag("MedKit"))
            {
                if (health < maxHealth)
                {
                EffectAudioController.PlaySound("heal");
                    health++;
                Destroy(playerHit.collider.gameObject);
                }
            }
            else
            {
                velocity.x -= velocity.x * 0.1f;
                health--;
                Destroy(playerHit.collider.gameObject);
                EffectAudioController.PlaySound("hit");
                StartCoroutine(HurtPlayer());
                CheckifDead();
            }
            //Set hurt animation
        }
    }

    private IEnumerator HurtPlayer()
    {
        hurt = true;
        yield return new WaitForSeconds(0.15f);
        hurt = false;
    }

    private float GroundHeight(Vector2 pos)
    {
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
                    velocity.y = 0;
                    isGrounded = true;
                    return groundHeight;
                }
            }
        }
        return pos.y;
    }

    private void notHoldingJump()
    {
        if (!isHoldingJump)
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
        animator.SetFloat("VelocityY", velocity.y);
    }

    private void HoldingJump()
    {
        if (isHoldingJump)
        {
            holdJumpTimer += Time.fixedDeltaTime;
            if (holdJumpTimer >= maxHoldJumpTime)
            {
                isHoldingJump = false;
            }
        }
    }

    private float MovePlayerInsideCamera(Vector2 pos)
    {
        if (pos.x > playerCamera.transform.position.x - playerCamera.orthographicSize)
        {
            return velocity.x * 0.25f * Time.fixedDeltaTime;
        }
        return 0;
    }

    private void IncreaseCameraSize()
    {
        if (!isDead && isGrounded)
        {
            playerCamera.orthographicSize = initialCameraSize + (velocity.x * 12) / maxXVelocity;
        }
    }

    private void CameraHeight(Vector2 pos)
    {
        if (pos.y >= 25)
        {
            Vector3 cameraPos = playerCamera.transform.position;
            Debug.DrawLine(pos, new Vector2(pos.x * 2, pos.y));
            playerCamera.transform.position = new Vector3(cameraPos.x, pos.y - 10, cameraPos.z);
        }
    }

    private void CheckPlayerDeath()
    {
        if (isDead)
        {
            return;
        }
    }

    private void CheckFallDeath(Vector2 pos)
    {
        if (pos.y < -20)
        {
            isDead = true;
        }
    }

    private void JumpingInput()
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
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }


    private void SetAnimation()
    {
        animator.SetFloat("VelocityY", velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("VelocityX", velocity.x);
        animator.SetBool("hurt", hurt);
    }


    private void CheckifDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            isDead = true;
        }
    }
}
