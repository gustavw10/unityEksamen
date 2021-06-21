using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerImproved : MonoBehaviour
{


    public Color gizmoColor = Color.red;


    public enum EnemyLevels
    {
        EnemyOne, EnemyTwo, EnemyThree, EnemyFour, EnemyFive
    }

    public string SpawnerType;

    public EnemyLevels enemyLevel = EnemyLevels.EnemyOne;

    public GameObject enemy_one;
    public GameObject enemy_two;
    public GameObject enemy_three;
    public GameObject enemy_four;
    public GameObject enemy_five;
    private Dictionary<EnemyLevels, GameObject> Enemies = new Dictionary<EnemyLevels, GameObject>(5);
    public int totalEnemy = 10;
    private int numEnemy = 0;
    //private int spawnedEnemy = 0;


    //private int SpawnID;

    private bool waveSpawn = false;
    public bool Spawn = true;

    public float waveTimer = 20.0f;
    private float timeTillWave = 0.0f;
    //Wave controls
    public int totalWaves = 5;
    private int numWaves = 0;

    public int waveOneAmount = 1;
    public int waveTwoAmount = 1;
    public int waveThreeAmount = 1;
    public int waveFourAmount = 1;
    public int waveFiveAmount = 1;

    void Start()
    {
        Enemies.Add(EnemyLevels.EnemyOne, enemy_one);
        Enemies.Add(EnemyLevels.EnemyTwo, enemy_two);
        Enemies.Add(EnemyLevels.EnemyThree, enemy_three);
        Enemies.Add(EnemyLevels.EnemyFour, enemy_four);
        Enemies.Add(EnemyLevels.EnemyFive, enemy_five);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
    }
    void Update()
    {

        if (numWaves <= totalWaves)
        {
            timeTillWave += Time.deltaTime;
            if (waveSpawn)
            {
                spawnEnemy();
            }
            if (timeTillWave >= waveTimer)
            {
                waveSpawn = true;
                timeTillWave = 0.0f;
                numWaves++;
                numEnemy = 0;
            }

        }
        else
        {
            Spawn = false;
        }
    }

    private void spawnEnemy()
    {
        setWaveSettings();

        GameObject Enemy = (GameObject)Instantiate(Enemies[enemyLevel], gameObject.transform.position, Quaternion.identity);

        numEnemy++;

        if (numEnemy >= totalEnemy)
        {
            waveSpawn = false;
        }
    }

    public void setWaveSettings()
    {
        switch (numWaves)
        {
            case 1:
                totalEnemy = waveOneAmount;
                enemyLevel = EnemyLevels.EnemyOne;
                break;
            case 2:
                totalEnemy = waveTwoAmount;
                enemyLevel = EnemyLevels.EnemyTwo;
                break;
            case 3:
                enemyLevel = EnemyLevels.EnemyThree;
                totalEnemy = waveThreeAmount;
                break;
            case 4:
                enemyLevel = EnemyLevels.EnemyFour;
                totalEnemy = waveFourAmount;
                break;
            case 5:
                enemyLevel = EnemyLevels.EnemyFive;
                totalEnemy = waveFiveAmount;
                break;
        }
    }
}