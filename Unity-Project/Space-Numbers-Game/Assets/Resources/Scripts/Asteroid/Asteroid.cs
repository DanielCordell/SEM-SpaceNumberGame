﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    public bool Selected = false;
    public int Value;
    GameObject crosshair;
    AudioSource audioSource;
    AudioClip destroySound;
    AudioClip selectSound;
    AudioClip cantSelect;
    Animator animator;
    Question question;
    
    // Start is called before the first frame update
    void Start()
    {
        Value = int.Parse(gameObject.transform.Find("NoRotation/Canvas/Text").gameObject.GetComponent<Text>().text);
        audioSource = GetComponent<AudioSource>();
        selectSound = Resources.Load("Audio/Sound/select") as AudioClip;
        destroySound = Resources.Load("Audio/Sound/explode") as AudioClip;
        cantSelect = Resources.Load("Audio/Sound/cant_select") as AudioClip;
        crosshair = transform.Find("NoRotation/Crosshair").gameObject;
        crosshair.SetActive(Selected);
        animator = gameObject.transform.Find("NoRotation/Explosion").gameObject.GetComponent<Animator>();
        question = GameObject.Find("Question").gameObject.GetComponent<Question>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log(Selected);
        if (!Selected)
        {
            if (question.FillBlank(Value))
            {           
                Selected =! Selected;
                crosshair.SetActive(Selected);
                audioSource.PlayOneShot(selectSound);
            }
            else
            {
                audioSource.PlayOneShot(cantSelect);
            }
        } 
        else
        {
            question.ClearBlank(Value);
            Selected =! Selected;
            crosshair.SetActive(Selected);
            audioSource.PlayOneShot(selectSound);
        }
    }

    public void Explode()
    {
        animator.SetBool("Exploding", true);
        audioSource.PlayOneShot(destroySound);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject.transform.Find("NoRotation/Canvas").gameObject);
        Destroy(gameObject.transform.Find("NoRotation/Crosshair").gameObject);
        Invoke("StopExplosion",0.13f);
        Destroy(gameObject, 1f);
    }

    void StopExplosion()
    {
        animator.SetBool("Exploding", false);
    }
}
