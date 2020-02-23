using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public bool Selected = false;
    public int Value;
    GameObject crosshair;
    AudioSource selectSound;
    AudioSource destroySound;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        selectSound = GetComponent<AudioSource>();
        crosshair = transform.Find("NoRotation/Crosshair").gameObject;
        crosshair.SetActive(Selected);
        animator = gameObject.transform.Find("NoRotation/Explosion").gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        Selected =! Selected;
        crosshair.SetActive(Selected);
        selectSound.Play(0);
    }

    public void Explode()
    {
        animator.SetBool("Exploding", true);
        Destroy(gameObject, 0.32f);
    }
}
