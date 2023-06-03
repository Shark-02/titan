using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SandStormManager : MonoBehaviour
{
    public string targetRenderFeature = "FogFeature";
    public Material fogMat;
    public float fogDensity = 0.1f;
    Coroutine coroutine;
    public void StartSandStorm(float fadeTime)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(StartSandStormCoroutine(fadeTime));
    }
    public void EndSandStorm(float fadeTime)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(EndSandStormCoroutine(fadeTime));
    }
    IEnumerator StartSandStormCoroutine(float fadeTime)
    {
        RenderFeatureManager.Instance.EnableRenderFeature(targetRenderFeature);
        float sumTime = 0;
        while (sumTime < fadeTime)
        {
            sumTime += Time.deltaTime;
            fogMat.SetFloat("_Density", fogDensity * sumTime / fadeTime);
            //Debug.Log(fogDensity * sumTime / fadeTime);
            yield return null;
        }
        fogMat.SetFloat("_Density", fogDensity);
    }
    IEnumerator EndSandStormCoroutine(float fadeTime)
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
        RenderFeatureManager.Instance.DisableRenderFeature(targetRenderFeature);
    }
}