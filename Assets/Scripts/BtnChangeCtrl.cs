using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnChangeCtrl : MonoBehaviour
{
    int num = 0;
    string OriBtnName = "BT_Jump_0";
    string OriBPName = "PB0";
    string BtnName,TextName,BPName,TPName;
    string OriSTName = "ST0";
    string OriTextName = "StoryText0";
    string BT_Mission = "BT_Mission";
    string BT_MissionStart = "BT_MissionStart";
    // Start is called before the first frame update
    void Start()
    {
        BPName= OriBPName+num;
        TPName= OriSTName+num;
        BtnName = OriBtnName + num;
        TextName= OriTextName + num;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnClick()
    {
        GameObject rootB = GameObject.Find(BPName);
        rootB.transform.Find(BtnName).gameObject.SetActive(false);
        //GameObject.Find(BtnName);
        GameObject rootP = GameObject.Find(TPName);
        rootP.transform.Find(TextName).gameObject.SetActive(false);
        //GameObject.Find(TextName).SetActive(false);
        if (num < 7)
        {
            num++;
            BPName = OriBPName + num;
            TPName = OriSTName + num;
            BtnName = OriBtnName + num;
            TextName = OriTextName + num;
            rootB = GameObject.Find(BPName);
            rootB.transform.Find(BtnName).gameObject.SetActive(true);
            //GameObject.Find(BtnName);
            rootP = GameObject.Find(TPName);
            rootP.transform.Find(TextName).gameObject.SetActive(true);

            //GameObject.Find(BtnName).SetActive(true);
            //GameObject.Find(TextName).SetActive(true);
        }
        else
        {
            //num = 7;
            //TPName = OriSTName + num;
            //TextName = OriTextName + num;
            //GameObject.Find(BtnName);
            //rootP = GameObject.Find(TPName);
            //rootP.transform.Find(TextName).gameObject.SetActive(true);
            num = 7;
            GameObject BT_start = GameObject.Find(BT_Mission);
            BT_start.transform.Find(BT_MissionStart).gameObject.SetActive(true);


        }

    }

    public void OnMissionClicked()
    {
        SceneManager.LoadScene("Terrain_URP");
    }
}
