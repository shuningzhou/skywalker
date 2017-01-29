using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Block{
	public GameObject theBlock;
	public float creationTime;

	public Block(GameObject b, float ct){
		theBlock = b;
		creationTime = ct;
	}
}

public class GenerateInfinite : MonoBehaviour {

	public GameObject plane;
	public GameObject player;

	int planeSize = 10;
	int halfBlockX = 10;
	int halfBlockZ = 10;
	Vector3 upScaleLevel = new Vector3 (10f, 15f, 10f);

	Vector3 startPos;

	Hashtable blocks = new Hashtable();

	// Use this for initialization
	void Start () {

		//test by biao
		gameObject.SetActive(false);

		this.gameObject.transform.position = Vector3.zero;
		startPos = player.transform.position;

		float updateTime = Time.realtimeSinceStartup;

		// Initially generate the terrain but set to inactive
		for (int x = -halfBlockX; x < halfBlockX; x++) {
			for (int z = -halfBlockZ; z < halfBlockZ; z++) {
				Vector3 pos = new Vector3 ((x * planeSize + startPos.x),
					0.541f,
					(z * planeSize + startPos.z));
				GameObject b = (GameObject)Instantiate (plane, pos, Quaternion.identity);
				b.SetActive (false);

				string blockName = "Block_" + ((int)(pos.x)).ToString () + "_" + ((int)(pos.z)).ToString ();
				b.name = blockName;
				Block block = new Block (b, updateTime);
				blocks.Add (blockName, block);
			}
		}
		// Upscale the terrain and set to active
		for (int x = -halfBlockX; x < halfBlockX; x++) {
			for (int z = -halfBlockZ; z < halfBlockZ; z++) {
				Vector3 pos = new Vector3 ((x * planeSize + startPos.x),
					0.541f,
					(z * planeSize + startPos.z));
				string blockName = "Block_" + ((int)(pos.x)).ToString () + "_" + ((int)(pos.z)).ToString ();
				GameObject b = GameObject.Find (blockName);
				//b.transform.localScale *= upScaleLevel;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
