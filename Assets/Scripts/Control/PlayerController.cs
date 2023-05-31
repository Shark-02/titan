using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camParent;
    public CharacterController player;

    public float moveSpeed;
    public float jumpVelocity;
    InputCommand command;
    Vector3 currentVelocity;
    [Range(0, 100)] public float gravityValue = 1.35f;
    [HideInInspector] public bool isAirBorne;
    [HideInInspector] public bool isCrouch;
    public void HandleInput(InputCommand command)
    {
        this.command = command;
        HandleCamAngle();
        HandleMovement();
    }
    void HandleCamAngle()
    {
        switch (command.inAngleType)
        {
            case InputCommand.InAngleType.DELTA:
                camParent.localEulerAngles += command.camAngle;
                break;
            case InputCommand.InAngleType.TARGET:
                camParent.localEulerAngles = command.camAngle;
                break;
        }
    }
    void HandleMovement()
    {
        Vector3 v = player.velocity;
        v.x = command.xzDir.x * moveSpeed;
        v.y = currentVelocity.y;
        v.z = command.xzDir.z * moveSpeed;
        isAirBorne = !player.isGrounded;
        isCrouch = !isAirBorne && command.isCrouchHold;
        v += Vector3.down * gravityValue * Time.deltaTime;
        if (!isAirBorne && !isCrouch)
        {
            if (command.isJumpPressed)
            {
                v.y = jumpVelocity;
            }
        }
        Logger.Log("isAirBorne:" + isAirBorne + "\nisCrouch:" + command.isCrouchHold);
        currentVelocity = v;
        //Debug.Log(v);
        player.Move(currentVelocity * Time.deltaTime);
        if (player.isGrounded)
            currentVelocity.y = -.1f;
    }
}
