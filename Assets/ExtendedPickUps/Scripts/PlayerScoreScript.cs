using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreScript : MonoBehaviour
{
    public TextMeshProUGUI textmeshPro;
    private int remainingCreeps = 0;

    void Start()
    {
        float startingAfter = 0.0f; // 0 sec
        float doEvery = 1.0f; // 1 sec 
        InvokeRepeating("UpdateGUI", startingAfter, doEvery);
    }

    private void UpdateRemainingCreeps() {
        GameObject[] creeps = GameObject.FindGameObjectsWithTag("Creep");
        remainingCreeps = creeps.Length;
    }

    private void UpdateGUI() {
        UpdateRemainingCreeps();
        textmeshPro.text = remainingCreeps.ToString("000");
    }
}
