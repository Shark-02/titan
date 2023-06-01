using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class LeadPlayer : MonoBehaviour
{
    public GameObject audioplayertxt;
    [SerializeField]DissolveController dc1,dc2/*,dc3*/;
    [SerializeField] explodeController /*ec1,*/ec2, ec3;
    [SerializeField] NavPathArrow npa;
    RocketTurret rt;
    [SerializeField] List<RocketTurret> rts;
    CameraShake cmshake;
    public AudioClip[] clips;
    AudioSource audioplayer;
    Volume blur;
    // Start is called before the first frame update
    void Start()
    {
        //裂缝
        //dc1 = GameObject.Find("crack").GetComponent<DissolveController>();
        //dc2 = GameObject.Find("crack02").GetComponent<DissolveController>();
        ////dc3 = GameObject.Find("crack03").GetComponent<DissolveController>();

        //火山
        //ec1 = GameObject.Find("Explosion1").GetComponent<explodeController>();
        //ec2 = GameObject.Find("Explosion2").GetComponent<explodeController>();
        //ec3 = GameObject.Find("Explosion3").GetComponent<explodeController>();

        //cmshake = GetComponentIn<CameraShake>();
        //镜头震动
        cmshake = GetComponentInChildren<CameraShake>();

        //视线模糊
        //BeautifySettings.settings.blurIntensity.Override(0.5f);

        //石头发射
        rt = GameObject.Find("ShootSpot").GetComponentInChildren<RocketTurret>();
        audioplayer = GetComponent<AudioSource>();
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
        else if (other.gameObject.tag == "lead2")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "EndTarget")
        {
            audioplayer.GetComponent<AudioSource>().PlayOneShot(clips[1]);
            dc1.active = true;
            dc2.active = true;
            //dc3.active = true;
            /*ec1.active = true;*/
            ec2.active = true;
            ec3.active = true;
            rt.arrived = true;
            audioplayer.clip = clips[0];
            audioplayer.Play();
            StartCoroutine(cmshake.Shake(10.0f, 0.25f));
            Destroy(other.gameObject);
            StartCoroutine(StopParticle());
            GameObject[] flead = GameObject.FindGameObjectsWithTag("lead");
            foreach (GameObject leads in flead)
            {
                Destroy(leads);
            }
            npa.HidePath();
        }
        else if(other.gameObject.name=="ShootersTrigger")
        {
            StartCoroutine("ShootersCoroutine");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name == "SeaEnd")
        {
            Destroy(other.gameObject);
            NavPathArrow.Instance.HidePath();
        }
    }

    IEnumerator StopParticle()
    {
        yield return new WaitForSeconds(10.0f);
        
        /*ec1.active = false;*/
        ec2.active = false;
        ec3.active = false;

    }

    IEnumerator ShootersCoroutine()
    {
        RenderFeatureManager.Instance.EnableRenderFeature("FogFeature");
        SandStormManager.Instance.StartSandStorm(10f);
        foreach (var shooter in rts)
        {
            shooter.arrived = true;
            yield return new WaitForSeconds(2f);
        }
    }
}
