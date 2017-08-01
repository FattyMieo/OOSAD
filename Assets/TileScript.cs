using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
	NONE = -1,
	SAND = 0,
	BRICK,
	WOOD,
	TOTAL
}

public class TileScript : MonoBehaviour 
{
	public TileModel tileModelScript;
	public TileType myType;

	// Use this for initialization
	void Start () 
	{
		GetComponent<SpriteRenderer>().sprite = tileModelScript.tileSpriteList [(int)myType];
	}
}
