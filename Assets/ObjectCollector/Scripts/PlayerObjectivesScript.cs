using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObjectivesScript : MonoBehaviour
{
    public Sprite active;
    private float activeAlpha = 0.0f;
    public Sprite inactive;
    private float inactiveAlpha = 0.6f;
    public Image icon;
    public Image foreground;
    public bool hasFlag = false;
    
    void Start()
    {
        // Player start out with no flag
        FadeOutIcon(); 
    }

    public void PickUpFlag() {
        if (!hasFlag) {
            hasFlag = true;
            FadeInIcon();
        }
    }

    public void DeliverFlag() {
        if (hasFlag) {
            hasFlag = false;
            FadeOutIcon();
        }
    }

    // Sets icon flag active with no fade which represents that the flag IS on the player
    private void FadeInIcon() {
        icon.sprite = active;
        Color tempColor = foreground.color;
        tempColor.a = activeAlpha;
        foreground.color = tempColor;
    }

    // Sets icon flag inactive with the dark fade which represents that the flag IS NOT on the player
    private void FadeOutIcon() {
        icon.sprite = inactive;
        Color tempColor = foreground.color;
        tempColor.a = inactiveAlpha;
        foreground.color = tempColor;
    }
}
