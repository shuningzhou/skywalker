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
	public float maxWidth;
	public float minWidth;

	public float thickness;
	public int initalRoadSectionCount;

	public bool turnRight = true;

	public RoadPoint RoadPoint; 

	private RoadSection lastRoadSection;
	public Vector3 currentStartPostion;
	public Vector3 currentStartDirection;
	private bool currentTurnRight;
	private Vector3 totalCircleCenter = Vector3.zero;
	private float totalCircleRadius = 0;
	public float stepLength;

	private float currentWidth;
	private RoadPoint firstRoadPoint;
	private RoadPoint previousRoadPoint;

	void Awake() {
		GameManager.onGamePlay += onGamePlay;
	}

	void OnDestroy() {
		GameManager.onGamePlay -= onGamePlay;
	}

	// Use this for initialization
	void Start () 
	{
		float startz = 0f - 30 * stepLength;

		Vector3 previousPoint = new Vector3 (0f, 49.5f, startz);

		for (int i = 1; i < 31; i++) 
		{
			Vector3 currentPoint = new Vector3 (0f, 49.5f, startz + stepLength * i);
			var rd = Instantiate (RoadPoint, Vector3.zero, Quaternion.identity);
			rd.position1 = previousPoint;
			rd.direction1 = Vector3.forward;
			rd.position2 = currentPoint;
			rd.direction2 = Vector3.forward;
			rd.width = maxWidth;
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

		generateRoadSections (initalRoadSectionCount);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void onGamePlay ()
	{
		firstRoadPoint.drop ();
	}

	public void generateNextRoadSections()
	{
		generateRoadSections (1);
	}

	void generateRoadSections(int count)
	{
		float widthChange = ((maxWidth - minWidth) / count ) * 5;
		currentWidth = maxWidth;

		for (int i = 0; i < count; i++) 
		{
			if (lastRoadSection)
			{
				currentStartPostion = lastRoadSection.tailPostion ();
				currentStartDirection = lastRoadSection.tailDirection ();
				currentTurnRight = !lastRoadSection.turnRight;
				totalCircleCenter = lastRoadSection.newTotalCircleCenter;
				totalCircleRadius = lastRoadSection.newTotalCircleRadius;
			}
			var newRoadSection = generateRoadSection (currentWidth, currentWidth - widthChange);

			currentWidth = currentWidth - widthChange;

			if (currentWidth <= 1f) 
			{
				widthChange = 0f;
			}

			if (previousRoadPoint) {
				previousRoadPoint.nextRoadPoint = newRoadSection.firstRoadPoint;
			}

			previousRoadPoint = newRoadSection.lastRoadPoint;
				
			lastRoadSection = newRoadSection;
		}
	}

	RoadSection generateRoadSection(float maxWidth, float minWidth)
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
		newRoadSection.maxWidth = maxWidth;
		newRoadSection.minWidth = minWidth;
		newRoadSection.thickness = thickness;
		newRoadSection.stepLength = stepLength;
		newRoadSection.roadGenerator = this;

		newRoadSection.drawRoad ();

		return newRoadSection;
	}
}
