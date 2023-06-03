using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStyleChange : MonoBehaviour
{
    static PanelStyleChange instance;
    public static PanelStyleChange Instance => instance;
    [SerializeField] Image panel;
    [SerializeField] Sprite emergencyPanelImage;
    [SerializeField] Image dialogueBox;
    [SerializeField] Sprite emergencyDialogueBox;
    [SerializeField] Image energyBarFill;
    [SerializeField] Color emergencyBarColor;
    //[SerializeField] GameObject warning;
    [SerializeField] GameObject lowEnergy;
    private void Awake()
    {
        instance = this;
    }
    public void TurnEmergencyMode()
    {
        panel.sprite = emergencyPanelImage;
        dialogueBox.sprite = emergencyDialogueBox;
        //warning.SetActive(true);
        lowEnergy.SetActive(true);
        energyBarFill.color = emergencyBarColor;
    }
}