using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicShuffler : MonoBehaviour
{
    AudioSource audioSource;

    AudioClip[] audioClips = new AudioClip[2];
    int currentAudioIndex;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClips[0] = Resources.Load<AudioClip>("Audio/Music/Background_Game1 - (Dream about space)");
        audioClips[1] = Resources.Load<AudioClip>("Audio/Music/Background_Game2 - (I didn‘t do it)");
        currentAudioIndex = Random.Range(0, 2);
        Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentAudioIndex = currentAudioIndex == 1 ? 0 : 1;
            Play();
        }
    }

    void Play()
    {
        audioSource.clip = audioClips[currentAudioIndex];
        audioSource.Play();
    }
}
