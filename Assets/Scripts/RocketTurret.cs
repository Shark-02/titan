using UnityEngine;
public class RocketTurret : MonoBehaviour
{

    public Transform target;       //目标
    public GameObject rocket;      //火箭 预设体
    public Transform firePoint;    //发射火箭的位置
    public float fireSpeed = 5;    //发射间隔
    public bool hitted=false;
    private static RocketTurret rt;

    private void Start()
    {
        rt = this;
    }
    // Update is called once per frame
    void Update()
    {
        fireSpeed -= Time.deltaTime;
        //Debug.Log("前" + hitted + "------");
        if (!hitted && fireSpeed < 0)
        {           
            fireSpeed = 5;
            Fire();  
        }
        //Debug.Log("后" + hitted + "------");

    }

    //开火方法
    void Fire()
    {
        GameObject tempRocket = Instantiate(rocket, firePoint.position, firePoint.rotation);
        tempRocket.GetComponent<Rocket>().target = GameObject.FindGameObjectWithTag("Target").transform;
        //hitted = tempRocket.GetComponent<Rocket>().hitted;
        //tempRocket.GetComponent<Rocket>().rocketTurret=rt;
    }

    public static RocketTurret Instance
    {
        get { return rt; }
        
    }

}
