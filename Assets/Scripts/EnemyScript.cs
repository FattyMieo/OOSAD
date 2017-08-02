using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolDirection
{
	NONE = -1,
	HORIZONTAL = 0,
	VERTICAL,
	TOTAL
}

public class EnemyScript : MonoBehaviour
{
	public int xPos;
	public int yPos;

	public PatrolDirection curDirection;
	bool isForward = true;

	float moveTimer = 0.0f;
	public float moveDuration = 1.0f;

	// Use this for initialization
	void Start ()
	{
//		int randForward = Random.Range(0, 2);
//		if(randForward == 0)
//			isForward = true;
//		else
//			isForward = false;

//		isForward = (Random.Range(0, 2) == 0 ? false : true);

		isForward = Random.Range(0, 2) == 0;

		curDirection = (PatrolDirection)Random.Range(0, (int)PatrolDirection.TOTAL);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GameManagerScript.Instance.curState != GameState.OVERWORLD)
			return;
		
		moveTimer += Time.deltaTime;
		if(moveTimer >= moveDuration)
		{
			moveTimer = 0.0f;
			if(curDirection == PatrolDirection.HORIZONTAL)
			{
				if(isForward)
					xPos++;
				else
					xPos--;
			}
			else if(curDirection == PatrolDirection.VERTICAL)
			{
				if(isForward)
					yPos++;
				else
					yPos--;
			}

			Move();
		}
	}

	void Move()
	{
		if(yPos >= TileManagerScript.Instance.ROW_COUNT - 1)
		{
			yPos = TileManagerScript.Instance.ROW_COUNT - 2;
			isForward = false;
		}
		if(xPos >= TileManagerScript.Instance.COL_COUNT - 1)
		{
			xPos = TileManagerScript.Instance.COL_COUNT - 2;
			isForward = false;
		}
		if(yPos < 1)
		{
			yPos = 1;
			isForward = true;
		}
		if(xPos < 1)
		{
			xPos = 1;
			isForward = true;
		}

		transform.position = TileManagerScript.Instance.posMap[xPos, yPos];
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			GameManagerScript.Instance.curState = GameState.BATTLE;
		}
	}
}
