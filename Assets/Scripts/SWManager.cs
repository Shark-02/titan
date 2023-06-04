using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

public class SWManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PXR_Manager.Instance.SetSpaceWarp(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
