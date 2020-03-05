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
            //TODO jump to game over scene
            Debug.Log("Once the timer is 0, jump to game over scene");
        }
    }
}