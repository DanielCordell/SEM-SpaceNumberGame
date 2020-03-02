using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    ParticleSystem[] particleSystems;
    public void Start()
    {
        particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    public void StartParticles()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.enableEmission = true;
        }
    }

    public void StopParticles()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.enableEmission = false;
        }
    }

    public void MoveCamera(Transform t)
    {
        Camera.main.transform.position = t.position;
    }
}
