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
		this.gameObject.transform.position = Vector3.zero;
		startPos = player.transform.position;

		float updateTime = Time.realtimeSinceStartup;

		for (int x = -halfBlockX; x < halfBlockX; x++) {
			for (int z = -halfBlockZ; z < halfBlockZ; z++) {
				//Vector3 pos = new Vector3(x * planeSize)
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
