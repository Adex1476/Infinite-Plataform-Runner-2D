using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletManager bulletManager;
    
    public Vector3 clickDirecction;

    private float speed = 25;

    [SerializeField]
    private LayerMask minionLayerMask;
    
    private Camera mainCamera;

    void Awake()
    {
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        clickDirecction = bulletManager.clickDirection;
        clickDirecction = new Vector3(clickDirecction.x, clickDirecction.y, 0);
        mainCamera = Camera.main;
        //transform.rotation = Quaternion.LookRotation(, Vector3.up); ;
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

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, clickDirecction * 10, speed * Time.fixedDeltaTime, minionLayerMask);
        if (raycastHit2D.collider != null)
        {
            Destroy(raycastHit2D.collider.gameObject);
        }
        //= bulletPosition;
    }
}
