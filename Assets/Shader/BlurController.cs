using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
    public string targetRenderFeature = "BlurFeature";
    public Material blurMat;
    public void StartBlur()
    {
        StartCoroutine("PingPong");
    }
    public void StopBlur()
    {
        StartCoroutine("StopPingPong");
    }
    IEnumerator PingPong()
    {
        RenderFeatureManager.Instance.EnableRenderFeature(targetRenderFeature);
        float sumTime = 0;
        while(true)
        {
            blurMat.SetFloat("_BlurSize", Mathf.PingPong(sumTime, 1));
            yield return null;
        }
    }
    IEnumerator StopPingPong()
    {
        StopCoroutine("PingPong");
        float cntBlur = blurMat.GetFloat("_BlurSize");
        while(cntBlur>0)
        {
            cntBlur -= Time.deltaTime;
            blurMat.SetFloat("_BlurSize", cntBlur);
            yield return null;
        }
        RenderFeatureManager.Instance.DisableRenderFeature(targetRenderFeature);
    }
}