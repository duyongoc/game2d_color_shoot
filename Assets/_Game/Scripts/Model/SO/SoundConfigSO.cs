using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Setting/Sound")]
public class SoundConfigSO : ScriptableObject
{

    [Header("Music")]
    public AudioClip MUSIC_BACKGROUND;
    public AudioClip MUSIC_MENU;

    [Header("SFX", order = 1)]
    [Space(10)]
    public AudioClip SFX_SHOOT;
    public AudioClip SFX_SHOOT_HIT;
    public AudioClip SFX_PASS_OBSTACLE;

    [Space(10)]
    public AudioClip SFX_GAMEOVER;



}
