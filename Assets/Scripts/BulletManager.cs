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

    private bool canShoot = true;

    [SerializeField]
    private float shootCDTime;

    [SerializeField]
    private Animator bulletUIAnimator;

    [SerializeField]
    private int totalLoad = 6;
    [SerializeField]
    private int bulletIndex;


    void Awake()
    {
        //playerPosition = player.transform.position;
        //Instantiate(bullet, playerPosition, Quaternion.identity);
        dataPlayer = player.GetComponent<Player>();
        totalLoad = 6;
        bulletIndex = totalLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dataPlayer.isDead)
        {
            playerPosition = player.transform.position;

            if (Input.GetKey(KeyCode.Mouse0))
            {                 

                clickDirection = ClickedDirection();

                if (clickDirection.x > 0)
                {
                    if (canShoot)
                    {
                        bulletIndex--;
                        PullTrigger();
                        StartCoroutine(ShootColdown());

                    }
                    //player.GetComponentInParent<Animator>().SetBool("shooting", true);
                }
            }
        }  
    }

    private IEnumerator ReloadCooldown()
    {
        bulletUIAnimator.SetBool("Reloading", true); 
        bulletIndex = 6;
        yield return new WaitForSeconds(shootCDTime);
        bulletUIAnimator.SetBool("Reloading", false);
        canShoot = true;
    }

    IEnumerator ShootColdown()
    {
        canShoot = false;
        bulletUIAnimator.SetInteger("bulletIndex", bulletIndex);
        yield return new WaitForSeconds(shootCDTime);
        if (bulletIndex > 0)
        {
            canShoot = true;
        }
        else if (bulletIndex <= 0)
        {
            StartCoroutine(ReloadCooldown());
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
