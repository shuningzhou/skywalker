using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	public GameObject explosionPrefab;

	public bool rotateY = true;
	public bool rotateX = true;
	public bool rotateZ = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int speed = Random.Range (10, 200);
		if (rotateY) {
			transform.Rotate (Vector3.up, speed * Time.deltaTime);
		}

		if (rotateZ) {
			transform.Rotate (Vector3.left, speed * Time.deltaTime);
		}

		if (rotateX) {
			transform.Rotate (Vector3.forward, speed * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision collision) {
		ContactPoint contact = collision.contacts[0];
		Vector3 pos = contact.point;

		doParticle (pos);
		onCollision (pos);

		gameObject.SetActive (false);
	}

	public virtual void onCollision(Vector3 position)
	{

	}

	public virtual void doParticle(Vector3 position)
	{
		Instantiate(explosionPrefab, position, Quaternion.identity);
	}
}
