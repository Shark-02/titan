using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCurrentTime : MonoBehaviour
{
    private int year;
    private int month;
    private int day;
    TMP_Text text;
    // Start is called before the first frame update
    void Awake()
    {
        year = DateTime.Now.Year;
        month = DateTime.Now.Month;
        day = DateTime.Now.Day;
        GetComponent<TMP_Text>().text = year + "." + month + "." + day + "，探索者鲍比*格林在探索土卫六时因火山喷发遇难。";
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
