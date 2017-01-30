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

	// with block gap
	int planeSizeX = 12*3*2+18;
	int planeSizeZ = 12*5*2+18;
	int halfBlockX = 2;
	int halfBlockZ = 2;
//	Vector3 upScaleLevel = new Vector3 (5f, 10f, 5f);// we dont need up scale for buildings

	Vector3 startPos;

	Hashtable blocks = new Hashtable();

	// Use this for initialization
	void Start () {

		this.gameObject.transform.position = Vector3.zero;
		//startPos = player.transform.position;// We cannot do this because when new block generates, they doesn't aline with
											   // the initial blocks
		startPos = Vector3.zero;

		float updateTime = Time.realtimeSinceStartup;

		// Initially generate the plane
		for (int x = -halfBlockX; x <= halfBlockX; x++) {
			for (int z = -halfBlockZ; z <= halfBlockZ; z++) {
				Vector3 pos = new Vector3 ((x * planeSizeX + startPos.x),
					0.541f,
					(z * planeSizeZ + startPos.z));
				GameObject b = (GameObject)Instantiate (plane, pos, Quaternion.identity);
				//b.SetActive (false);

				string blockName = "Block_" + ((int)(pos.x)).ToString () + "_" + ((int)(pos.z)).ToString ();
				b.name = blockName;
				Block block = new Block (b, updateTime);
				blocks.Add (blockName, block);
			}
		}
		// Upscale the terrain and set to active
		// We dont need upscale for buildings
//		for (int x = -halfBlockX; x <= halfBlockX; x++) {
//			for (int z = -halfBlockZ; z <= halfBlockZ; z++) {
//				Vector3 pos = new Vector3 ((x * planeSize + startPos.x),
//					0.541f, (z * planeSize + startPos.z));
//				string blockName = "Block_" + ((int)(pos.x)).ToString () + "_" + ((int)(pos.z)).ToString ();
//				GameObject b = GameObject.Find (blockName);
//				b.transform.localScale = upScaleLevel;
//				Vector3 newPos = new Vector3 ((x * planeSize * upScaleLevel.x+ startPos.x),
//					0.541f, (z * planeSize * upScaleLevel.z + startPos.z));
//				b.transform.position = newPos;
//				b.SetActive (true);
//			}
//		}
	}
	
	// Update is called once per frame
	void Update () {

		int xMove = (int)(player.transform.position.x - startPos.x);
		int zMove = (int)(player.transform.position.z - startPos.z);

		if (Mathf.Abs (xMove) >= planeSizeX || Mathf.Abs (zMove) >= planeSizeZ) {
			float updateTime = Time.realtimeSinceStartup;

			// force integer position and round to nearest block size
			int playerX = (int)(Mathf.Floor(player.transform.position.x/planeSizeX)*planeSizeX);
			int playerZ = (int)(Mathf.Floor (player.transform.position.z / planeSizeZ) * planeSizeZ);

			for (int x = -halfBlockX; x <= halfBlockX; x++) {
				for (int z = -halfBlockZ; z <= halfBlockZ; z++) {
					Vector3 pos = new Vector3((x*planeSizeX+playerX),
						0.541f,
						(z*planeSizeZ+playerZ));
					string blockName = "Block_" + ((int)(pos.x)).ToString () + "_" + ((int)(pos.z)).ToString ();

					if (!blocks.ContainsKey (blockName)) {
						GameObject b = (GameObject)Instantiate (plane, pos, Quaternion.identity);
						b.name = blockName;
						Block block = new Block (b, updateTime);
						blocks.Add (blockName, block);
					} else {
						(blocks [blockName] as Block).creationTime = updateTime;
					}
				}
			}

			// Destroy all blocks not just created or with time updated
			// and put new blocks and kept blocks in a new hashtable
			Hashtable newPlane = new Hashtable();
			foreach (Block blk in blocks.Values) {
				if (blk.creationTime != updateTime) {
					//Delete GameObject
					Destroy (blk.theBlock);
				} else {
					newPlane.Add (blk.theBlock.name, blk);
				}
			}

			// Copy new hashtable contents to the working hashtable
			blocks = newPlane;

			startPos = player.transform.position;
		}
	}
}
