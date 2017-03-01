using UnityEngine;
using System.Collections;
using System;

public class CharacterMovement : MonoBehaviour {

	// skill flags
	public bool hasteIsActive = false;
	public bool LeviationIsActive = false;
	public bool GrowthIsActive = false;
	public bool MagnetIsActive = false;

	float rotateSpeed;
	float rotateSpeedChange;
	float maxRotateSpeed;

	float hasteRotateSpeed = 400f;

	public GameObject explosionPrefab;
	public GameObject leftFoot;
	public GameObject rightFoot;

	bool rightInFront = false;
	public float rayReach = 2.0f;
	public bool inTutorial = false;

	bool waveIsVisiable = false;

	public delegate void playerMoved(Vector3 position);
	public static event playerMoved OnPlayerMoved;

	private Vector3 lastPosition;
	private Quaternion lastRotation;

	GameObject wave;

	void Awake () {
		GameManager.onPowerUp += onPowerUp;
	}

	void OnDestroy()
	{
		GameManager.onPowerUp -= onPowerUp;
	}

	void Start()
	{
		this.rotateSpeed = LevelManager.sharedManager.currentLevel.initialRotateSpeed;
		this.rotateSpeedChange = LevelManager.sharedManager.currentLevel.rotateSpeedChange;
		this.maxRotateSpeed = LevelManager.sharedManager.currentLevel.maxRotateSpeed;
		wave = Instantiate(explosionPrefab, Vector3.zero, explosionPrefab.transform.rotation);
	}

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

	// Update is called once per frame
	void Update () 
	{
		if (GameManager.sharedManager.gameState == GameManager.GameState.playing) {
			checkRoadCollapsed ();
			checkForMobile ();
			checkForKeyboard ();
			keepRotating ();
		} 
		else if (GameManager.sharedManager.gameState == GameManager.GameState.tutorial) 
		{
			if (inTutorial) {

			} else {
				keepRotating ();
			}
		}

		if (!LeviationIsActive && waveIsVisiable) 
		{
			wave.transform.position = Vector3.zero;
			waveIsVisiable = false;
		}
	}

	public void excuateInSeconds(Action action, float seconds)
	{
		StartCoroutine (delayStart(seconds, action));
	}

	IEnumerator delayStart(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action ();
	}

	void checkRoadCollapsed()
	{
		RaycastHit footHit;
		if (!(Physics.Raycast (getFootPosition(), Vector3.down, out footHit, rayReach) && (footHit.transform.tag == "road")) && !LeviationIsActive)
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

	float getCurrentRotateSpeed()
	{
		if (hasteIsActive) {
			return hasteRotateSpeed;
		} else {
			return rotateSpeed;
		}
	}

	void keepRotating()
	{
		if (rightInFront) {
			//rotate around the right foot
			transform.RotateAround (rightFoot.transform.position, Vector3.down, getCurrentRotateSpeed() * Time.deltaTime);

		} else {

			//rotate around the right foot
			transform.RotateAround (leftFoot.transform.position, Vector3.up, getCurrentRotateSpeed() * Time.deltaTime);

		}
	}

	void onPowerUp()
	{
		Vector3 scale = transform.localScale;
		scale.z = scale.z + 0.3f;
		transform.localScale = scale;
//		transform.position += new Vector3( 0f, 0f, 6 * 0.5f ); 
	}

	public void doTurn() {
		rightInFront = !rightInFront;
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.moved);

		lastPosition = gameObject.transform.position;
		lastRotation = gameObject.transform.rotation;

		if (OnPlayerMoved != null)
		{
			OnPlayerMoved (getFootPosition());
		}

		if (rotateSpeed < maxRotateSpeed) 
		{
			rotateSpeed = rotateSpeed + rotateSpeedChange;
		}

		if (LeviationIsActive) {
			doParticle (getFootPosition ());
		} else {
			
		}
	}

	public bool canRevive()
	{
		RaycastHit movingHit;
		Vector3 lastP = lastPosition;
		lastP.y = 70.7f;
		Debug.DrawRay (lastP, Vector3.down, Color.red);

		if ((Physics.Raycast (lastP, Vector3.down, out movingHit, rayReach) && (movingHit.transform.tag == "road")))
		{
			return true;
		} 
		else {
			return false;
		}
	}

	public void revive()
	{
		Rigidbody body = GetComponent<Rigidbody> ();
		body.useGravity = false;
		body.isKinematic = true;


		Vector3 scale = gameObject.transform.localScale;

		scale.x = 0.5f;
		scale.z = 2f;
		scale.y = 2f;

		gameObject.transform.localScale = scale;

		lastPosition.y = 70.7f;

		gameObject.transform.localScale = scale;
		gameObject.transform.position = lastPosition;
		gameObject.transform.rotation = lastRotation;
	}

	void doFailed()
	{
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

		GameManager.sharedManager.playerFailed ();
	}

	void checkFailed()
	{
		RaycastHit movingHit;
		Debug.DrawRay (getFootPosition (), Vector3.down, Color.red);

		if ((Physics.Raycast (getMovingPosition(), Vector3.down, out movingHit, rayReach) && (movingHit.transform.tag == "road")) || LeviationIsActive)
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
		float randomDistance = UnityEngine.Random.Range(0.3f, 0.7f);
		randomDistance = 1f + randomDistance;
		return randomDistance;
	}

	public void doParticle(Vector3 position)
	{
		position.y = position.y - 0.2f;
		wave.transform.position = position;
		waveIsVisiable = true;
	}
}