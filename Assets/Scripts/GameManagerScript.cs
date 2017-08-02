using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	OVERWORLD = 0,
	BATTLE,
	PAUSE,
	GAMEOVER,
	TOTAL
}

public class GameManagerScript : MonoBehaviour
{
	public static GameManagerScript Instance;

	public GameState curState = GameState.OVERWORLD;

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(curState == GameState.BATTLE)
		{
			Debug.Log("Check Battle");
		}
	}
}
