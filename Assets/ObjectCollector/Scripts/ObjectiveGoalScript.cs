using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveGoalScript : MonoBehaviour
{
    public int objectivesReturned = 0;
    private int objectivesTotal = 0;
    public TextMeshProUGUI textmeshPro;
    public GameObject rewardPrefab;
    public int rewardAmount = 10;

    void Start()
    {
        SetAmountOfSpawnPoints();
        UpdateGUI();
    }

    // The amount of flags collected is updated on gui next to the total amount og flags
    private void UpdateGUI() { 
        string newText = objectivesReturned + "/" + objectivesTotal;
        textmeshPro.text = newText;
    }

    private void SetAmountOfSpawnPoints() {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        objectivesTotal = spawnPoints.Length;
    }

    private void CheckAndGiveReward() {
        if(objectivesReturned == objectivesTotal) {
            GiveRewardsInCircle();
        }
    }

    void GiveRewardsInCircle () 
    {
        float angle = 360f / (float)rewardAmount;
        float radius = 2.5f;
        for (int i = 0; i < rewardAmount; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
            Vector3 position = gameObject.transform.position + (rotation * Vector3.forward * radius);
            // Sets position to over ground
            position.y += .5f;
            Instantiate(rewardPrefab, position, rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerObjectivesScript player = other.GetComponent<PlayerObjectivesScript>();
            if (player.hasFlag) {
                player.DeliverFlag();
                objectivesReturned++;
                UpdateGUI();
                CheckAndGiveReward();
            }
        }
    }
}
