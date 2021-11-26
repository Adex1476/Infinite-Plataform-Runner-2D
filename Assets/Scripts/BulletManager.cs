using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bullet;

    public GameObject player;
    
    public Player dataPlayer;
    
    public Vector3 clickPosition;
    
    public Vector3 clickDirection;
    
    private Vector3 playerPosition;

    void Awake()
    {
        //playerPosition = player.transform.position;
        //Instantiate(bullet, playerPosition, Quaternion.identity);
        dataPlayer = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
/*        clickDirection = new Vector3(0, 0, 0);
*/        if (!dataPlayer.isDead)
        {
            playerPosition = player.transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                //Point = camera Screen Point
                var screenPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                clickPosition = new Vector3(screenPoint.origin.x, screenPoint.origin.y, screenPoint.origin.z);
               
                clickDirection = (clickPosition - playerPosition).normalized;

                if (clickDirection.x > 0)
                {
                    Instantiate(bullet, playerPosition, Quaternion.identity);
                    //player.GetComponentInParent<Animator>().SetBool("shooting", true);
                }
            }
        }  
    }
}
