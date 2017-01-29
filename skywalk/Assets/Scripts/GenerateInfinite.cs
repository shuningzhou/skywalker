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

	Vector3 startPos;

	Hashtable blocks = new Hashtable();

	// Use this for initialization
	void Start () {

		//test by biao
		gameObject.SetActive(false);

		this.gameObject.transform.position = Vector3.zero;
		startPos = player.transform.position;

		float updateTime = Time.realtimeSinceStartup;

		for (int x = -halfBlockX; x < halfBlockX; x++) {
			for (int z = -halfBlockZ; z < halfBlockZ; z++) {
				Vector3 pos = new Vector3 ((x * planeSize + startPos.x),
					              0.541f,
					              (z * planeSize + startPos.z));
				GameObject b = (GameObject)Instantiate (plane, pos, Quaternion.identity);

				string blockName = "Block_" + ((int)(pos.x)).ToString () + "_" + ((int)(pos.z)).ToString ();
				b.name = blockName;
				Block block = new Block (b, updateTime);
				blocks.Add (blockName, block);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
