using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundMusic;
    [SerializeField] private AudioClip soundShoot;
    [SerializeField] private AudioClip soundDead;
    [SerializeField] private AudioClip soundMusicGameOver;
    [SerializeField] private AudioClip soundHit;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        GameStart();
    }

    public void PlaySoundDead()
    {
        PlaySound(soundDead);
    }

    public void PlaySoundShoot()
    {
        PlaySound(soundShoot);
    }

    public void PlaySoundMusicGameOver()
    {
        PlaySound(soundMusicGameOver);
    }

    public void PlaySoundHit()
    {
        PlaySound(soundHit);
    }

    public void GameStart()
    {
        PlaySoundMusic();
    }

    private void PlaySoundMusic()
    {
        AudioClip musicClip = soundMusic[UnityEngine.Random.Range(0, soundMusic.Length)];
        PlaySound(musicClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Sound clip is missing");
            return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }
}