using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource audioSourceMusic;
    [SerializeField] AudioSource audioSourceFX;
    [SerializeField] GameSound gameSound;
    [SerializeField] Sprite[] img;
    [SerializeField] private Image mutear;
    [SerializeField] private Slider musicVolume;
    
    void Start()
    {
        audioSourceFX.mute = gameSound.soundFxMute;
        audioSourceMusic.mute = gameSound.musicMute;
        ChangeImage();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            mute();
        }

        audioSourceMusic.mute = gameSound.musicMute;
        audioSourceFX.mute = gameSound.soundFxMute;

        if (musicVolume != null)
            Debug.Log(musicVolume.value);

        ChangeImage();  
    }

    public void mute()
    {
        gameSound.soundFxMute = !gameSound.soundFxMute;
        gameSound.musicMute = !gameSound.musicMute;

        
        ChangeImage();
    }


    public void ChangeImage()
    {
        if (audioSourceMusic.mute && audioSourceFX.mute)
        {
            mutear.sprite = img[1];
        }
        else
        {
            mutear.sprite = img[0];
        }
    }
}
