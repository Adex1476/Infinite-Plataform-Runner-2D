using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject player;
    
    [SerializeField]
    private Player dataPlayer;

    [SerializeField]
    private Vector2 clickPosition;
    
    public Vector2 clickDirection;
    
    private Vector2 playerPosition;

    void Awake()
    {
        //playerPosition = player.transform.position;
        //Instantiate(bullet, playerPosition, Quaternion.identity);
        dataPlayer = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dataPlayer.isDead)
        {
            playerPosition = player.transform.position;

            if (Input.GetMouseButtonDown(0))
            {                 
                clickDirection = ClickedDirection();

                if (clickDirection.x > 0)
                {
                    PullTrigger();
                    //player.GetComponentInParent<Animator>().SetBool("shooting", true);
                }
            }
        }  
    }


    private void PullTrigger() => Instantiate(bullet, playerPosition, Quaternion.identity);


    private Vector2 ClickedDirection()
    {
        var screenPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

        clickPosition = new Vector2(screenPoint.origin.x, screenPoint.origin.y);

        return (clickPosition - playerPosition).normalized;
    }


}
