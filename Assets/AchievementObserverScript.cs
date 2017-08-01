using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementObserverScript : MonoBehaviour 
{
	public AchievementType myType;
	public string achievementName;
	public Text achievementTitleText;
	public Image achievementBar;

	public int achievementMaxCount = 1;
	public int achievementCount = 0;

	// Use this for initialization
	void Start () 
	{
		AchievementSubjectScript.Instance.SubscribeObserver(this);
		achievementTitleText.text = achievementName;
		achievementBar.fillAmount = achievementCount/achievementMaxCount;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Notify(AchievementType type)
	{
		//! How should i react to this notification?
		if(myType == type)
		{
			achievementCount++;
			achievementBar.fillAmount = (float)achievementCount/(float)achievementMaxCount;
			if(achievementCount >= achievementMaxCount)
			{
				AchievementSubjectScript.Instance.UnSubscribeObserver(this);
			}
		}
	}
}
