using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    // Audio players components.
    public AudioSource effectsSource;
    public AudioSource sustainSource;
    public AudioSource musicSource;

    // Random pitch adjustment range.
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    public static SoundsManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip, bool sustain = false)
    {
        if (sustain)
        {
            effectsSource.clip = clip;
            effectsSource.Play();
        }
        else
        {
            sustainSource.clip = clip;
            sustainSource.Play();
        }
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        effectsSource.pitch = randomPitch;
        effectsSource.clip = clips[randomIndex];
        effectsSource.Play();
    }

    public void ToggleSFX()
    {
        effectsSource.mute = !effectsSource.mute;
        sustainSource.mute = !sustainSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

}
