using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
	NONE = -1,
	SAND = 0,
	BRICK,
	WOOD,
	WALL,
	TOTAL
}

public class TileScript : MonoBehaviour
{
	public TileType type;
	
	public void InitializeTile()
	{
		GetComponent<SpriteRenderer>().sprite = TileManagerScript.Instance.tileSpriteList[(int)type];

		if(type != TileType.WALL)
		{
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
