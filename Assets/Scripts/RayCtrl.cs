using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCtrl : MonoBehaviour
{
    AudioSource walkmusic;
    public Transform rayGameObject;
    public AudioClip[] clips;
    public Camera camera;
    private int countOb,countSaturn,countMon;
    Transform CameraTrans;
    void Start()
    {
        countOb = 0;
        countSaturn = 0;
        countMon = 0;
        walkmusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        CameraTrans = camera.transform;
        Ray ray = new Ray(CameraTrans.position, CameraTrans.rotation * Vector3.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 500f))
        {
            if (raycastHit.transform.gameObject.tag == "ObRock" && countOb==0)
            {
                walkmusic.Stop();
                walkmusic.clip = clips[0];
                walkmusic.Play();
                countOb++;
            }
            if (raycastHit.transform.gameObject.name == "SaturnTrack" && countSaturn == 0)
            {
                walkmusic.Stop();
                walkmusic.clip = clips[1];
                walkmusic.Play();
                countSaturn++;
            }
            if (raycastHit.transform.gameObject.name == "Mountain" && countMon == 0)
            {
                walkmusic.Stop();
                walkmusic.clip = clips[2];
                walkmusic.Play();
                countMon++;
            }
            Debug.Log(raycastHit.transform.gameObject.name);

        }
           
    }

 

}