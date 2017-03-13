using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_1 : MonoBehaviour {

	private bool isOverlap = false;
	private float distance;
	private Vector3 newPosition;

	// Use this for initialization
	void Start () {
		if (isOverlap) {
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		Tile tileScript = FindObjectOfType<Tile>();

		if (tileScript.timeToChange)
		{
			CharacterMovement moveScript = FindObjectOfType<CharacterMovement> ();
			newPosition = moveScript.getFootPosition ();
			distance = Mathf.Sqrt (Mathf.Pow (newPosition.x - transform.position.x, 2f) - Mathf.Pow (newPosition.z - transform.position.z, 2f));

			if (distance >= (tileScript.initTileSize / 2)) {
				//Debug.Log ("Building_1 is destroyed because it is too far away from player");
				Destroy (gameObject);
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("building_1 is overlapping");
		isOverlap = true;
	}
}
