using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderseaController : MonoBehaviour
{
    public string targetRenderFeature = "UnderseaFeature";
    private void OnEnable()
    {
        RenderFeatureManager.Instance.EnableRenderFeature(targetRenderFeature);
    }
    private void OnDisable()
    {
        RenderFeatureManager.Instance.DisableRenderFeature(targetRenderFeature);
    }
}