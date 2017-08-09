using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType
{
	ACHIEVEMENT_SCORE_200 = 0,
	ACHIEVEMENT_SCORE_500,
	ACHIEVEMENT_SCORE_1000,
	ACHIEVEMENT_DEFEAT_ENEMY01,
	ACHIEVEMENT_DEFEAT_ENEMY02,
	ACHIEVEMENT_DEFEAT_ENEMY03,
	ACHIEVEMENT_DEFEAT_BOSS,

	TOTAL
}

public class AchievementManagerScript : MonoBehaviour
{
	#region Singleton
	private AchievementManagerScript() {}

	private static AchievementManagerScript mInstance;

	public static AchievementManagerScript Instance
	{
		get
		{
			if(mInstance == null) Debug.LogError(typeof(AchievementManagerScript).Name + " doesn't exist in the scene!");
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

	List<AchievementObserverScript> observerList = new List<AchievementObserverScript>();
	public int score;
	public int[] enemiesSlain;

	void Awake ()
	{
		InitializeSingleton();
		score = 0;
		enemiesSlain = new int[(int)EnemyManagerScript.Type.TOTAL];
	}

	// Update is called once per frame
	void Update () 
	{
//		if(Input.GetKeyDown(KeyCode.Space))
//		{
//			Notify(AchievementType.ACHIEVEMENT_COUNT);
//		}
	}

	public void AddScore(int newScore)
	{
		score += newScore;

		Notify(AchievementType.ACHIEVEMENT_SCORE_200, score);
		Notify(AchievementType.ACHIEVEMENT_SCORE_500, score);
		Notify(AchievementType.ACHIEVEMENT_SCORE_1000, score);
	}

	public void AddMonsterSlay(EnemyManagerScript.Type type)
	{
		enemiesSlain[(int)type]++;

		switch(type)
		{
			case EnemyManagerScript.Type.ENEMY_01:
				Notify(AchievementType.ACHIEVEMENT_DEFEAT_ENEMY01, enemiesSlain[(int)type]);
				break;
			case EnemyManagerScript.Type.ENEMY_02:
				Notify(AchievementType.ACHIEVEMENT_DEFEAT_ENEMY02, enemiesSlain[(int)type]);
				break;
			case EnemyManagerScript.Type.ENEMY_03:
				Notify(AchievementType.ACHIEVEMENT_DEFEAT_ENEMY03, enemiesSlain[(int)type]);
				break;
			case EnemyManagerScript.Type.BOSS:
				Notify(AchievementType.ACHIEVEMENT_DEFEAT_BOSS, enemiesSlain[(int)type]);
				break;
		}
	}

	public void SubscribeObserver(AchievementObserverScript observerScript)
	{
		observerList.Add(observerScript);
		// ! For scroll view
		observerScript.gameObject.SetActive(true);
	}

	public void UnSubscribeObserver(AchievementObserverScript observerScript)
	{
		observerList.Remove(observerScript);
		// ! For scroll view
		observerScript.gameObject.SetActive(false);
	}

	public void Notify(AchievementType type, int newValue)
	{
		for(int i = 0; i < observerList.Count; i++)
		{
			observerList[i].Notify(type, newValue);
		}
	}
}
