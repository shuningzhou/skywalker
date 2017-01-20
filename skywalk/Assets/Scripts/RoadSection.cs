using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSection : MonoBehaviour {
	public Vector3 startPostion;
	public Vector3 startDirectrion;
	public float maxRad;
	public float minRad;
	public int maxRadius;
	public int minRadius;
	public bool turnRight;
	public float width;
	public float thickness;

	public Vector3 totalCircleCenter;
	public float totalCircleRadius;
	public Vector3 newTotalCircleCenter;
	public float newTotalCircleRadius;

	public float stepLength;

	public RoadPoint RoadPoint;
	public SectionMark SectionMark;
	public RoadGenerator roadGenerator;

	public RoadPoint firstRoadPoint = null;
	public RoadPoint lastRoadPoint = null;

	public CollectableManager collectableManager;

	private int length;
	private int radius;
	private Vector3 startNormal;
	private Vector3 center;
	private float perimeter;
	private float cordinatesRad;

	private Vector3 currentPoint;
	private Vector3 currentDirection;

	private Vector3 tp;
	private Vector3 td;

	private bool acutallyTurnedRight;

	public void drawRoad()
	{
		prepareAllNecessaryValues ();

		Vector3 previousPoint = Vector3.zero;
		Vector3 previousDirection = Vector3.zero;

		for (float i = 0f; i < length; i = i + stepLength)
		{
			currentPoint = calculateRoadPoint(i);

			if (acutallyTurnedRight) 
			{
				currentDirection = Quaternion.AngleAxis (360 * i / perimeter, Vector3.up) * startDirectrion;
			} else {
				currentDirection = Quaternion.AngleAxis (-360 * i / perimeter, Vector3.up) * startDirectrion;
			}
				
			collectableManager.moved (stepLength, currentPoint);

			if (i > 0) 
			{
				var rd = Instantiate (RoadPoint, Vector3.zero, Quaternion.identity);
				rd.position1 = previousPoint;
				rd.direction1 = previousDirection;
				rd.position2 = currentPoint;
				rd.direction2 = currentDirection;
				rd.width = width;
				rd.thickness = thickness;

				if (lastRoadPoint) 
				{
					lastRoadPoint.nextRoadPoint = rd;
				}

				lastRoadPoint = rd;

				if (firstRoadPoint == null) 
				{
					firstRoadPoint = rd;
				}
			}
				
			previousPoint = currentPoint;
			previousDirection = currentDirection;
		}

		Vector3 markPosition = new Vector3 (currentPoint.x, currentPoint.y + 1, currentPoint.z);
		var mark = Instantiate (SectionMark, markPosition, Quaternion.identity);
		mark.rd = roadGenerator;

		calculateValuesForNextSection ();
	}

	void prepareAllNecessaryValues()
	{
		// check if current direction is towards the existing road
		float angle = angleBetweenVectors (startDirectrion, (startPostion - totalCircleCenter));
		float degreeAngle = 360 * (angle / (Mathf.PI * 2));

		if (degreeAngle > 90) //use a small road to turn ourwards
		{
			radius = 5;
			length = (int)(radius * angle);
			acutallyTurnedRight = turnRight;
		} 
		else 
		{
			acutallyTurnedRight = turnRight;
			randomValues ();
		}

		// calculate normal of the start direction
		startNormal = getRightNormal(startDirectrion, acutallyTurnedRight);

		Debug.DrawRay (startPostion, startDirectrion * 3, Color.blue, 100);
		Debug.DrawRay (startPostion, startNormal * 3, Color.red, 100);

		// calculate circle center of the road
		center = new Vector3 (	startPostion.x + radius * startNormal.x,
			startPostion.y + radius * startNormal.y, 
			startPostion.z + radius * startNormal.z);


		//		GameObject c = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		//		c.transform.position = center;
//		Debug.Log (center);
//		Debug.Log ("Radius = " + radius);
//		Debug.Log ("Length = " + length);
		//		c.transform.localScale = new Vector3 (2, 2, 2);


		//		GameObject s = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		//		s.transform.position = startPostion;
		//		s.transform.localScale = new Vector3 (3, 3, 3);

		perimeter = 2 * Mathf.PI * radius;

		// calculate rotation of the coordinates
		cordinatesRad = Mathf.Atan (startDirectrion.x/startDirectrion.z);

		if (startDirectrion.z < 0 && startDirectrion.x > 0) {
			cordinatesRad = cordinatesRad + Mathf.PI;
		}

		if (startDirectrion.x < 0 && startDirectrion.z < 0) {
			cordinatesRad = cordinatesRad + Mathf.PI;
		}
	}

	void calculateValuesForNextSection()
	{
		// start point of the next road
		tp = currentPoint;
		// start dirction of the next road
		td = currentDirection;

		// update the large circle that includes all the existing roads
		if (turnRight && acutallyTurnedRight) 
		{
			newTotalCircleRadius = totalCircleRadius;
			newTotalCircleCenter = totalCircleCenter;
		} 
		else 
		{
			newTotalCircleRadius = totalCircleRadius + radius;
			float moveRatio = radius / newTotalCircleRadius;
			Vector3 centerMove = new Vector3 (center.x - totalCircleCenter.x, center.y - totalCircleCenter.y, center.z - totalCircleCenter.z);
			Vector3 centerMoveTimesRatio = centerMove * moveRatio;
			newTotalCircleCenter = totalCircleCenter + centerMoveTimesRatio;
		}
		Debug.DrawRay (tp, td * 10, Color.cyan, 100);
	}

	float angleBetweenVectors(Vector3 a, Vector3 b)
	{
		float dotProduct = a.x * b.x + a.y * b.y + a.z * b.z;
		float magnitudeA = Mathf.Sqrt( a.x * a.x + a.y * a.y + a.z * a.z);
		float magnitudeB = Mathf.Sqrt( b.x * b.x + b.y * b.y + b.z * b.z);
		float degree = Mathf.Acos (dotProduct / (magnitudeA * magnitudeB));
		return degree;
	}

	public Vector3 tailPostion()
	{
		return tp;
	}

	public Vector3 tailDirection()
	{
		return td;
	}

	void randomValues()
	{
		float rad = Random.Range (minRad, maxRad);
		radius = Random.Range (minRadius, maxRadius);
		length = (int)(radius * rad);
	}

	Vector3 calculateRoadPoint(float step)
	{
		float rad = 2f * Mathf.PI * step / perimeter;

		if (acutallyTurnedRight) {
			rad = Mathf.PI - rad;
		}

		float dz = Mathf.Sin (rad) * radius;
		float dx = Mathf.Cos (rad) * radius;

		float x = dx * Mathf.Cos (-cordinatesRad) - dz * Mathf.Sin (-cordinatesRad);
		float z = dx * Mathf.Sin (-cordinatesRad) + dz * Mathf.Cos (-cordinatesRad);

		float result_x = center.x + x;
		float result_z = center.z + z;

		Vector3 result = new Vector3 (result_x, startPostion.y, result_z);

		return result;
	}

	Vector3 getRightNormal(Vector3 d, bool right)
	{
		if (right)
		{
			return new Vector3 (d.z, d.y, -d.x);
		}
		else
		{
			return new Vector3 (-d.z, d.y, d.x);
		} 
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
