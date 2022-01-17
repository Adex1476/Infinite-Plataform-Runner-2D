using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    AudioSource audioSourceMusic;
    AudioSource audioSourceFX;

    public Sprite[] img;
    // Start is called before the first frame update
    [SerializeField] private Image mutear;
    void Start()
    {
        audioSourceMusic = GetComponent<AudioSource>();
        audioSourceFX = GameObject.Find("AudioEffectManager").GetComponent<AudioSource>();
        mutear= GameObject.Find("Mute").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q)){
            
            audioSourceMusic.mute= !audioSourceMusic.mute;
            audioSourceFX.mute=!audioSourceFX.mute;
            
        }
    }

    public void mute()
    {
        
        audioSourceMusic.mute= !audioSourceMusic.mute;
        audioSourceFX.mute=!audioSourceFX.mute;
        if(audioSourceMusic.mute==false){
            mutear.sprite= img[0];
        }else{
            mutear.sprite= img[1];
        }
    }
}
