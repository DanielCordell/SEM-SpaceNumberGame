using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // how many time left
    public float TimerCurrent;
    public float TimerStart;
    public float TimerPercent;
    public AudioSource CountdownSound;
    private bool hasPlayedSound;
    private Image timerBox;
    SceneHandler sceneHandler;

    void Start()
    {
        InitialiseTimer();
        TimerCurrent = TimerStart;
    }


    void InitialiseTimer()
    {
        TimerStart = 60f;
        timerBox = GetComponent<Image>();
        CountdownSound = timerBox.GetComponent<AudioSource>();
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
    }
   

    void Update()
    {
        // count time
        TimerCurrent -= Time.deltaTime;
        TimerPercent = TimerCurrent / TimerStart;
        timerBox.fillAmount = TimerPercent;
        if (TimerCurrent < CountdownSound.clip.length && !hasPlayedSound)
        {
            hasPlayedSound = true;
            CountdownSound.Play();
        }
        if (CountdownSound.time == CountdownSound.clip.length)
            sceneHandler.GoGameOverScene();
    }
}