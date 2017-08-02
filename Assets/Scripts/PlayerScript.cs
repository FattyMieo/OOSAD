using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public int xPos;
	public int yPos;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			yPos++;
			Move();
		}
		else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			yPos--;
			Move();
		}
		else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			xPos--;
			Move();
		}
		else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			xPos++;
			Move();
		}
	}

	void Move()
	{
		if(yPos >= TileManagerScript.Instance.ROW_COUNT - 1)
		{
			yPos = TileManagerScript.Instance.ROW_COUNT - 2;
		}
		if(xPos >= TileManagerScript.Instance.COL_COUNT - 1)
		{
			xPos = TileManagerScript.Instance.COL_COUNT - 2;
		}
		if(yPos < 1)
		{
			yPos = 1;
		}
		if(xPos < 1)
		{
			xPos = 1;
		}

		transform.position = TileManagerScript.Instance.posMap[xPos, yPos];
	}
}
