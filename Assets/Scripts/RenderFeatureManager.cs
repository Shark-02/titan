using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[DefaultExecutionOrder(-100)]
public class RenderFeatureManager : MonoBehaviour
{
    static RenderFeatureManager instance;
    public static RenderFeatureManager Instance => instance;
    [SerializeField] UniversalRendererData universalRendererData;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void EnableRenderFeature(string featureName)
    {
        foreach (var renderFeature in universalRendererData.rendererFeatures)
        {
            if (featureName == renderFeature.name)
            {
                renderFeature.SetActive(true);
            }
        }
    }
    public void DisableRenderFeature(string featureName)
    {
        foreach (var renderFeature in universalRendererData.rendererFeatures)
        {
            if (featureName == renderFeature.name)
            {
                renderFeature.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        foreach (var renderFeature in universalRendererData.rendererFeatures)
            renderFeature.SetActive(false);
    }
}