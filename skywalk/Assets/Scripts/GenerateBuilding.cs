using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBuilding : MonoBehaviour {

	public GameObject cube;
	public GameObject plane;

	int buildingDistance = 12;
	int halfBlockX = 3;
	int halfBlockZ = 5;

	Vector3 startPos;

	// Use this for initialization
	void Start () {

		startPos = this.gameObject.transform.position;

		// Initially generate the terrain but set to inactive
		for (int x = -halfBlockX; x <= halfBlockX; x++) {
			for (int z = -halfBlockZ; z <= halfBlockZ; z++) {
				float randomNum = Random.Range (0f, 50f);
				// Sort the building height to make it more nature
				if (randomNum <= 10f) {
					randomNum = 10f;
				}
				if (10 < randomNum && randomNum< 40) {
					randomNum = Random.Range (25f, 30f);
				}
				if (randomNum > 40) {
					randomNum = 50;
				}

				// If cube is along the side, set it to a taller cube
//				if ((x == -halfBlockX) || (x == halfBlockX) || (z == -halfBlockZ) || (z == halfBlockZ)) {
//					randomNum = Random.Range (10f, 50f);
//				}

				Vector3 pos = new Vector3 ((x * buildingDistance + startPos.x),
					0.541f,
					(z * buildingDistance + startPos.z));
				
				// create a plane
				if ((x == 0) && (z == 0)) {
					GameObject p = (GameObject)Instantiate (plane, pos, Quaternion.identity);
					p.transform.SetParent (this.gameObject.transform);
					p.transform.localScale = new Vector3((buildingDistance*halfBlockX*2+20)/10, 1,
						(buildingDistance*halfBlockZ*2+20)/10);
				}

//				// if cube is shorter than 20, ignore it
//				if (randomNum < 20) {
//					continue;
//				}
				pos.y = randomNum / 2;
				float randomNum2 = Random.Range (8f, 11f);
				float randomNum3 = Random.Range (8f, 11f);
				if (randomNum == 50) {
					randomNum2 = randomNum3 = 10;
				}
				GameObject c = (GameObject)Instantiate (cube, pos, Quaternion.identity);
				c.transform.localScale = new Vector3 (randomNum2, randomNum, randomNum3);
				c.transform.SetParent (this.gameObject.transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
