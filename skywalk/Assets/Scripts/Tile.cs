using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public GameObject Building_0, Building_1;
	public GameObject followTarget;
	public float initTileSize = 1000;
	public double buildingDensity = 0.5;
	private int randomNum = 0;
	private int numOfBuildingsInTile;
	private Vector3 position;
	private Vector3 newPosition;

	// Use this for initialization
	void createBuildings(Vector3 centerPosition, float tileSize)
	{
		Debug.Log ("we are going to create builings");
		numOfBuildingsInTile = (int)(tileSize * buildingDensity);
		for (int i = 0; i < numOfBuildingsInTile; i++)
		{
			Vector3 addPosition = new Vector3(Random.Range(-tileSize/2, tileSize/2), 0, Random.Range(-tileSize/2, tileSize/2));
			randomNum = Random.Range (0, 1 + 1);

			if (randomNum == 0) {
				Instantiate (Building_0, centerPosition+addPosition, Quaternion.identity);
			} else if (randomNum == 1) {
				Instantiate (Building_1, centerPosition+addPosition, Quaternion.identity);
			}
		}
	}
	void Start ()
	{
		CharacterMovement moveScript = followTarget.GetComponent<CharacterMovement> ();
		position = moveScript.getFootPosition ();
		position.y = 21.1f;
		createBuildings (position, initTileSize);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
