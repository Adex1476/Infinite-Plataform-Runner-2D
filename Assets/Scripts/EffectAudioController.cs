using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudioController : MonoBehaviour
{
    public static AudioClip healSound, hitSound, playershootSound, shootbossSound, bossdeathSound, miniondeathSound, bosshitSound, click, backClick, gunMetalClick;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        healSound = Resources.Load<AudioClip>("heal");
        hitSound = Resources.Load<AudioClip>("hit");
        playershootSound = Resources.Load<AudioClip>("playershoot");
        shootbossSound = Resources.Load<AudioClip>("shootboss");
        bossdeathSound = Resources.Load<AudioClip>("bossdeath");
        miniondeathSound = Resources.Load<AudioClip>("miniondeath");
        bosshitSound = Resources.Load<AudioClip>("bosshit");

        click = Resources.Load<AudioClip>("click");
        backClick = Resources.Load<AudioClip>("backclick");
        gunMetalClick = Resources.Load<AudioClip>("gunMetalClick");

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
            case "bossdeath":
                audioSrc.PlayOneShot(bossdeathSound);
                break;
            case "miniondeath":
                audioSrc.PlayOneShot(miniondeathSound);
                break;
            case "bosshit":
                audioSrc.PlayOneShot(bosshitSound);
                break;
        }
    }

    public void OnClickSound() => audioSrc.PlayOneShot(click);
    

    public void OnClickBackSound() => audioSrc.PlayOneShot(backClick);


    public void EmptyShootGun() => audioSrc.PlayOneShot(gunMetalClick);
}
