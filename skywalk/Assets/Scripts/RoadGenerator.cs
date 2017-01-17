using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {
	public RoadSection roadSection;
	public GameObject player;
	public CollectableManager collectableManager;

	public Vector3 startPostion = new Vector3 (0f, 49.5f, 0f);
	public Vector3 startDirection = Vector3.forward;

	public float maxRad = Mathf.PI * 3 / 4;
	public float minRad = Mathf.PI / 10;
	public int maxRadius = 3;
	public int minRadius = 2;

	public bool turnRight = true;

	private RoadSection lastRoadSection;
	public Vector3 currentStartPostion;
	public Vector3 currentStartDirection;
	private bool currentTurnRight;
	private Vector3 totalCircleCenter = Vector3.zero;
	private float totalCircleRadius = 0;
	// Use this for initialization
	void Start () {

		for (int i = 0; i < 6; i++) {

			Debug.Log ("Drawing..." + i);

			if (i == 0) {
				currentStartPostion = startPostion;
				currentStartDirection = startDirection;
				currentTurnRight = turnRight;
			} else {
				currentStartPostion = lastRoadSection.tailPostion ();
				currentStartDirection = lastRoadSection.tailDirection ();
				currentTurnRight = !lastRoadSection.turnRight;
				totalCircleCenter = lastRoadSection.newTotalCircleCenter;
				totalCircleRadius = lastRoadSection.newTotalCircleRadius;
			}

			var newRoadSection = Instantiate (roadSection, Vector3.zero, Quaternion.identity);
			newRoadSection.collectableManager = collectableManager;
			newRoadSection.maxRad = maxRad;
			newRoadSection.minRad = minRad;
			newRoadSection.maxRadius = maxRadius;
			newRoadSection.minRadius = minRadius;
			newRoadSection.startPostion = currentStartPostion;
			newRoadSection.startDirectrion = currentStartDirection;
			newRoadSection.turnRight = currentTurnRight;
			newRoadSection.totalCircleCenter = totalCircleCenter;
			newRoadSection.totalCircleRadius = totalCircleRadius;
			newRoadSection.width = 2f;
			newRoadSection.thickness = 2f;
			newRoadSection.stepLength = 0.2f;

			newRoadSection.drawRoad ();

			lastRoadSection = newRoadSection;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
