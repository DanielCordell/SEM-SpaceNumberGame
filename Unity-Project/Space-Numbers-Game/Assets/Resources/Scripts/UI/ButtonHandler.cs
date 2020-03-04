using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    ParticleSystem[] particleSystems;
    MoveCamera moveCamera;

    public void Start()
    {
        particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        moveCamera = Camera.main.GetComponent<MoveCamera>();
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
        moveCamera.SetMoveTo(t.position);
    }
}
