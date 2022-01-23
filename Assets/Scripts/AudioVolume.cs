using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] private GameSound gameSound;
    [SerializeField] private Slider sliderMusicVol;
    [SerializeField] private Slider sliderSoundFxVol;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundFxSource;
    // Start is called before the first frame update
    void Start()
    {
        sliderMusicVol.value = gameSound.musicVolume;
        sliderSoundFxVol.value = gameSound.soundFxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        gameSound.musicVolume = sliderMusicVol.value;
        gameSound.soundFxVolume = sliderSoundFxVol.value;

        musicSource.mute = gameSound.musicMute;
        soundFxSource.mute = gameSound.soundFxMute;

        musicSource.volume = gameSound.musicVolume;
        soundFxSource.volume = gameSound.soundFxVolume;
    }
}
