using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Setting/Sound")]
public class SOSoundConfig : ScriptableObject
{

    [Header("Music")]
    public AudioClip MUSIC_BACKGROUND;
    public AudioClip MUSIC_MENU;


    [Header("SFX")]
    public AudioClip SFX_SHOOT;
    public AudioClip SFX_SHOOT_HIT;
    public AudioClip SFX_PASS_OBSTACLE;
    public AudioClip SFX_GAMEOVER;



}
