using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public static Tile Instance = null;
	public GameObject Building_0, Building_1;
	public GameObject followTarget;
	public float initTileSize = 200;
	public double buildingDensity = 0.3;
	private int randomNum = 0;
	private int numOfBuildingsInTile;
	private Vector3 position;
	private Vector3 newPosition;
	private Vector3 futurePosition;
	private float distance;
	private Vector3 direction;
	public bool timeToChange = false;
	private int changeFactor = 0;

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

	void Awake () {

		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}

		CharacterMovement.OnPlayerMoved += OnPlayerMoved;
	}

	void OnDestroy()
	{
		CharacterMovement.OnPlayerMoved -= OnPlayerMoved;
	}

	void OnPlayerMoved(Vector3 other){
		CharacterMovement moveScript = followTarget.GetComponent<CharacterMovement> ();
		newPosition = moveScript.getFootPosition ();
		distance = Mathf.Sqrt(Mathf.Pow(newPosition.x-position.x, 2f) - Mathf.Pow(newPosition.z-position.z, 2f));
		if (distance >= (initTileSize / 4)) {
			direction = newPosition - position;
			direction.y = 0;
			futurePosition = direction * 3 + position;
			futurePosition.y = 21.1f;
			createBuildings (futurePosition, initTileSize);
			position = newPosition;
			changeFactor++;
		}

		if (changeFactor >= 2) {
			timeToChange = true;
			changeFactor++;
		}
		if (changeFactor > 20) {
			timeToChange = false;
			changeFactor = 0;
		}
	}
}
