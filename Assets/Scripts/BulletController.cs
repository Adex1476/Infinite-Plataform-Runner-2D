using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletManager bulletManager;
    
    public Vector3 clickDirecction;

    private float speed = 5;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        clickDirecction = bulletManager.clickDirection;
        clickDirecction = new Vector3(clickDirecction.x, clickDirecction.y, 0);
        mainCamera = Camera.main;
        //transform.rotation = Quaternion.LookRotation(, Vector3.up); ;
        //transform.rotation = clickDirecction;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 bulletPosition = gameObject.transform.position;
        gameObject.transform.position +=  clickDirecction * speed * Time.fixedDeltaTime; 
        //

         //= bulletPosition;
    }
}
