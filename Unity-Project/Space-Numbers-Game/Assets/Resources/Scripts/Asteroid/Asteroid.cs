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
    Animator animator;
    Question question;
    
    // Start is called before the first frame update
    void Start()
    {
        Value = int.Parse(gameObject.transform.Find("NoRotation/Canvas/Text").gameObject.GetComponent<Text>().text);
        audioSource = GetComponent<AudioSource>();
        selectSound = Resources.Load("Audio/Sound/select") as AudioClip;
        destroySound = Resources.Load("Audio/Sound/explode") as AudioClip;
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
        Selected =! Selected;
        crosshair.SetActive(Selected);
        audioSource.PlayOneShot(selectSound);

        if (Selected)
        {
            question.FillBlank(Value);
        } 
        else
        {
            question.ClearBlank(Value);
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
