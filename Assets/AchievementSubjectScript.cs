using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AchievementType
{
	ACHIEVEMENT_COUNT = 0,
	ACHIEVEMENT_GOAL,
	ACHIEVEMENT_LEVEL1,
	ACHIEVEMENT_LEVEL2,
	ACHIEVEMENT_LEVEL3,
}

public class AchievementSubjectScript : MonoBehaviour 
{
	public static AchievementSubjectScript Instance;

	List<AchievementObserverScript> observerList = new List<AchievementObserverScript>();

	public void Awake()
	{
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Notify(AchievementType.ACHIEVEMENT_COUNT);
		}
	}

	public void SubscribeObserver(AchievementObserverScript observerScript)
	{
		observerList.Add(observerScript);
	}

	public void UnSubscribeObserver(AchievementObserverScript observerScript)
	{
		observerList.Remove(observerScript);
	}

	public void Notify(AchievementType type)
	{
		for(int i = 0; i < observerList.Count; i++)
		{
			observerList[i].Notify(type);
		}
	}
}
