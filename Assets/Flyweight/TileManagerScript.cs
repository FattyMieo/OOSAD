using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerScript : MonoBehaviour
{
	//! Create tiles
	//! Where to create tiles

	public TileModel tileModelScript;
	public GameObject tilePrefab;

	int ROW_COUNT = 12;
	int COL_COUNT = 16;

	float tileSize = 0.64f;

	void Start () 
	{
		GenerateTileMap ();
	}

	void GenerateTileMap ()
	{		
		//! Go upwards
		for (int i = 0; i < ROW_COUNT; i++) 
		{
			//! Go to right
			for (int j = 0; j < COL_COUNT; j++) 
			{
				GameObject obj = (GameObject)Instantiate
				(
					tilePrefab,
					new Vector3
					(
						j * tileSize - COL_COUNT/2.0f * tileSize + tileSize/2.0f,
						i * tileSize - ROW_COUNT/2.0f *tileSize + tileSize/2.0f,
						0.0f
					),
					Quaternion.identity
				);

				TileScript tileScript = obj.GetComponent<TileScript> ();
				//! Set tile
				tileScript.myType = (TileType)Random.Range(0,(int)TileType.TOTAL);
				tileScript.tileModelScript = tileModelScript;
			}
		}	
	}
}
