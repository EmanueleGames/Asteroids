using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton pattern
    public static AudioManager instance;

    // Game music and sfx Tags
    public const string DEEPSPACE_NOISE_SOUND = "deepspace_noise";
    public const string BOOST_SFX = "boost";
    public const string SHOOT_SFX = "shoot";
    public const string EXPLOSION_SFX = "explosion";

    private bool _gameMuted = false;

    // Sound effects array
    public SoundEffect[] sounds;

    // Sets and Gets
    public bool GameMuted()
    { return _gameMuted; }
    public void ToggleSound(bool gameMuted)
    {
        _gameMuted = gameMuted;
        AudioListener.volume = gameMuted ? 0 : 1;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (SoundEffect sfx in sounds)
        {
            sfx.source = gameObject.AddComponent<AudioSource>();
            sfx.source.clip = sfx.clip;
            sfx.source.volume = sfx.volume;
            sfx.source.pitch = sfx.pitch;
            sfx.source.loop = sfx.loop;
            sfx.source.spatialBlend = sfx.spatialBlend;
        }
    }

    public void Play(string name)
    {
        SoundEffect sound = Array.Find(sounds, sfx => sfx.name == name);
        if (sound == null)
        {
            Debug.Log("Sound effect " + name + " not found!");
            return;
        }
        sound.source.Play();
    }

    public void Stop(string name)
    {
        SoundEffect sound = Array.Find(sounds, sfx => sfx.name == name);
        if (sound == null)
        {
            Debug.Log("Sound effect " + name + " not found!");
            return;
        }
        if (sound.source.isPlaying)
            sound.source.Stop();

    }

    public bool IsPlaying(string name)
    {
        SoundEffect sound = Array.Find(sounds, sfx => sfx.name == name);
        if (sound == null)
        {
            Debug.Log("Sound effect " + name + " not found!");
            return false;
        }
        else
        {
            return sound.source.isPlaying;
        }
    }

    public void SetVolume(string name, float new_volume)
    {
        SoundEffect sound = Array.Find(sounds, sfx => sfx.name == name);
        if (sound == null)
        {
            Debug.Log("Sound effect " + name + " not found!");
            return;
        }
        if (new_volume < 0f)
            new_volume = 0f;
        if (new_volume > 1f)
            new_volume = 1f;

        sound.source.volume = new_volume;
    }

    public void SetPitch(string name, float new_pitch)
    {
        SoundEffect sound = Array.Find(sounds, sfx => sfx.name == name);
        if (sound == null)
        {
            Debug.Log("Sound effect " + name + " not found!");
            return;
        }
        if (new_pitch < 0.1f)
            new_pitch = 0.1f;
        if (new_pitch > 3f)
            new_pitch = 3f;

        sound.source.pitch = new_pitch;
    }
}
