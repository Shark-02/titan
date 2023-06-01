using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camParent;
    public CharacterController player;
    public GameObject Obstacle;
    public float moveSpeed;
    public float jumpVelocity;
    InputCommand command;
    Vector3 currentVelocity;
    [Range(0, 100)] public float gravityValue = 1.35f;
    [HideInInspector] public bool isAirBorne;
    [HideInInspector] public bool isCrouch;
    [Header("»èÃÔ")]
    public bool coma;
    public float comaduration = 20;    //»èÃÔ¼ä¸ô
    public float comatime = 0.5f; //»èÃÔÊ±³¤
    public Transform seaend;
    AudioSource audioplayer,hitplayer;
    static PlayerController instance;
    public static PlayerController Instance => instance;
    private void Start()
    {
        instance = this;
        audioplayer = GetComponent<AudioSource>();
        hitplayer=GameObject.Find("Target").GetComponent<AudioSource>();
    }
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
    public void HitByStone(bool killPlayer)
    {
        if(killPlayer)
        {
            Killed();
        }
        else
        {
            hitplayer.Play();
            Coma();
        }
    }
    private IEnumerator Comacoroutine(float comatime,float comaduration)
    {
        coma = true;
        CurtainController.Instance.ShowCurtain(comatime);
        audioplayer.Pause();
        yield return new WaitForSeconds(comaduration);
        Debug.Log("Coroutine ended after " + comaduration + " seconds.");
        CurtainController.Instance.HideCurtain(1);
        NavPathArrow.Instance.points[1] = seaend;
        NavPathArrow.Instance.ShowPath();
        Obstacle.SetActive(true);
        coma = false;
    }
    void Killed()
    {
        Debug.Log("Killed");
    }
    void Coma()
    {
        StartCoroutine(Comacoroutine(comatime, comaduration));
    }
}
