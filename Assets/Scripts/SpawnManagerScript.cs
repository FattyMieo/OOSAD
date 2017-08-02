using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
	public static SpawnManagerScript Instance;

	public int enemyTotal = 5;
	public GameObject enemyPrefab;
	public List<EnemyScript> enemyList = new List<EnemyScript>();

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
	}

	public bool IsEnemyPresent(int checkX, int checkY)
	{
		for(int i = 0; i < enemyList.Count; i++)
		{
			if(enemyList[i].xPos == checkX && enemyList[i].yPos == checkY)
			{
				return true;
			}
		}
		return false;
	}

	public void SpawnEnemies()
	{
		for(int i = 0; i < enemyTotal; i++)
		{
			EnemyScript enemyScript = Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity).GetComponent<EnemyScript>();

			// ! Set enemy pos
			enemyScript.xPos = Random.Range(1, TileManagerScript.Instance.COL_COUNT - 1);
			enemyScript.yPos = Random.Range(1, TileManagerScript.Instance.ROW_COUNT - 1);
			enemyScript.transform.position = TileManagerScript.Instance.posMap[enemyScript.xPos, enemyScript.yPos];

			// ! Make sure pos doesn't have enemy / player (future)

			// ! Save enemy in list
			enemyList.Add(enemyScript);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
