using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
    public Material blurMat;
    public void SetBlur(int sampleCount, float blurSize)
    {
        blurMat.SetInt("_SampleCount", sampleCount);
        blurMat.SetFloat("_BlurSize", blurSize);
    }
}