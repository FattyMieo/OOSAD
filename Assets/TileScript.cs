using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
	SAND,
	BRICK,
	WOOD,
	NONE,
}

public class TileScript : MonoBehaviour 
{
	public TileModel tileModelScript;
	public TileType myType;

	// Use this for initialization
	void Start () 
	{

		GetComponent<SpriteRenderer>().sprite=tileModelScript.tileSpriteList [(int)myType];
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
