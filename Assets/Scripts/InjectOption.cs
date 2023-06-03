using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectOption : MonoBehaviour
{
    [SerializeField] BlurController blurController;
    [SerializeField] HealthBarCtrl healthBar;
    public void Accept()
    {
        blurController.StopBlur();
        healthBar.decreaseRate /= 3;
        gameObject.SetActive(false);
    }
    public void Refuse()
    {
        gameObject.SetActive(false);
    }
}