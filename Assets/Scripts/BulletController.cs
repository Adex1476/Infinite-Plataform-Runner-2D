using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletManager bulletManager;
    
    public Vector3 clickDirecction;

    private float speed = 5;

    [SerializeField]
    private LayerMask minionLayerMask;

    private float screenRight; 
    private float screenTop; 
    private float screenBottom; 
    void Awake()
    {
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        clickDirecction = bulletManager.clickDirection;
        clickDirecction = new Vector3(clickDirecction.x, clickDirecction.y, 0);
        screenRight = Camera.main.transform.position.x * 2;
        screenTop = Camera.main.transform.position.y * 2 + 1;
        screenBottom = Camera.main.transform.position.y - Camera.main.transform.position.y - 1;
        // Transforma la rotació de la bullet en funció de la direcció clicada  
        transform.right = clickDirecction;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 bulletPosition = gameObject.transform.position;
        //Position = Position + vector / magnitud del vector * velocitat
        gameObject.transform.position +=  clickDirecction / clickDirecction.magnitude * speed * Time.fixedDeltaTime;
        //
        Debug.DrawRay(transform.position, clickDirecction * 10, Color.green);

        // Col·lisió de la bullet amb el minion
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, clickDirecction * 10, speed * Time.fixedDeltaTime, minionLayerMask);
        if (raycastHit2D.collider != null)
        {
            Destroy(raycastHit2D.collider.gameObject);
        }
        
        // Bullet dins dels marges de la camera
        if (transform.position.x > screenRight || transform.position.y < screenBottom || transform.position.y > screenTop)
        {
            Destroy(gameObject);
        }
    }
}
