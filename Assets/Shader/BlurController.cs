using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
    public string targetRenderFeature = "BlurFeature";
    public Material blurMat;
    public float pingpongTime = 2;
    public float intensity = 10;
    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //        StartBlur();
    //    if (Input.GetKeyDown(KeyCode.E))
    //        StopBlur();
    //}
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
        while (true)
        {
            sumTime += Time.deltaTime;
            blurMat.SetFloat("_BlurSize", Mathf.PingPong(sumTime, pingpongTime) * intensity);
            yield return null;
        }
    }
    IEnumerator StopPingPong()
    {
        StopCoroutine("PingPong");
        float cntBlur = blurMat.GetFloat("_BlurSize");
        while (cntBlur > 0)
        {
            cntBlur -= Time.deltaTime * intensity * 2 / pingpongTime;
            blurMat.SetFloat("_BlurSize", cntBlur);
            yield return null;
        }
        blurMat.SetFloat("_BlurSize", 0);
        RenderFeatureManager.Instance.DisableRenderFeature(targetRenderFeature);
    }
}