using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class StoryShow_TMP : MonoBehaviour
{
    [Header("打字间隔时间")]
    public float speedTime = 0.2f;//打字间隔时间

    float timer;//计时器时间
    TMP_Text TextCompnt;//Text文字组件
    int wordNumber;//文字数量
    public static bool isStart;//是否开始打字

    public string wordContent;//----文字内容

    void Awake()
    {
        TextCompnt = this.GetComponent<TMP_Text>();//从当前物体获取到Text组件
        wordContent = TextCompnt.text;
        TextCompnt.text="";
        isStart = false;
    }

    void FixedUpdate()
    {
        StartTyping();
    }
    void StartTyping()
    {
        if (isStart)
        {
            timer += Time.deltaTime;//简单的计时器
            if (timer >= speedTime)//如果计时器时间>打字间隔时间
            {
                timer = 0;//重置
                wordNumber++;//文字数量+1

                //Substring() 官方文档解释：从此实例检索子字符串。 子字符串从指定的字符位置开始且具有指定的长度。
                TextCompnt.text = wordContent.Substring((0), wordNumber);//数字数量的起始字符位置（从零开始）
                if (wordNumber >= wordContent.Length)//数字数量=文字的长度
                {
                    isStart = false;//停止打字
                }
            }
        }
    }
}
