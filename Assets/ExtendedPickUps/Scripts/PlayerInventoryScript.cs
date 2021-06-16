using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventoryScript : MonoBehaviour
{
    public int goldStored = 0;
    public TextMeshProUGUI textmeshPro;

    public void AddGold(int goldAmount) {
        goldStored += goldAmount;
        textmeshPro.text = goldStored.ToString("000");
    }
}
