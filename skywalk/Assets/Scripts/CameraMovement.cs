using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject followTarget;
	public float moveSpeed;
	public float riseSpeed;
	public float rotateSpeed;
	public float maxHeight;
	public float riseDelay;

	private bool shouldRise = false;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (followTarget != null) {
			
			if (shouldRise) {
				Vector3 position = transform.position;
				position.y = maxHeight;
				transform.position = Vector3.Lerp(transform.position,
					position, Time.deltaTime * riseSpeed);
				transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
			} else {
				CharacterMovement moveScript = followTarget.GetComponent<CharacterMovement> ();
				Vector3 position = moveScript.getFootPosition ();

				position.x = position.x + 0;
				position.y = position.y + 0;
				position.z = position.z + 0;

				transform.position = Vector3.Lerp (transform.position,
					position, Time.deltaTime * moveSpeed);
			}
		}
	}

	public void playerFailed()
	{
		StartCoroutine (doRise ());
	}

	IEnumerator doRise()
	{
		yield return new WaitForSeconds(riseDelay);
		shouldRise = true;
	}
}
