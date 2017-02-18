using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	public GameObject explosionPrefab;

	public CharacterMovement player;

	float magneticActiveRange = 6.0f;

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

	void FixedUpdate()
	{
		if (player == null) {
			return;
		}
		if (player.MagnetIsActive) 
		{
			float distance = Vector3.Distance (player.gameObject.transform.position, gameObject.transform.position);

			if (distance < magneticActiveRange) 
			{
				Vector3 magnetVector = player.gameObject.transform.position - gameObject.transform.position;
				Vector3 direction = magnetVector / distance;
				Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
				float mag = magneticActiveRange / distance + 0.5f;
				if (mag > 2) {
					mag = 2;
				}
				rb.AddForce (direction * 20 / (distance*distance));
			}
		}
	}

	public virtual void doParticle(Vector3 position)
	{
		Instantiate(explosionPrefab, position, Quaternion.identity);
	}
}
