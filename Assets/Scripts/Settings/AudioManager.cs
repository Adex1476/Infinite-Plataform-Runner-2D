using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    AudioSource audioSourceMusic;
    AudioSource audioSourceFX;
    public GameSound gameSound;
    public Sprite[] img;
    // Start is called before the first frame update
    [SerializeField] private Image mutear;


    void Start()
    {
        audioSourceMusic = GetComponent<AudioSource>();
        audioSourceFX = GameObject.Find("AudioEffectManager").GetComponent<AudioSource>();
        mutear= GameObject.Find("Mute").GetComponent<Image>();

        audioSourceFX.mute = gameSound.soundFx;
        audioSourceMusic.mute = gameSound.music;
        ChangeImage();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q)){
            mute();
        }
    }

    public void mute()
    {
        gameSound.soundFx = !gameSound.soundFx;
        gameSound.music = !gameSound.music;

        audioSourceMusic.mute = gameSound.music;
        audioSourceFX.mute = gameSound.soundFx;
        ChangeImage();
    }


    public void ChangeImage()
    {
        if (audioSourceMusic.mute == false && audioSourceFX.mute == false)
        {
            mutear.sprite = img[0];
        }
        else
        {
            mutear.sprite = img[1];
        }
    }
}
