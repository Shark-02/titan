using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadPlayer : MonoBehaviour
{
    DissolveController dc1,dc2,dc3;
    explodeController ec1,ec2,ec3;
    // Start is called before the first frame update
    void Start()
    {
        dc1 = GameObject.Find("crack").GetComponent<DissolveController>();
        dc2 = GameObject.Find("crack02").GetComponent<DissolveController>();
        dc3 = GameObject.Find("crack03").GetComponent<DissolveController>();

        ec1 = GameObject.Find("Explosion1").GetComponent<explodeController>();
        ec2 = GameObject.Find("Explosion2").GetComponent<explodeController>();
        ec3 = GameObject.Find("Explosion3").GetComponent<explodeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "lead")
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "EndTarget")
        {
            dc1.active = true;
            dc2.active = true;
            dc3.active = true;
            ec1.active = true;
            ec2.active = true;
            ec3.active = true;
            Destroy(other.gameObject);
            StartCoroutine(StopParticle());
        }
    }

    IEnumerator StopParticle()
    {
        yield return new WaitForSeconds(30.0f);
        ec1.active = false;
        ec2.active = false;
        ec3.active = false;

    }




}
