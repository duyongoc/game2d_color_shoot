using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{


    [Header("Setting")]
    [SerializeField] private SOSoundConfig config;
    [SerializeField] private Soundy soundyPrefab;

    [Header("Audio")]
    [SerializeField] private Transform musicAudio;
    [SerializeField] private Transform sfxAudio;


    //= private
    private static AudioSource audioMusic;
    private static AudioSource audioSFX;


    // music
    public static AudioClip MUSIC_BACKGROUND;
    public static AudioClip MUSIC_MENU;

    // sfx
    public static AudioClip SFX_SHOOT;
    public static AudioClip SFX_SHOOT_HIT;
    public static AudioClip SFX_PASS_OBSTACLE;

    public static AudioClip SFX_GAMEOVER;



    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    // private void Update()
    // {
    // }
    #endregion


    public static void PlayMusic(AudioClip audi, bool loop = true)
    {
        audioMusic.clip = audi;
        audioMusic.loop = loop;
        audioMusic.Play();
    }

    public static void StopMusic()
    {
        audioMusic.Stop();
    }

    public void PlaySFX(AudioClip audi)
    {
        var sound = Instantiate(soundyPrefab, audioSFX.transform);
        sound.Play(audi);
    }


    public static void StopSFX()
    {
        audioSFX.Stop();
        audioSFX.clip = null;
    }

    public static void StopSFX(AudioClip clip)
    {
        audioSFX.Stop();
    }

    public static void PlaySFXOneShot(AudioClip clip)
    {
        audioSFX.PlayOneShot(clip);
    }

    public static void PlaySFXBlend(AudioClip clip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(clip);
    }

    public static bool MusicPlaying(AudioClip audi)
    {
        return audioMusic.clip == audi && audioMusic.isPlaying;
    }

    public static bool SFXPlaying(AudioClip audi)
    {
        return audioSFX.clip == audi && audioSFX.isPlaying;
    }


    private void CacheDefine()
    {
        MUSIC_BACKGROUND = config.MUSIC_BACKGROUND;
        MUSIC_MENU = config.MUSIC_MENU;

        // sfx
        SFX_SHOOT = config.SFX_SHOOT;
        SFX_SHOOT_HIT = config.SFX_SHOOT_HIT;
        SFX_PASS_OBSTACLE = config.SFX_PASS_OBSTACLE;

        SFX_GAMEOVER = config.SFX_GAMEOVER;
    }

    private void CacheComponent()
    {
        audioMusic = musicAudio.GetComponent<AudioSource>();
        audioSFX = sfxAudio.GetComponent<AudioSource>();
    }
}
