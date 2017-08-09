using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	#region Singleton
	private GameManagerScript() {}

	private static GameManagerScript mInstance;

	public static GameManagerScript Instance
	{
		get
		{
			if(mInstance == null) Debug.LogError(typeof(GameManagerScript).Name + " doesn't exist in the scene!");
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

//	public Text scoreText;
//	public int score;

	void Awake ()
	{
		InitializeSingleton();
	}

	// Use this for initialization
	void Start ()
	{
		SoundManagerScript.Instance.PlayBGM(AudioClipID.BGM_GAMEPLAY);

		ObjectPoolManagerScript.Instance.CreatePool(SpawnManagerScript.Instance.enemyPrefabList[0], 100, 100);
		ObjectPoolManagerScript.Instance.CreatePool(SpawnManagerScript.Instance.enemyPrefabList[1], 20, 20);
		ObjectPoolManagerScript.Instance.CreatePool(SpawnManagerScript.Instance.enemyPrefabList[2], 20, 20);
		ObjectPoolManagerScript.Instance.CreatePool(SpawnManagerScript.Instance.bulletPrefab, 200, 200);
	}
	
	// Update is called once per frame
	void Update ()
	{
//		scoreText.text = "Score: " + score;
	}
}
