using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button mute;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        mute.onClick.AddListener(()=>Mute());
    }

    void Mute(){
        Debug.Log("mute");
        
    }
    
}
