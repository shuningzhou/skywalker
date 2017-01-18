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
	public float width;
	public float thickness;
	public int initalRoadSectionCount;
	public int roadSectionThreshHold;

	public bool turnRight = true;

	public RoadPoint RoadPoint; 

	private RoadSection lastRoadSection;
	public Vector3 currentStartPostion;
	public Vector3 currentStartDirection;
	private bool currentTurnRight;
	private Vector3 totalCircleCenter = Vector3.zero;
	private float totalCircleRadius = 0;
	public float stepLength;

	private RoadPoint firstRoadPoint;
	private RoadPoint previousRoadPoint;

	private bool dropped = false;

	// Use this for initialization
	void Start () 
	{
		float startz = 0f - 10 * stepLength;

		Vector3 previousPoint = new Vector3 (0f, 49.5f, startz);

		for (int i = 1; i < 11; i++) 
		{
			Vector3 currentPoint = new Vector3 (0f, 49.5f, startz + stepLength * i);
			var rd = Instantiate (RoadPoint, Vector3.zero, Quaternion.identity);
			rd.position1 = previousPoint;
			rd.direction1 = Vector3.forward;
			rd.position2 = currentPoint;
			rd.direction2 = Vector3.forward;
			rd.width = width;
			rd.thickness = thickness;

			previousPoint = currentPoint;

			if (i == 1) 
			{
				firstRoadPoint = rd;	
			}

			if (previousRoadPoint) 
			{
				previousRoadPoint.nextRoadPoint = rd;
			}

			previousRoadPoint = rd;
		}
			
		currentStartPostion = startPostion;
		currentStartDirection = startDirection;
		currentTurnRight = turnRight;

		generateRoadSection (initalRoadSectionCount);
	}
	
	// Update is called once per frame
	void Update () {
		if (!dropped)
		{
			firstRoadPoint.drop ();
			dropped = true;
		}
	}

	void generateRoadSection(int count)
	{
		for (int i = 0; i < count; i++) {

			if (lastRoadSection)
			{
				currentStartPostion = lastRoadSection.tailPostion ();
				currentStartDirection = lastRoadSection.tailDirection ();
				currentTurnRight = !lastRoadSection.turnRight;
				totalCircleCenter = lastRoadSection.newTotalCircleCenter;
				totalCircleRadius = lastRoadSection.newTotalCircleRadius;
			}

			var newRoadSection = generateRoadSection ();

			if (previousRoadPoint) {
				previousRoadPoint.nextRoadPoint = newRoadSection.firstRoadPoint;
			}

			previousRoadPoint = newRoadSection.lastRoadPoint;
				
			lastRoadSection = newRoadSection;
		}
	}

	RoadSection generateRoadSection()
	{
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
		newRoadSection.width = width;
		newRoadSection.thickness = thickness;
		newRoadSection.stepLength = stepLength;

		newRoadSection.drawRoad ();

		return newRoadSection;
	}
}
