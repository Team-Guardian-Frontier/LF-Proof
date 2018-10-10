
using UnityEngine.Audio;
using System;
using UnityEngine;

/*
 * source tutorial
 * https://www.youtube.com/watch?v=6OT43pvUyfY
 */

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    //instance checker
    public static AudioManager instance;
    private bool startedLoop;

	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            //Initialize each sound with an AudioSource component.
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
	}

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not Found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not Found!");
            return;
        }
        s.source.Stop();
    }

    void Start()
    {
        Play("LevelIntro");
        startedLoop = false;
        
    }

    
    void FixedUpdate () {
        if (!sounds[1].source.isPlaying && !startedLoop)
        {
            sounds[0].source.Play();
            Debug.Log("Done playing");
            startedLoop = true;
        }
    }
}
