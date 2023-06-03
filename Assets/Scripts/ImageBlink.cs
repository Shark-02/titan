using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageBlink : MonoBehaviour
{
    Image targetImage;
    public float timePerRound = 1;
    float sumTime;
    private void Awake()
    {
        targetImage = GetComponent<Image>();
    }
    void Update()
    {
        Color c = targetImage.color;
        sumTime += Time.deltaTime;
        c.a = Mathf.PingPong(sumTime, timePerRound / 2);
        targetImage.color = c;
    }
}
