using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParticlePlane : MonoBehaviour
{
    public LightParticleManager particleManager;
    private void OnTriggerEnter(Collider other)
    {
        particleManager.StartLightParticle();
        Debug.Log("StartedLightParticle");
    }
}
