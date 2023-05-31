using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SandStormManager : MonoBehaviour
{
    public static SandStormManager Instance;
    public Material fogMat;
    public float fogDensity = 0.1f;
    public float fadeTime = 1f;
    Coroutine coroutine;
#if UNITY_EDITOR
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            StartSandStorm();
        else if (Input.GetKeyDown(KeyCode.E))
            EndSandStorm();
    }
#endif
    public void StartSandStorm()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(StartSandStormCoroutine());
    }
    public void EndSandStorm()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(EndSandStormCoroutine());
    }
    IEnumerator StartSandStormCoroutine()
    {
        float sumTime = 0;
        while (sumTime < fadeTime)
        {
            sumTime += Time.deltaTime;
            fogMat.SetFloat("_Density", fogDensity * sumTime / fadeTime);
            Debug.Log(fogDensity * sumTime / fadeTime);
            yield return null;
        }
        fogMat.SetFloat("_Density", fogDensity);
    }
    IEnumerator EndSandStormCoroutine()
    {
        float sumTime = fadeTime;
        while (sumTime > 0)
        {
            sumTime -= Time.deltaTime;
            fogMat.SetFloat("_Density", fogDensity * sumTime / fadeTime);
            Debug.Log(fogDensity * sumTime / fadeTime);
            yield return null;
        }
        fogMat.SetFloat("_Density", 0);
    }
}