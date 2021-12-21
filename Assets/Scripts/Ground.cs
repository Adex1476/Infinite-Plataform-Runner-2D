using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;

    public float groundHeight;
    private float groundRight;
    private float screenRight;
    private BoxCollider2D groundCollider;

    bool didGenerateGround = false;

    [SerializeField]
    private GameObject minion;
    [SerializeField]
    private GameObject GreenHealthKit;
    [SerializeField]
    private GameObject redHealthKit;
    
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        groundCollider = GetComponent<BoxCollider2D>(); 
        groundHeight = transform.position.y + (groundCollider.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;
    }
    void Start()
    {
        
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
            pos.x -= MoveGround(pos);

            DestroyGroundIfOut();

            if (!didGenerateGround)
            {
                if (groundRight < screenRight)
                {
                    didGenerateGround = true;
                    GenerateGround();
                }
            }

            transform.position = pos;
        }
    }

    private void DestroyGroundIfOut()
    {
        groundRight = transform.position.x + (groundCollider.size.x / 2);

        if (groundRight < -40)
        {
            Destroy(gameObject);
        }
    }

    private float MoveGround(Vector2 pos)
    {
        return player.velocity.x * Time.fixedDeltaTime;
    }

    void GenerateGround()
    {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        //Maximum high
        float h1 = player.jumpVelocity * player.maxHoldJumpTime / 2;
        float t = player.jumpVelocity / -player.gravity;
        float h2 = player.jumpVelocity * t + (0.5f * (player.gravity * (t * t)));
        float maxJumpHeight = h1 + h2;
        float maxY = maxJumpHeight * 0.3f;
        maxY += groundHeight;
        float minY = 5;
        float actualY = Random.Range(minY, maxY); //- goCollider.size.y / 2;

        pos.y = actualY - goCollider.size.y / 2;
        if (pos.y > 1f)
            pos.y = 1.7f;

        float t1 = t + player.maxHoldJumpTime;
        float t2 = Mathf.Sqrt((4.0f * (maxY - actualY)) / -player.gravity);
        float totalTime = t1 + t2;
        float maxX = totalTime * player.velocity.x;
        maxX *= 0.7f;
        maxX += groundRight;
        float minX = screenRight + 5;
        float actualX = Random.Range(minX, maxX);

        pos.x = actualX + goCollider.size.x / 2;
        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);


        float minionNum = Random.Range(0, 2);
        for (int i=0; i< minionNum; i++)
        {
            GenerateMinion(go, goGround, goCollider, minion);
        }
        float greenHealthNum = Random.Range(0, 2);
        for (int i=0; i< greenHealthNum; i++)
        {
            GenerateMinion(go, goGround, goCollider, GreenHealthKit);
        }
        float redHealthNum = Random.Range(0, 2);
        for (int i=0; i< redHealthNum; i++)
        {
            GenerateMinion(go, goGround, goCollider, redHealthKit);
        }
    }

    private void GenerateMinion(GameObject go, Ground goGround, BoxCollider2D goCollider, GameObject gameObjectToSpawn)
    {
        GameObject goMinion = Instantiate(gameObjectToSpawn);
        float y = goGround.groundHeight - 2.5f;
        float halfWidth = goCollider.size.x / 2 - 1;
        float left = go.transform.position.x - halfWidth;
        float right = go.transform.position.x + halfWidth;
        float x = Random.Range(left, right);
        Vector2 minionPos = new Vector2(x, y);
        goMinion.transform.position = minionPos;
    }
}
