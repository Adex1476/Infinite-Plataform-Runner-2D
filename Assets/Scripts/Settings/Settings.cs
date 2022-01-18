using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Button muteMusic;
    [SerializeField] private Button muteFx;
    [SerializeField] private Image muteFxImg;
    [SerializeField] private Image muteMusicImg;
    public GameSound gameSound;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        muteFxImg.enabled = gameSound.soundFx;
        muteMusicImg.enabled = gameSound.music;
    }

    public void Music() => gameSound.music = !gameSound.music;
    public void SoundFx() => gameSound.soundFx = !gameSound.soundFx;

    
}
