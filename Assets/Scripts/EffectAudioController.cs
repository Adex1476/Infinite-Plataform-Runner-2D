using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudioController : MonoBehaviour
{
    public static AudioClip healSound, hitSound, playershootSound, shootbossSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        healSound = Resources.Load<AudioClip>("heal");
        hitSound = Resources.Load<AudioClip>("hit");
        playershootSound = Resources.Load<AudioClip>("playershoot");
        shootbossSound = Resources.Load<AudioClip>("shootboss");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "heal":
                audioSrc.PlayOneShot(healSound);
                break;
            case "hit":
                audioSrc.PlayOneShot(hitSound);
                break;
            case "playershoot":
                audioSrc.PlayOneShot(playershootSound);
                break;
            case "shootboss":
                audioSrc.PlayOneShot(shootbossSound);
                break;
        }
    }
}
