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
		AchievementManagerScript.Instance.SubscribeObserver(this);
		achievementTitleText.text = achievementName;
		achievementBar.fillAmount = achievementCount/achievementMaxCount;
	}

	public void Notify(AchievementType type, int newValue)
	{
		//! How should i react to this notification?
		if(myType == type)
		{
			achievementCount = newValue;
			achievementBar.fillAmount = (float)achievementCount/(float)achievementMaxCount;
			if(achievementCount >= achievementMaxCount)
			{
				achievementCount = achievementMaxCount;
				achievementTitleText.text = "Achievement Completed!";
				AchievementManagerScript.Instance.UnSubscribeObserver(this);
			}
		}
	}
}
