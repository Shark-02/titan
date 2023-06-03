using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlpha : MonoBehaviour
{
    private Image image;
    float colorA;
    bool IsColorA;
    void Start()
    {
        image = GetComponent<Image>();
        colorA = image.color.a;
    }

    // Update is called once per frame
    void Update()
    {

        if (colorA >= 1)//Õº∆¨“—œ‘ æ
        {
            IsColorA = true;
        }
        else if (colorA <= 0)//Õº∆¨“—“˛≤ÿ
        {
            IsColorA = false;
        }

        if (IsColorA)//Ω•“˛
        {
            colorA -= 1f * Time.deltaTime;
        }
        else//Ω•œ÷
        {
            colorA += 1f * Time.deltaTime;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, colorA);
        Debug.Log(colorA);
    }
}
