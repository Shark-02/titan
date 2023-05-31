using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RenderFeatureManager : MonoBehaviour
{
    [SerializeField] UniversalRendererData universalRendererData;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            EnableRenderFeature("FogFeature");
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            DisableRenderFeature("FogFeature");
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            EnableRenderFeature("BlurFeature");
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            DisableRenderFeature("BlurFeature");
        }
    }
    public void EnableRenderFeature(string featureName)
    {
        foreach(var renderFeature in universalRendererData.rendererFeatures)
        {
            if(featureName==renderFeature.name)
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
}
