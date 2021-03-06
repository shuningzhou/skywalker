﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject followTarget;

	public float moveSpeed;
	public float riseSpeed;
	public float rotateSpeed;
	public float maxHeight;
	public float riseDelay;

	private float currentMaxHeight;
	float levelRotateSpeed;

	// Use this for initialization
	void Start () 
	{
		float randomness = Random.Range (0f, 20f);
		currentMaxHeight = maxHeight + randomness;
		levelRotateSpeed = LevelManager.sharedManager.currentLevel.cameraSpeed;
	}

	// Update is called once per frame
	void Update () {
		if (followTarget != null) 
		{
			if (GameManager.sharedManager.gameState == GameManager.GameState.gamefinished) 
			{   
				Vector3 position = transform.position;
				position.y = currentMaxHeight;
				transform.position = Vector3.Lerp(transform.position,
					position, Time.deltaTime * riseSpeed);
				transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
			} else {
				CharacterMovement moveScript = followTarget.GetComponent<CharacterMovement> ();
				Vector3 position = moveScript.getFootPosition ();

				position.x = position.x + 0;
				position.y = position.y + 0;
				position.z = position.z + 0;

				if (moveScript.hasteIsActive || moveScript.GrowthIsActive) {
					position.y = position.y + 4f;
				}

				transform.position = Vector3.Lerp (transform.position,
					position, Time.deltaTime * moveSpeed);

				transform.Rotate(Vector3.down, levelRotateSpeed * Time.deltaTime);
			}
		}
	}
}
