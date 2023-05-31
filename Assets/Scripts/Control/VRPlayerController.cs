using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class VRPlayerController : MonoBehaviour
{
    public XROrigin xrOrigin;
    public float jumpThreshold = .2f;
    public float crouchThreshold = -.5f;
    UnityEngine.XR.InputDevice leftController;
    UnityEngine.XR.InputDevice rightController;
    private void Start()
    {
        if (PXR_Input.IsControllerConnected(PXR_Input.Controller.LeftController))
        {
            leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }
    }
    void Update()
    {
        //playerController.HandleInput(new InputCommand(Vector3.zero, xrOrigin.CameraInOriginSpaceHeight > jumpThreshold, xrOrigin.CameraInOriginSpaceHeight < crouchThreshold, Vector3.zero, InputCommand.InAngleType.DELTA));
        bool leftPrimaryButton, rightPrimaryButton;
        leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out leftPrimaryButton);
        rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out rightPrimaryButton);
        PlayerController.Instance.HandleInput(new InputCommand(Vector3.zero, rightPrimaryButton, leftPrimaryButton, Vector3.zero, InputCommand.InAngleType.DELTA));
    }
}