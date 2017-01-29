using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBuilding : MonoBehaviour {

	public GameObject cube;
	public GameObject plane;

	int buildingDistance = 20;
	int halfBlockX = 3;
	int halfBlockZ = 5;

	Vector3 startPos;

	// Use this for initialization
	void Start () {

		startPos = this.gameObject.transform.position;

		// Initially generate the terrain but set to inactive
		for (int x = -halfBlockX; x <= halfBlockX; x++) {
			for (int z = -halfBlockZ; z <= halfBlockZ; z++) {
				float randomNum = Random.Range (0f, 40f);

				// If cube is along the side, set it to a taller cube
				if ((x == -halfBlockX) || (x == halfBlockX) || (z == -halfBlockZ) || (z == halfBlockZ)) {
					randomNum = Random.Range (20f, 40f);
				}

				Vector3 pos = new Vector3 ((x * buildingDistance + startPos.x),
					randomNum / 2,
					(z * buildingDistance + startPos.z));
				
				// create a plane
				if ((x == 0) && (z == 0)) {
					pos.y = 0.541f;
					GameObject p = (GameObject)Instantiate (plane, pos, Quaternion.identity);
					p.transform.SetParent (this.gameObject.transform);
					p.transform.localScale = new Vector3((buildingDistance*halfBlockX*2+50)/10, 1,
						(buildingDistance*halfBlockZ*2+50)/10);
				}

				// if cube is shorter than 20, ignore it
				if (randomNum < 20) {
					continue;
				}
				GameObject c = (GameObject)Instantiate (cube, pos, Quaternion.identity);
				c.transform.localScale = new Vector3 (10, randomNum, 10);
				c.transform.SetParent (this.gameObject.transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
