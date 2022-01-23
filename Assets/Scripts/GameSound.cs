using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Sound Data")]
public class GameSound : ScriptableObject
{
    public bool soundFxMute;
    public bool musicMute;
    public float musicVolume;
    public float soundFxVolume;
}
