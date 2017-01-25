using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_1 : MonoBehaviour {

	private bool isOverlap = false;

	// Use this for initialization
	void Start () {
		if (isOverlap) {
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log ("building_1 is overlapping");
		isOverlap = true;
	}
}
