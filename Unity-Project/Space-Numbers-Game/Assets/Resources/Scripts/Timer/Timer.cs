using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // how many time left
    public float currentTimer;
    public float startTimer;
    public float timerPercent;
    public AudioSource countdown;
    private bool hasPlayedSound;
    private Image timerBox;

    void Start()
    {
        InitialiseTimer();
        currentTimer = startTimer;
    }


    void InitialiseTimer()
    {
        startTimer = 60f;
        timerBox = GetComponent<Image>();
        countdown = timerBox.GetComponent<AudioSource>();
        
    }
   

    void Update()
    {
        // count time
        currentTimer -= Time.deltaTime;
        timerPercent = currentTimer / startTimer;
        timerBox.fillAmount = timerPercent;
        if (currentTimer < countdown.clip.length && !hasPlayedSound)
        {
            hasPlayedSound = true;
            countdown.Play();
        }
    }
}