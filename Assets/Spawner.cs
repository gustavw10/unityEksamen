
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class Spawner : MonoBehaviour
{

	public Color gizmoColor = Color.red;
 

	public enum SpawnTypes
    {
		Normal,Once,Wave,TimedWave
    }

	public enum EnemyLevels
    {
		Easy,Medium,Hard,Boss
    }

    public string SpawnerType;

	public EnemyLevels enemyLevel = EnemyLevels.Easy;
 
	public GameObject EasyEnemy;
	public GameObject MediumEnemy;
	public GameObject HardEnemy;
	public GameObject BossEnemy;
	private Dictionary<EnemyLevels, GameObject> Enemies = new Dictionary<EnemyLevels, GameObject>(4);
	public int totalEnemy = 10;
	private int numEnemy = 0;
	private int spawnedEnemy = 0;


	private int SpawnID;
 
	private bool waveSpawn = false;
	public bool Spawn = true;
	public SpawnTypes spawnType = SpawnTypes.Normal;

	public float waveTimer = 30.0f;
	private float timeTillWave = 0.0f;
	//Wave controls
	public int totalWaves = 5;
	private int numWaves = 0;

    public int waveOneAmount = 1;
    public int waveTwoAmount = 1;
    public int waveThreeAmount = 1;
    public int waveFourAmount = 1;
    public int waveFiveAmount = 1;
    public int waveSixAmount = 1;
    public int waveSevenAmount = 1;
    public int waveEightAmount = 1;
    public int waveNineAmount = 1;
    public int waveTenAmount = 1;

    private int waveCount = 1;

	void Start()
	{
		SpawnID = Random.Range(1, 500);
		Enemies.Add(EnemyLevels.Easy, EasyEnemy);
		Enemies.Add(EnemyLevels.Boss, BossEnemy);
		Enemies.Add(EnemyLevels.Medium, MediumEnemy);
		Enemies.Add(EnemyLevels.Hard, HardEnemy);
	}

	void OnDrawGizmos()
	{
		// Sets the color to red
		Gizmos.color = gizmoColor;
		//draws a small cube at the location of the gam object that the script is attached to
		Gizmos.DrawCube(transform.position, new Vector3 (0.5f,0.5f,0.5f));
	}
	void Update ()
	{

		 if(spawnType == SpawnTypes.TimedWave)
			{
				if(numWaves <= totalWaves)
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
					if(numEnemy >= totalEnemy)
					{
						waveSpawn = false;
					}
				}
				else
				{
					Spawn = false;
				}
			}
		}
	
	private void spawnEnemy()
	{
        setWaveSettings();
        print("Wave count: " + numWaves);

		GameObject Enemy = (GameObject) Instantiate(Enemies[enemyLevel], gameObject.transform.position, Quaternion.identity);
		//Enemy.SendMessage("setName", SpawnID);
		numEnemy++;
		spawnedEnemy++;
	}

    public void setWaveSettings(){
        switch(numWaves) {
            case 1:
                totalEnemy = waveOneAmount;
                break;
            case 2:
                totalEnemy = waveTwoAmount;
                if(SpawnerType == "WithLarge" || SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Hard;
                }
                break;
            case 3:            
                if(SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Medium;
                }
                else {
                    enemyLevel = EnemyLevels.Easy;
                }
                totalEnemy = waveThreeAmount;
                break;
            case 4:
                if(SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Medium;
                }
                totalEnemy = waveFourAmount;
                break;
            case 5:
                if(SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Medium;
                }
                if(SpawnerType == "WithLarge"){
                    enemyLevel = EnemyLevels.Hard;
                }
                totalEnemy = waveFiveAmount;
                break;
            case 6:
                if(SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Medium;
                }
                totalEnemy = waveSixAmount;
                break;
            case 7:
                if(SpawnerType == "WithLarge" || SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Hard;
                }
                totalEnemy = waveSevenAmount;
                break;
            case 8:
                if(SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Medium;
                }
                totalEnemy = waveEightAmount;
                break;
            case 9:
                if(SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Medium;
                }
                totalEnemy = waveNineAmount;
                break;
            case 10:
                if(SpawnerType == "WithFast"){
                    enemyLevel = EnemyLevels.Medium;
                }
                totalEnemy = waveTenAmount;
                break;
        }
    }

	public void killEnemy(int sID)
	{
		if (SpawnID == sID)
		{
			numEnemy--;
		}
	}

	public void enableSpawner(int sID)
	{
		if (SpawnID == sID)
		{
			Spawn = true;
		}
	}
	public void disableSpawner(int sID)
	{
		if(SpawnID == sID)
		{
			Spawn = false;
		}
	}
	public float TimeTillWave
	{
		get
		{
			return timeTillWave;
		}
	}

	public void enableTrigger()
	{
		Spawn = true;
	}
}