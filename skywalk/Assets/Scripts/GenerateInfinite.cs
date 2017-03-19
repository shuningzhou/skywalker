using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateInfinite : MonoBehaviour {

	public GameObject plane;
	public GameObject player;

	// with block gap
	int planeSizeX = 12*9; // buildingDistance*halfBlockX+Road_Width
	int planeSizeZ = 12*9; // buildingDistance*halfBlockZ+Road_Width
	int halfBlockX = 1;
	int halfBlockZ = 1;
//	Vector3 upScaleLevel = new Vector3 (5f, 10f, 5f);// we dont need up scale for buildings

	Vector3 startPos;

	List<GameObject> blocks = new List<GameObject>();

	void Awake () {
		CharacterMovement.OnPlayerMoved += OnPlayerMoved;
	}

	void OnDestroy()
	{
		CharacterMovement.OnPlayerMoved -= OnPlayerMoved;
	}

	void OnPlayerMoved(Vector3 other){
		// force integer position and round to nearest block size
		GameObject b5 = blocks [4];
		Vector3 center = b5.transform.position;
		if (other.z > center.z + (float)planeSizeZ / 2.0f) 
		{
			exitTop ();
		} 
		else if (other.z < center.z - (float)planeSizeZ / 2.0f) 
		{
			exitBot ();
		}
		else if (other.x < center.x - (float)planeSizeX / 2.0f) 
		{
			exitLeft ();
		}
		else if (other.x > center.x + (float)planeSizeX / 2.0f) 
		{
			exitRight ();
		}
	}

	void exitLeft()
	{
		GameObject b0 = blocks [0];
		GameObject b1 = blocks [1];
		GameObject b2 = blocks [2];
		GameObject b3 = blocks [3];
		GameObject b4 = blocks [4];
		GameObject b5 = blocks [5];
		GameObject b6 = blocks [6];
		GameObject b7 = blocks [7];
		GameObject b8 = blocks [8];

		Vector3 newPosition = new Vector3 (b2.transform.position.x - 3 * planeSizeX, b2.transform.position.y, b2.transform.position.z);
		b2.transform.position = newPosition;

		newPosition = new Vector3 (b5.transform.position.x - 3 * planeSizeX, b5.transform.position.y, b5.transform.position.z);
		b5.transform.position = newPosition;

		newPosition = new Vector3 (b8.transform.position.x - 3 * planeSizeX, b8.transform.position.y, b8.transform.position.z);
		b8.transform.position = newPosition;

		blocks.Clear ();
		blocks.Add (b2);
		blocks.Add (b0);
		blocks.Add (b1);
		blocks.Add (b5);
		blocks.Add (b3);
		blocks.Add (b4);
		blocks.Add (b8);
		blocks.Add (b6);
		blocks.Add (b7);
	}

	void exitRight()
	{
		GameObject b0 = blocks [0];
		GameObject b1 = blocks [1];
		GameObject b2 = blocks [2];
		GameObject b3 = blocks [3];
		GameObject b4 = blocks [4];
		GameObject b5 = blocks [5];
		GameObject b6 = blocks [6];
		GameObject b7 = blocks [7];
		GameObject b8 = blocks [8];

		Vector3 newPosition = new Vector3 (b0.transform.position.x + 3 * planeSizeX, b0.transform.position.y, b0.transform.position.z);
		b0.transform.position = newPosition;

		newPosition = new Vector3 (b3.transform.position.x + 3 * planeSizeX, b3.transform.position.y, b3.transform.position.z);
		b3.transform.position = newPosition;

		newPosition = new Vector3 (b6.transform.position.x + 3 * planeSizeX, b6.transform.position.y, b6.transform.position.z);
		b6.transform.position = newPosition;

		blocks.Clear ();
		blocks.Add (b1);
		blocks.Add (b2);
		blocks.Add (b0);
		blocks.Add (b4);
		blocks.Add (b5);
		blocks.Add (b3);
		blocks.Add (b7);
		blocks.Add (b8);
		blocks.Add (b6);
	}

	void exitTop()
	{
		GameObject b0 = blocks [0];
		GameObject b1 = blocks [1];
		GameObject b2 = blocks [2];
		GameObject b3 = blocks [3];
		GameObject b4 = blocks [4];
		GameObject b5 = blocks [5];
		GameObject b6 = blocks [6];
		GameObject b7 = blocks [7];
		GameObject b8 = blocks [8];

		Vector3 newPosition = new Vector3 (b6.transform.position.x, b6.transform.position.y, b6.transform.position.z + 3 * planeSizeZ);
		b6.transform.position = newPosition;

		newPosition = new Vector3 (b7.transform.position.x, b7.transform.position.y, b7.transform.position.z + 3 * planeSizeZ);
		b7.transform.position = newPosition;

		newPosition = new Vector3 (b8.transform.position.x, b8.transform.position.y, b8.transform.position.z + 3 * planeSizeZ);
		b8.transform.position = newPosition;

		blocks.Clear ();
		blocks.Add (b6);
		blocks.Add (b7);
		blocks.Add (b8);
		blocks.Add (b0);
		blocks.Add (b1);
		blocks.Add (b2);
		blocks.Add (b3);
		blocks.Add (b4);
		blocks.Add (b5);
	}

	void exitBot()
	{
		GameObject b0 = blocks [0];
		GameObject b1 = blocks [1];
		GameObject b2 = blocks [2];
		GameObject b3 = blocks [3];
		GameObject b4 = blocks [4];
		GameObject b5 = blocks [5];
		GameObject b6 = blocks [6];
		GameObject b7 = blocks [7];
		GameObject b8 = blocks [8];

		Vector3 newPosition = new Vector3 (b0.transform.position.x, b0.transform.position.y, b0.transform.position.z - 3 * planeSizeZ);
		b0.transform.position = newPosition;

		newPosition = new Vector3 (b1.transform.position.x, b1.transform.position.y, b1.transform.position.z - 3 * planeSizeZ);
		b1.transform.position = newPosition;

		newPosition = new Vector3 (b2.transform.position.x, b2.transform.position.y, b2.transform.position.z - 3 * planeSizeZ);
		b2.transform.position = newPosition;
	
		blocks.Clear ();
		blocks.Add (b3);
		blocks.Add (b4);
		blocks.Add (b5);
		blocks.Add (b6);
		blocks.Add (b7);
		blocks.Add (b8);
		blocks.Add (b0);
		blocks.Add (b1);
		blocks.Add (b2);
	}

	// Use this for initialization
	void Start () {

		this.gameObject.transform.position = Vector3.zero;
		//startPos = player.transform.position;// We cannot do this because when new block generates, they doesn't aline with
											   // the initial blocks
		startPos = Vector3.zero;

		float updateTime = Time.realtimeSinceStartup;

		// Initially generate the plane
		for (int z = halfBlockZ; z >= -halfBlockZ; z--) {
			for (int x = -halfBlockX; x <= halfBlockX; x++) {
				Vector3 pos = new Vector3 ((x * planeSizeX + startPos.x),
					0.541f,
					(z * planeSizeZ + startPos.z));
				GameObject b = (GameObject)Instantiate (plane, pos, Quaternion.identity);
				string blockName = "Block_" + x.ToString () + "_" + z.ToString ();
				b.name = blockName;
				blocks.Add (b);
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
//	void Update () {
//
//		int xMove = (int)(player.transform.position.x - startPos.x);
//		int zMove = (int)(player.transform.position.z - startPos.z);
//
//		if (Mathf.Abs (xMove) >= planeSizeX || Mathf.Abs (zMove) >= planeSizeZ) {
//			float updateTime = Time.realtimeSinceStartup;
//
//			// force integer position and round to nearest block size
//			int playerX = (int)(Mathf.Floor(player.transform.position.x/planeSizeX)*planeSizeX);
//			int playerZ = (int)(Mathf.Floor (player.transform.position.z / planeSizeZ) * planeSizeZ);
//
//			for (int x = -halfBlockX; x <= halfBlockX; x++) {
//				for (int z = -halfBlockZ; z <= halfBlockZ; z++) {
//					Vector3 pos = new Vector3((x*planeSizeX+playerX),
//						0.541f,
//						(z*planeSizeZ+playerZ));
//					string blockName = "Block_" + ((int)(pos.x)).ToString () + "_" + ((int)(pos.z)).ToString ();
//
//					if (!blocks.ContainsKey (blockName)) {
//						GameObject b = (GameObject)Instantiate (plane, pos, Quaternion.identity);
//						b.name = blockName;
//						Block block = new Block (b, updateTime);
//						blocks.Add (blockName, block);
//					} else {
//						(blocks [blockName] as Block).creationTime = updateTime;
//					}
//				}
//			}
//
//			// Destroy all blocks not just created or with time updated
//			// and put new blocks and kept blocks in a new hashtable
//			Hashtable newPlane = new Hashtable();
//			foreach (Block blk in blocks.Values) {
//				if (blk.creationTime != updateTime) {
//					//Delete GameObject
//					Destroy (blk.theBlock);
//				} else {
//					newPlane.Add (blk.theBlock.name, blk);
//				}
//			}
//
//			// Copy new hashtable contents to the working hashtable
//			blocks = newPlane;
//
//			startPos = player.transform.position;
//		}
//	}
}
