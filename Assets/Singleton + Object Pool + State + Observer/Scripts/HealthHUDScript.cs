using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUDScript : MonoBehaviour
{
	public Text healthTitleText;
	public Image healthBar;

	public int maxCount = 1;
	public int valueCount = 0;

	// Use this for initialization
	void Start () 
	{
		healthBar.fillAmount = (float)valueCount / (float)maxCount;
	}

	public void Notify(int curHealth, bool force = false)
	{
		if(force || valueCount != curHealth)
		{
			valueCount = curHealth;
			healthBar.fillAmount = (float)valueCount / (float)maxCount;
		}
	}
}
