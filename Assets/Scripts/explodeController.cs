using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        // 获取对粒子系统的引用
        particleSystem = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
    }
}
