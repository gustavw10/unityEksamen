using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInventoryScript : MonoBehaviour
{
    public int goldStored = 0;
    public TextMeshProUGUI textmeshPro;
    public Image icon;
    public Sprite coinsSprite;
    public Sprite stackOfCoinsSprite;
    public Sprite bagOfCoinsSprite;

    public void AddGold(int goldAmount) {
        goldStored += goldAmount;
        UpdateGUI();
    }

    public void UpdateGUI() {
        textmeshPro.text = goldStored.ToString("000");
        if(goldStored > 99) {
            icon.sprite = bagOfCoinsSprite;
        } else if (goldStored > 9) {
            icon.sprite = stackOfCoinsSprite;
        } else {
            icon.sprite = coinsSprite;
        }
    }
}
