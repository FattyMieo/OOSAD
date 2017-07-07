using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
	private static GameManagerScript mInstance;

	public static GameManagerScript Instance
	{
		get
		{
			// Singleton implementation for objects that can be dynamically created (doesnt have reference to hierarchy objects)
			if(mInstance == null)
			{
				GameObject tempObject = GameObject.FindWithTag("GameManager");

				if(tempObject == null)
				{
					tempObject = Instantiate(PrefabManagerScript.Instance.gameManagerPrefab, Vector3.zero, Quaternion.identity);
				}

				mInstance = tempObject.GetComponent<GameManagerScript>();
			}
			return mInstance;
		}
	}

	public Text scoreText;
	public int score;

	// Use this for initialization
	void Start ()
	{
		SoundManagerScript.Instance.PlayBGM(AudioClipID.BGM_GAMEPLAY);

		ObjectPoolManagerScript.Instance.CreatePool(SpawnManagerScript.Instance.enemyPrefabList[0], 20, 20);
		ObjectPoolManagerScript.Instance.CreatePool(SpawnManagerScript.Instance.enemyPrefabList[1], 20, 20);
		ObjectPoolManagerScript.Instance.CreatePool(SpawnManagerScript.Instance.enemyPrefabList[2], 20, 20);
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = "Score: " + score;
	}
}
