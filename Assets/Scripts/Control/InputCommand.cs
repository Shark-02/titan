using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCommand
{
    public Vector3 xzDir;
    public bool isCrouchHold;
    public bool isJumpPressed;
    public Vector3 camAngle;
    public enum InAngleType
    {
        DELTA,
        TARGET,
    }
    public InAngleType inAngleType;
    public InputCommand(Vector3 xzDir,bool isCrouchHold,bool isJumpPressed,Vector3 camAngle,InAngleType inAngleType)
    {
        this.xzDir = xzDir;
        this.isCrouchHold = isCrouchHold;
        this.isJumpPressed = isJumpPressed;
        this.camAngle = camAngle;
        this.inAngleType = inAngleType;
    }
}