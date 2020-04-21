﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static AudioSource audioSource;
    public static List<AudioClip> trackList = new List<AudioClip>();

    // Used to set up the music player.
    public static void Initialize()
    {
        Object[] trackListArray = Resources.LoadAll("Music", typeof(AudioClip));
        foreach (var c in trackListArray)
        {
            Debug.Log("Added " + c.name + " to the Track List.");
            trackList.Add(c as AudioClip);
        }

        audioSource = new GameObject("Music Player").AddComponent<AudioSource>();

    }

    #region PlayTrack Methods

    // Plays an AudioClip based on an AudioClip.
    public static void Play(AudioClip c)
    {
        try
        {
            audioSource.clip = c;
            audioSource.Play();
        }
        catch
        {
            Debug.LogError("The Music Player does not contain a track with the AudioClip of " + c + ".\nPerhaps you need to add it to the Tracklist or use another clip?");
        }
    }

    // Plays an AudioClip based on an index of the trackList array.
    public static void Play(int i)
    {
        try
        {
            audioSource.clip = trackList[i];
            audioSource.Play();
        }
        catch
        {
            Debug.LogError("The Music Player does not contain a track at index " + i + ".\nPerhaps you need to add it to the Tracklist or use another clip?");
        }
    }
    
    // Plays an AudioClip based on a name as a string.
    public static void Play(string s)
    {
        try
        {
            foreach (AudioClip c in trackList)
            {
                if (c.name == s)
                {
                    audioSource.clip = c;
                    audioSource.Play();

                    break;
                }
            }
        }
        catch
        {
            Debug.LogError("The Music Player does not contain a track named " + s + "./nPerhaps you need to add it to the Tracklist or use another clip?");
        }
    }

    #endregion

    #region Return Track AudioClips

    // Returns an AudioClip based on a name.
    public static AudioClip Track(string s)
    {
        foreach (AudioClip c in trackList)
        {
            if (c.name == s)
                return c;
        }

        return null;
    }
    
    // Returns an AudioClip based on an int.
    public static AudioClip Track(int i)
    {
        try
        {
            return trackList[i];
        }
        catch
        {
            Debug.LogError("There is no track at index " + i + " of Music Player.");
        }

        return null;
    }

    #endregion

    // Returns the AudioClip that's playing.
    public static AudioClip CurrentTrack()
    {
        if (audioSource.isPlaying)
            return audioSource.clip;

        return null;
    }

    // Returns whether there's anything playing.
    public static bool isPlaying()
    {
        return audioSource.isPlaying;
    }
}
