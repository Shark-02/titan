using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem targetParticle;
    ParticleSystem.EmissionModule emission;
    private void Awake()
    {
        emission = targetParticle.emission;
    }
    public void StartLightParticle()
    {
        emission.enabled = true;
        Debug.Log("EmissionEnabled");
    }
    public void StopLightParticle()
    {
        emission.enabled = false;
    }
}
