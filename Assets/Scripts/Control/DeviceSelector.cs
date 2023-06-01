using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSelector : MonoBehaviour
{
    public PCPlayerController pcController;
    public VRPlayerController vrController;
    private void Awake()
    {
#if UNITY_EDITOR
        vrController.enabled = false;
#else
        pcController.enabled = false;
#endif
    }
}
