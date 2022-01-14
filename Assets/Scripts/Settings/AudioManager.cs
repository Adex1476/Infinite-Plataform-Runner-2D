using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    AudioSource audioSource;
    public Sprite[] img;
    // Start is called before the first frame update
    [SerializeField] private Image mutear;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mutear= GameObject.Find("Mute").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q)){
            
            audioSource.mute= !audioSource.mute;
            
            
        }
    }

    public void mute()
    {
        
        audioSource.mute= !audioSource.mute;
        if(audioSource.mute==false){
            mutear.sprite= img[0];
        }else{
            mutear.sprite= img[1];
        }
    }
}
