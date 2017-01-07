using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject followTarget;
	public float moveSpeed;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (followTarget != null) {
			CharacterMovement moveScript = followTarget.GetComponent<CharacterMovement>();
			Vector3 position = moveScript.getFootPosition();

			position.x = position.x + 0;
			position.y = position.y + 0;
			position.z = position.z + 0;

			transform.position = Vector3.Lerp(transform.position,
				position, Time.deltaTime * moveSpeed);
		}
	}
}
