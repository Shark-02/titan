using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxSetter : MonoBehaviour
{
    [SerializeField] Image dialogueBox;
    static DialogueBoxSetter instance;
    public static DialogueBoxSetter Instance => instance;
    public void SetProfile(Sprite targetSprite)
    {
        dialogueBox.sprite = targetSprite;
    }
}