using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button muteMusic;
    [SerializeField] private Button muteFx;
    [SerializeField] private Image muteFxImg;
    [SerializeField] private Image muteMusicImg;
    [SerializeField] private GameSound gameSound;
    [SerializeField] private GameObject pauseGO;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        muteFxImg.enabled = gameSound.soundFxMute;
        muteMusicImg.enabled = gameSound.musicMute;
    }

    public void Music() => gameSound.musicMute = !gameSound.musicMute;

    public void SoundFx() => gameSound.soundFxMute = !gameSound.soundFxMute;

    public void Back() => gameObject.SetActive(false);


    
}
