using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public GameObject Building_0, Building_1;
	public GameObject followTarget;
	public float buildingDistance = 25;
	int randomNum = 0;

	// Use this for initialization
	void Start ()
	{
		CharacterMovement moveScript = followTarget.GetComponent<CharacterMovement> ();
		Vector3 position = moveScript.getFootPosition ();
		position.y = 21.1f;
		for (int i = 0; i < 4; i++)
		{
			Vector3 addPosition = new Vector3(Random.Range(-buildingDistance, buildingDistance), 0, Random.Range(-buildingDistance, buildingDistance));
			randomNum = Random.Range (0, 1 + 1);

			if (randomNum == 0) {
				Instantiate (Building_0, position+addPosition, Quaternion.identity);
			} else if (randomNum == 1) {
				Instantiate (Building_1, position+addPosition, Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
