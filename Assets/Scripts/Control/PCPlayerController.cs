using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPlayerController : MonoBehaviour
{
    public PlayerController playerController;
    [Header("Ðý×ª")]
    public Transform camParent;
    public float xSensitivity;
    public float ySensitivity;
    [Range(0, 89)] public float maxPitchAngle;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    InputCommand command;
    void Update()
    {
        playerController.HandleInput(new InputCommand(GetXZDir(), Input.GetKey(KeysForPCTest.crouchKey), Input.GetKeyDown(KeysForPCTest.jumpKey),EulerAnglesChange(), InputCommand.InAngleType.TARGET));
    }
    Vector3 EulerAnglesChange()
    {
        Vector3 newAngles = camParent.localEulerAngles + new Vector3(Input.GetAxis("Mouse Y") * ySensitivity, Input.GetAxis("Mouse X") * xSensitivity, 0);
        newAngles.x = (newAngles.x + 36000) % 360;
        if (newAngles.x > 180) newAngles.x -= 360;
        newAngles.x = Mathf.Min(maxPitchAngle, Mathf.Max(-maxPitchAngle, newAngles.x));
        return newAngles;
    }
    Vector3 GetXZDir()
    {
        Vector3 forwardDir = camParent.forward;
        float angle = Mathf.Atan2(forwardDir.z, forwardDir.x);
        float leftAngle = angle + Mathf.PI / 2;
        forwardDir = Vector3.zero;
        if (Input.GetKey(KeysForPCTest.forwardKey))
            forwardDir += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        if (Input.GetKey(KeysForPCTest.backwardKey))
            forwardDir -= new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        if (Input.GetKey(KeysForPCTest.leftKey))
            forwardDir += new Vector3(Mathf.Cos(leftAngle), 0, Mathf.Sin(leftAngle));
        if (Input.GetKey(KeysForPCTest.rightKey))
            forwardDir -= new Vector3(Mathf.Cos(leftAngle), 0, Mathf.Sin(leftAngle));
        if (forwardDir.magnitude == 0)
            return Vector3.zero;
        return forwardDir.normalized;
    }
}