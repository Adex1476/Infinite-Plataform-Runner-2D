using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private BulletManager bulletManager;
    
    private Vector3 clickDirecction;

    [SerializeField]
    private float speed = 25;

    [SerializeField]
    private LayerMask minionLayerMask;

    [SerializeField]
    private GameObject player;

    private float screenRight; 
    private float screenTop; 
    private float screenBottom; 
    void Awake()
    {
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        clickDirecction = bulletManager.clickDirection;
        clickDirecction = new Vector3(clickDirecction.x, clickDirecction.y, 0);
        screenRight = Camera.main.transform.position.x * 2;
        screenTop = Camera.main.transform.position.y * 2;
        screenBottom = Camera.main.transform.position.y - Camera.main.transform.position.y;
        // Transforma la rotació de la bullet en funció de la direcció clicada  
        transform.right = clickDirecction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveBullet();

        Debug.DrawRay(transform.position, clickDirecction * speed, Color.green);

        //Vector3 bulletPosition = gameObject.transform.position;
        //Position = Position + vector / magnitud del vector * velocitat
        
        // Col·lisió de la bullet amb el minion
        BulletHit();

        // Bullet dins dels marges de la camera
        DestroyBulletIfOut();
        
    }

    private void DestroyBulletIfOut()
    {
        if (transform.position.x > screenRight + 20 || transform.position.y < screenBottom - 20 || transform.position.y > screenTop + 20)
        {
            Destroy(gameObject);
        }
    }

    private void BulletHit()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, clickDirecction, speed * Time.fixedDeltaTime, minionLayerMask);
        if (raycastHit2D.collider != null)
        {
            Debug.Log(raycastHit2D.collider);
            Destroy(raycastHit2D.collider.gameObject);
            if (raycastHit2D.collider.gameObject)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveBullet()
    {
        Vector2 vel = player.GetComponent<Player>().velocity;
        speed += vel.x;
        gameObject.transform.position += clickDirecction / clickDirecction.magnitude * speed * Time.fixedDeltaTime;
    }
}
