using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableTexture : MonoBehaviour
{

    public float horizontalScrollSpeed = 0.05f;
    public float verticalScrollSpeed = 0.05f;


    private Renderer r;

    public void Start()
    {
        r = GetComponent<Renderer>();
    }

    public void FixedUpdate()
    {
        float verticalOffset = Time.time * verticalScrollSpeed;
        float horizontalOffset = Time.time * horizontalScrollSpeed;
        r.material.mainTextureOffset = new Vector2(horizontalOffset, verticalOffset);
    }
}
