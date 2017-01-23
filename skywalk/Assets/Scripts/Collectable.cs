using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	public GameObject explosionPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int speed = Random.Range (10, 200);
		transform.Rotate (Vector3.up, speed * Time.deltaTime);
		transform.Rotate (Vector3.left, speed * Time.deltaTime);
		transform.Rotate (Vector3.forward, speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision) {
		ContactPoint contact = collision.contacts[0];
		Vector3 pos = contact.point;
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.collected);

		doParticle (pos);

		GameManager.sharedManager.collectedRed ();

		gameObject.SetActive (false);
	}

	void doParticle(Vector3 position)
	{
		Instantiate(explosionPrefab, position, Quaternion.identity);
	}
}
