using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBuilding : MonoBehaviour {

	public GameObject cube;

	int buildingDistance = 15;
	int halfBlockX = 10;
	int halfBlockZ = 10;

	Vector3 startPos;

	// Use this for initialization
	void Start () {

		startPos = this.gameObject.transform.position;

		// Initially generate the terrain but set to inactive
		for (int x = -halfBlockX; x <= halfBlockX; x++) {
			for (int z = -halfBlockZ; z <= halfBlockZ; z++) {
				float randomNum = Random.Range (0f, 40f);
				if (randomNum < 20) {
					continue;
				}
				Vector3 pos = new Vector3 ((x * buildingDistance + startPos.x),
					randomNum / 2,
					(z * buildingDistance + startPos.z));
				GameObject c = (GameObject)Instantiate (cube, pos, Quaternion.identity);
				c.transform.localScale = new Vector3 (10, randomNum, 10);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
