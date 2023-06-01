using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
    public static Logger Instance;
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        if (!Instance)
            Instance = this;
    }
    //public static void Log(string content)
    //{
        //Instance.text.text = content;
    //}
}
