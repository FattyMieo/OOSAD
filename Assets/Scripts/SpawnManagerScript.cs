using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
	private static SpawnManagerScript mInstance;

	public static SpawnManagerScript Instance
	{
		get
		{
			// Singleton implementation for objects that can be dynamically created (doesnt have reference to hierarchy objects)
			if(mInstance == null)
			{
				GameObject tempObject = GameObject.FindWithTag("SpawnManager");

				if(tempObject == null)
				{
					tempObject = Instantiate(PrefabManagerScript.Instance.spawnManagerPrefab, Vector3.zero, Quaternion.identity);
				}

				mInstance = tempObject.GetComponent<SpawnManagerScript>();
			}
			return mInstance;
		}
	}

	public GameObject[] enemyPrefabList;
	public List<EnemyBaseScript> enemyList = new List<EnemyBaseScript>();

	float spawnTimer = 0.0f;
	float spawnDuration = 0.5f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		spawnTimer += Time.deltaTime;
		if(spawnTimer > spawnDuration)
		{
			spawnTimer = 0.0f;
			EnemyBaseScript.Type type;

			if(enemyList.Count < 3)
			{
				type = EnemyBaseScript.Type.ENEMY_01;
			}
			else
			{
				type = (EnemyBaseScript.Type) Random.Range(0, (int) EnemyBaseScript.Type.TOTAL);
			}

			// ! Fancy Smenchy Spawning Logic
			EnemyBaseScript newEnemy = Spawn(type).GetComponent<EnemyBaseScript>();
			newEnemy.speed = Random.Range(1.0f, 5.0f);

			if(newEnemy.type == EnemyBaseScript.Type.ENEMY_03)
			{
				Spawn(EnemyBaseScript.Type.ENEMY_01);
				Spawn(EnemyBaseScript.Type.ENEMY_02);
			}
		}
		*/
		/*
		if(specialTimer > specialDuration)
		{
			Spawn(EnemyBaseScript.Type.TOTAL);
		}
		*/
	}

	// ! Factory method
	public GameObject Spawn (EnemyBaseScript.Type type)
	{
		GameObject newEnemy = null;

		if(type == EnemyBaseScript.Type.ENEMY_01)
		{
			//newEnemy = Instantiate(enemyPrefabList[(int) type], new Vector3(0.0f, 5.6f, 0.0f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject("Enemy01");
			newEnemy.transform.position = new Vector3(0.0f, 5.6f, 0.0f);
			newEnemy.transform.rotation = Quaternion.identity;
		}
		else if(type == EnemyBaseScript.Type.ENEMY_02)
		{
			//newEnemy = Instantiate(enemyPrefabList[(int) type], new Vector3(7.0f, 5.6f, 0.0f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject("Enemy02");
			newEnemy.transform.position = new Vector3(7.0f, 5.6f, 0.0f);
			newEnemy.transform.rotation = Quaternion.identity;
		}
		else if(type == EnemyBaseScript.Type.ENEMY_03)
		{
			//newEnemy = Instantiate(enemyPrefabList[(int) type], new Vector3(-7.0f, 5.6f, 0.0f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject("Enemy03");
			newEnemy.transform.position = new Vector3(-7.0f, 5.6f, 0.0f);
			newEnemy.transform.rotation = Quaternion.identity;
		}
		else
		{
			Debug.Log("Unknown Type in Spawn()");
		}

		enemyList.Add(newEnemy.GetComponent<EnemyBaseScript>());

		return newEnemy;
	}
}
