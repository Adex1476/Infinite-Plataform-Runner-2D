using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private int contador;
    private int colorType;

    [SerializeField]
    private int minRange = 200;
    [SerializeField]
    private int maxRange = 205;

    void Start()
    {
       player = GameObject.Find("Player").GetComponent<Player>();
       contador = 1;
       colorType = 0;
       GetComponent<Colorblind>().Type = colorType; 
    }

    // Update is called once per frame
    void Update()
    {     
        if(player.distance/contador > minRange && player.distance/contador < maxRange){
            contador++;
            colorType++;
            colorType = (colorType > 3) ? 0 : colorType;
            GetComponent<Colorblind>().Type = colorType; 
        }
    }

}
