using System.Collections;
using UnityEngine;
public class RocketTurret : MonoBehaviour
{

    public Transform target;       //目标
    public GameObject rocket;      //火箭 预设体
    public Transform firePoint;    //发射火箭的位置
    public float fireSpeed = 25;    //发射间隔
    public bool hitted=false;
    bool handledHit;
    public bool arrived=false;
    public bool killPlayer;

    // Update is called once per frame
    void Update()
    {
        fireSpeed -= Time.deltaTime;
        Debug.Log("前" + hitted + "------");
        if (arrived && !hitted && fireSpeed < 0)
        {           
            fireSpeed = 5;
            Fire();  
        }
        //Debug.Log("后" + hitted + "------");
        if (hitted&&!handledHit)
        {
            handledHit = true;
            PlayerController.Instance.HitByStone(killPlayer);
        }

    }

    //开火方法
    void Fire()
    {
        GameObject tempRocket = Instantiate(rocket, firePoint.position, firePoint.rotation);
        Rocket r=tempRocket.GetComponent<Rocket>();
        r.target = GameObject.FindGameObjectWithTag("Target").transform;
        r.rocketTurret = this;
        //hitted = tempRocket.GetComponent<Rocket>().hitted;
        //tempRocket.GetComponent<Rocket>().rocketTurret=rt;
    }

}
