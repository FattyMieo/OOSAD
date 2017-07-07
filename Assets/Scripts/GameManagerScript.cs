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
			GameObject tempObject = GameObject.FindWithTag("GameManager");

			if(tempObject == null)
			{
				GameObject go = new GameObject("GameManager");
				mInstance = go.AddComponent<GameManagerScript>();
				go.tag = "GameManager";
			}
			else
			{
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
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = "Score: " + score;
	}
}
