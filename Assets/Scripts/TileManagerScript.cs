using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerScript : MonoBehaviour
{
	public static TileManagerScript Instance;

	public List<Sprite> tileSpriteList = new List<Sprite>();

	public GameObject tilePrefab;

	public int ROW_COUNT = 12;
	public int COL_COUNT = 20;

	float tileSize = 0.64f;

	public Vector2[,] posMap = new Vector2[20, 12];
	public GameObject playerObj;

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		GenerateTileMap();

		SpawnManagerScript.Instance.SpawnEnemies();

		playerObj = GameObject.FindGameObjectWithTag("Player");

		PlayerScript playerScript = playerObj.GetComponent<PlayerScript>();

		int tempX = 0;
		int tempY = 0;

		do
		{
			tempX = Random.Range(1, COL_COUNT - 2);
			tempY = Random.Range(1, ROW_COUNT - 2);
		}
		while(SpawnManagerScript.Instance.IsEnemyPresent(tempX, tempY));

		playerScript.xPos = tempX;
		playerScript.yPos = tempY;

		playerObj.transform.position = posMap[playerScript.xPos, playerScript.yPos];
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void GenerateTileMap()
	{
		for(int i = 0 ; i < ROW_COUNT; i++)
		{
			for(int j = 0; j < COL_COUNT; j++)
			{
				GameObject obj = Instantiate(tilePrefab, new Vector3(j * tileSize - COL_COUNT / 2.0f * tileSize + tileSize / 2.0f, i * tileSize - ROW_COUNT / 2.0f * tileSize + tileSize / 2.0f), Quaternion.identity);
				
				posMap[j, i] = obj.transform.position;

				TileScript tileScript = obj.GetComponent<TileScript>();
				if(tileScript != null)
				{
					// ! Logic to select tile type
					if(i == 0 || i == ROW_COUNT - 1 || j == 0 || j == COL_COUNT - 1)
					{
						tileScript.type = TileType.WALL;
					}

					tileScript.InitializeTile();
				}
			}
		}
	}
}
