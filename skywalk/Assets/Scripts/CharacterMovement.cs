using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public float rotateSpeed = 250.0f;
	public GameObject leftFoot;
	public GameObject rightFoot;
	public GameManager gameManager;

	bool rightInFront = false;
	float rayReach = 2.0f;
	bool gameRunning = false;

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

	// Update is called once per frame
	void Update () {

		if (!gameRunning) 
		{
			return;
		}
			
		checkRoadCollapsed ();
		checkForMobile ();
		checkForKeyboard ();
		keepRotating ();
	}

	void checkRoadCollapsed()
	{
		RaycastHit footHit;
		if (!(Physics.Raycast (getFootPosition(), Vector3.down, out footHit, rayReach) && (footHit.transform.tag == "road")))
		{
			doFailed ();
		}
	}

	void checkForMobile()
	{
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Ended) {
				checkFailed ();
			}
		}
	}

	void checkForKeyboard()
	{
		if (Input.GetButtonUp ("Horizontal")) 
		{
			checkFailed ();
		}
	}

	void keepRotating()
	{
		if (rightInFront) 
		{
			//rotate around the right foot
			transform.RotateAround (rightFoot.transform.position, Vector3.down, rotateSpeed * Time.deltaTime);

		} else {

			//rotate around the right foot
			transform.RotateAround (leftFoot.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);

		}
	}

	public void doGameStart ()
	{
		gameRunning = true;
	}

	public void doGamePaused()
	{
		gameRunning = false;
	}

	public void doGameResume()
	{
		gameRunning = true;
	}

	public void doGameEnd()
	{
		gameRunning = false;
	}


	void doTurn() {
		rightInFront = !rightInFront;
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.moved);
		float movedDistance = calculateDistance ();
		gameManager.playerMoved (movedDistance);
	}

	void doFailed()
	{
		gameManager.append ("doFailed");
		Rigidbody body = GetComponent<Rigidbody> ();
		body.useGravity = true;
		body.isKinematic = false;
		if (rightInFront) {
			body.AddRelativeTorque (Vector3.forward * 200);
			body.AddRelativeTorque (Vector3.down * 250);

		} else {
			body.AddRelativeTorque (Vector3.forward * 200);
			body.AddRelativeTorque (Vector3.up * 250);
		}
		body.AddForce (Vector3.down * 100);
		gameManager.append ("doFailed1");
		gameManager.playerFailed (false);
	}

	void checkFailed()
	{
		RaycastHit movingHit;
		Debug.DrawRay (getFootPosition (), Vector3.down, Color.red);

		if (Physics.Raycast (getMovingPosition(), Vector3.down, out movingHit, rayReach) && (movingHit.transform.tag == "road"))
		{
			doTurn ();
		} 
		else {
			doFailed ();
		}
	}

	float calculateDistance ()
	{
//		Vector3 newPosition = getFootPosition ();
//		Debug.Log (newPosition);
//		Debug.Log (lastPosition);
//		float distance = Mathf.Sqrt((newPosition.z - lastPosition.z) * (newPosition.z - lastPosition.z) + (newPosition.x - lastPosition.x) * (newPosition.x - lastPosition.x));
//		return distance/2f;
		return 1.5f;
	}
}