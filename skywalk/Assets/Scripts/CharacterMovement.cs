﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public float rotateSpeed = 250.0f;
	public GameObject leftFoot;
	public GameObject rightFoot;

	bool rightInFront = false;
	bool failed = false;
	float rayReach = 2.0f;

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

	public Vector3 getMovingPosition()
	{
		if (rightFoot != null && leftFoot != null) {
			if (rightInFront) {
				return leftFoot.transform.position;
			} else {
				return rightFoot.transform.position;
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
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.moved);
	}

	// Update is called once per frame
	void Update () {
		if (rightFoot != null && leftFoot != null) {
			RaycastHit footHit;
			if (!(Physics.Raycast (getFootPosition(), Vector3.down, out footHit, rayReach) && (footHit.transform.tag == "road"))&& failed == false)
			{
				Rigidbody body = GetComponent<Rigidbody> ();
				body.useGravity = true;
				body.isKinematic = false;
				failed = true;
				if (rightInFront) {
					body.AddRelativeTorque (Vector3.forward * 200);
					body.AddRelativeTorque (Vector3.down * 250);

				} else {
					body.AddRelativeTorque (Vector3.forward * 200);
					body.AddRelativeTorque (Vector3.up * 250);
				}
				body.AddForce (Vector3.down * 100);
			}

			if (Input.GetButtonUp ("Horizontal") && failed == false) 
			{
				RaycastHit movingHit;
				Debug.DrawRay (getFootPosition (), Vector3.down, Color.red);

				if (Physics.Raycast (getMovingPosition(), Vector3.down, out movingHit, rayReach) && (movingHit.transform.tag == "road"))
				{
					doTurn ();
				} 
				else {
					Rigidbody body = GetComponent<Rigidbody> ();
					body.useGravity = true;
					body.isKinematic = false;
					failed = true;
					if (rightInFront) {
						body.AddRelativeTorque (Vector3.forward * 200);
						body.AddRelativeTorque (Vector3.down * 250);

					} else {
						body.AddRelativeTorque (Vector3.forward * 200);
						body.AddRelativeTorque (Vector3.up * 250);
					}
					body.AddForce (Vector3.down * 100);
				}
			}

			if (failed == false) {
				if (rightInFront) {

					//rotate around the right foot
					transform.RotateAround (rightFoot.transform.position, Vector3.down, rotateSpeed * Time.deltaTime);

				} else {

					//rotate around the right foot
					transform.RotateAround (leftFoot.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);

				}

//				transform.position = new Vector3(transform.position.x, 52f, transform.position.z);
			} 
		}
	}


}