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
    Animator explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        selectSound = GetComponent<AudioSource>();
        crosshair = transform.Find("NoRotation/Crosshair").gameObject;
        crosshair.SetActive(Selected);
        explosion = transform.Find("NoRotation").gameObject.GetComponent<Animator>();
        explosion.SetTrigger("AsteroidExplosion");
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

    public void Detroy()
    {

    }
}
