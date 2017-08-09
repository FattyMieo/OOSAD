using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
	#region Singleton
	private SpawnManagerScript() {}

	private static SpawnManagerScript mInstance;

	public static SpawnManagerScript Instance
	{
		get
		{
			if(mInstance == null) Debug.LogError(typeof(SpawnManagerScript).Name + " doesn't exist in the scene!");
			return mInstance;
		}
	}

	private void InitializeSingleton()
	{
		if (mInstance == null) mInstance = this;				//Assign this object to this reference
		else if (mInstance != this) Destroy(this.gameObject);	//Existed two or more instances, destroy duplicates
		DontDestroyOnLoad (gameObject);							//Avoid destroying when switching to another scene
	}
	#endregion Singleton

	public GameObject[] enemyPrefabList;
	public GameObject bulletPrefab;
	public GameObject bossObject;
	public List<EnemyManagerScript> enemyList = new List<EnemyManagerScript>();
	public List<BulletScript> bulletList = new List<BulletScript>();

	float spawnTimer = 0.0f;
	float spawnDuration = 2.0f;

	float bossSpawnTimer = 0.0f;
	float bossSpawnDuration = 60.0f;
	bool hasSpawnedBoss = false;

	public float camXExtend;
	public float camYExtend;

	// Use this for initialization
	void Awake ()
	{
		InitializeSingleton();
	}

	void Start()
	{
		camYExtend = Camera.main.orthographicSize;
		camXExtend = camYExtend * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update ()
	{
		spawnTimer += Time.deltaTime;
		if(!hasSpawnedBoss) bossSpawnTimer += Time.deltaTime;
		if(spawnTimer > spawnDuration)
		{
			spawnTimer = 0.0f;
			EnemyManagerScript.Type type = EnemyManagerScript.Type.ENEMY_01;

			if(!hasSpawnedBoss && bossSpawnTimer >= bossSpawnDuration)
			{
				bossObject.SetActive(true);
				hasSpawnedBoss = true;
			}
			else if(enemyList.Count < 3)
			{
				type = EnemyManagerScript.Type.ENEMY_01;
			}
			else if(enemyList.Count < 5)
			{
				type = EnemyManagerScript.Type.ENEMY_02;
			}
			else
			{
				type = (EnemyManagerScript.Type) Random.Range(0, (int) EnemyManagerScript.Type.BOSS);
			}

			// ! Fancy Smenchy Spawning Logic
			EnemyManagerScript newEnemy = Spawn(type).GetComponent<EnemyManagerScript>();

			if(newEnemy.type == EnemyManagerScript.Type.ENEMY_02)
			{
				Spawn(EnemyManagerScript.Type.ENEMY_01);
				Spawn(EnemyManagerScript.Type.ENEMY_01);
			}
		}
		/*
		if(specialTimer > specialDuration)
		{
			Spawn(EnemyBaseScript.Type.TOTAL);
		}
		*/
	}

	// ! Factory method
	public GameObject Spawn (EnemyManagerScript.Type type)
	{
		GameObject newEnemy = null;

		if(type == EnemyManagerScript.Type.ENEMY_01)
		{
			//newEnemy = Instantiate(enemyPrefabList[(int) type], new Vector3(0.0f, 5.6f, 0.0f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject("Enemy01");
		}
		else if(type == EnemyManagerScript.Type.ENEMY_02)
		{
			//newEnemy = Instantiate(enemyPrefabList[(int) type], new Vector3(7.0f, 5.6f, 0.0f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject("Enemy02");
		}
		else if(type == EnemyManagerScript.Type.ENEMY_03)
		{
			//newEnemy = Instantiate(enemyPrefabList[(int) type], new Vector3(-7.0f, 5.6f, 0.0f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject("Enemy03");
		}
		else
		{
			Debug.Log("Unknown Type in Spawn()");
			return null;
		}

		float spawnX;
		float spawnY;

		int random = Random.Range(0, 4);

		if(random % 2 == 0)
		{
			spawnX = Random.Range(-camXExtend-0.5f, camXExtend+0.5f);
			if(random / 2 == 0)
			{
				spawnY = -camYExtend-0.5f;
			}
			else
			{
				spawnY = camYExtend+0.5f;
			}
		}
		else
		{
			spawnY = Random.Range(-camYExtend-0.5f, camYExtend+0.5f);
			if(random / 2 == 0)
			{
				spawnX = -camXExtend-0.5f;
			}
			else
			{
				spawnX = camXExtend+0.5f;
			}
		}

		newEnemy.transform.position = new Vector3(spawnX, spawnY, 0.0f);
		newEnemy.transform.rotation = Quaternion.identity;

		enemyList.Add(newEnemy.GetComponent<EnemyManagerScript>());

		return newEnemy;
	}

	// ! Factory method
	public GameObject SpawnBullet()
	{
		GameObject newBullet = null;
		
		newBullet = ObjectPoolManagerScript.Instance.GetObject("Bullet");

		bulletList.Add(newBullet.GetComponent<BulletScript>());

		return newBullet;
	}
}
