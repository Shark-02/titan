using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
    public string targetRenderFeature = "BlurFeature";
    public Material blurMat;
    public void StartBlur()
    {
        RenderFeatureManager.Instance.EnableRenderFeature(targetRenderFeature);
    }
    public void StopBlur()
    {
        RenderFeatureManager.Instance.DisableRenderFeature(targetRenderFeature);
    }
}