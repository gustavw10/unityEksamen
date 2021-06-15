using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    public int goldStored = 0;

    public void AddGold(int goldAmount) {
        goldStored += goldAmount;
    }
}
