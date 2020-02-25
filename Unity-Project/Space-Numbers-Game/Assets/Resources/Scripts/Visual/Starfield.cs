using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Starfield : MonoBehaviour
{
	public int starCount = 100;
	public float starSize = 0.1f;
	public float starSizeRange = 0.5f;
	public float starSpeed = 0.5f;

	public float starFieldWidth = 20f;
	public float starFieldHeight = 25f;
	public bool colorise = false;

	private float xOffset;
	private float yOffset;

	ParticleSystem particles;
	ParticleSystem.Particle[] stars;
	Transform cameraTransform;


	void Awake()
	{
		stars = new ParticleSystem.Particle[starCount];
		particles = GetComponent<ParticleSystem>();
		cameraTransform = GameObject.Find("Main Camera").transform;

		xOffset = starFieldWidth * 0.5f;
		yOffset = starFieldHeight * 0.5f;

		for (int i = 0; i < starCount; i++)
		{
			float randSize = Random.Range(starSizeRange, starSizeRange + 1f);
			float scaledColor = colorise ? randSize - starSizeRange : 1f;

			stars[i].position = GetRandomInRectangle(starFieldWidth, starFieldHeight) + transform.position;
			stars[i].startSize = starSize * randSize;
			stars[i].startColor = new Color(1f, scaledColor, scaledColor, 1f);
		}
		particles.SetParticles(stars, stars.Length);
	}

	void Update()
	{
		for (int i = 0; i < stars.Length; i++) 
		{
			Vector3 pos = stars[i].position + transform.position;
			pos += Vector3.down * starSpeed * Time.deltaTime;
			if		(pos.y < (cameraTransform.position.y - yOffset)) pos.y += starFieldHeight;
			else if (pos.y > (cameraTransform.position.y + yOffset)) pos.y -= starFieldHeight;
			stars[i].position = pos - transform.position;
		}
		particles.SetParticles(stars, stars.Length);
	}


	Vector3 GetRandomInRectangle(float width, float height)
	{
		float x = Random.Range(0, width);
		float y = Random.Range(0, height);
		return new Vector3(x - xOffset, y - yOffset, 0);
	}
}
