using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletManager bulletManager;

    public Vector3 clickDirecction;

    private float speed = 10;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        clickDirecction = bulletManager.clickDirection;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bulletPosition = gameObject.transform.position;
        bulletPosition -= clickDirecction * speed * Time.fixedDeltaTime; 
        //

        gameObject.transform.position = bulletPosition;
    }
}
