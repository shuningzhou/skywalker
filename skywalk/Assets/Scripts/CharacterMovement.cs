using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public float rotateSpeed = 250.0f;
	public GameObject leftFoot;
	public GameObject rightFoot;
	bool rightInFront = false;

	public Vector3 getFootPosition()
	{
		if (rightFoot != null && leftFoot != null) {
			if (rightInFront) {
				return rightFoot.transform.position;
			} else {
				return leftFoot.transform.position;
			}
		} else {
			return Vector3.zero;
		}
	}
	// Use this for initialization
	void Start () {
	}

	void doTurn() {
		rightInFront = !rightInFront;
	}

	// Update is called once per frame
	void Update () {
		if (rightFoot != null && leftFoot != null) {
			if (Input.GetButtonUp ("Horizontal")) {
				doTurn ();
			}

			if (rightInFront) {

				//rotate around the right foot
				transform.RotateAround (rightFoot.transform.position, Vector3.down, rotateSpeed * Time.deltaTime);

			} else {

				//rotate around the right foot
				transform.RotateAround (leftFoot.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);

			}
		}
	}


}