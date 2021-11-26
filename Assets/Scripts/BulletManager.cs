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
    //public float speed=2;
    // Start is called before the first frame update
    void Start()
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
            //playerPosition.y -= 3;

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, playerPosition, Quaternion.identity);
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                clickPosition = new Vector3(ray.origin.x, ray.origin.y, ray.origin.z);
                clickDirection = (clickPosition - playerPosition).normalized;
                Debug.Log(clickDirection);

            }
        }
        //Vector3 bulletPosition = transform.position;
        
        //bullet.transform.position = Vector3.MoveTowards(playerPosition,clickPosition,speed * Time.fixedDeltaTime);

/*        if(playerPosition == clickPosition){
            Destroy(bullet);
        }*/
        
    }
   
}
