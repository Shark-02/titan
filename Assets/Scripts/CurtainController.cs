using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CurtainController : MonoBehaviour
{
    [SerializeField] Image curtain;
    public static CurtainController Instance { get => instance; }
    static CurtainController instance;
    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    Coroutine coroutine;
    public void ShowCurtain(float timePeriod)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(ShowCurtainCoroutine(timePeriod));
    }
    public void HideCurtain(float timePeriod)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(HideCurtainCoroutine(timePeriod));
    }
    IEnumerator ShowCurtainCoroutine(float timePeriod)
    {
        curtain.raycastTarget = true;
        float sumTime = 0;
        Color c = curtain.color;
        c.a = 0;
        while (sumTime < timePeriod)
        {
            sumTime += Time.deltaTime;
            c.a = sumTime / timePeriod;
            curtain.color = c;
            yield return null;
        }
        c.a = 1;
        curtain.color = c;
    }
    IEnumerator HideCurtainCoroutine(float timePeriod)
    {
        float sumTime = timePeriod;
        Color c = curtain.color;
        c.a = 1;
        while (sumTime > 0)
        {
            sumTime -= Time.deltaTime;
            c.a = sumTime / timePeriod;
            curtain.color = c;
            yield return null;
        }
        c.a = 0;
        curtain.color = c;
        curtain.raycastTarget = false;
    }
}