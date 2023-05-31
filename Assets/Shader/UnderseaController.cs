using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderseaController : MonoBehaviour
{
    private void OnEnable()
    {
        RenderFeatureManager.Instance.EnableRenderFeature("UnderseaFeature");
    }
    private void OnDisable()
    {
        RenderFeatureManager.Instance.DisableRenderFeature("UnderseaFeature");
    }
}