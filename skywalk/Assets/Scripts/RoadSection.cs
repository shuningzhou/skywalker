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

	public Vector3 totalCircleCenter;
	public float totalCircleRadius;
	public Vector3 newTotalCircleCenter;
	public float newTotalCircleRadius;

	private int length;
	private int radius;
	private Vector3 startNormal;
	private Vector3 center;
	private float perimeter;
	private float cordinatesRad;
	private Vector3 tp;
	private Vector3 td;
	private bool acutallyTurnedRight;

	public void drawRoad()
	{
		float angle = angleBetweenVectors (startDirectrion, (startPostion - totalCircleCenter));
		float degreeAngle = 360 * (angle / (Mathf.PI * 2));

		if (degreeAngle > 90) 
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
			
		if (acutallyTurnedRight)
		{
			startNormal = new Vector3 (startDirectrion.z, startDirectrion.y, -startDirectrion.x);
		}
		else
		{
			startNormal = new Vector3 (-startDirectrion.z, startDirectrion.y, startDirectrion.x);
		}

		Debug.DrawRay (startPostion, startDirectrion * 3, Color.blue, 100);
		Debug.DrawRay (startPostion, startNormal * 3, Color.red, 100);


		center = new Vector3 (	startPostion.x + radius * startNormal.x,
								startPostion.y + radius * startNormal.y, 
								startPostion.z + radius * startNormal.z);


//		GameObject c = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//		c.transform.position = center;
		Debug.Log (center);
		Debug.Log ("Radius = " + radius);
		Debug.Log ("Length = " + length);
//		c.transform.localScale = new Vector3 (2, 2, 2);


//		GameObject s = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//		s.transform.position = startPostion;
//		s.transform.localScale = new Vector3 (3, 3, 3);

		perimeter = 2 * Mathf.PI * radius;

		cordinatesRad = Mathf.Atan (startDirectrion.x/startDirectrion.z);

		if (startDirectrion.z < 0 && startDirectrion.x > 0) {
			cordinatesRad = cordinatesRad + Mathf.PI;
		}

		if (startDirectrion.x < 0 && startDirectrion.z < 0) {
			cordinatesRad = cordinatesRad + Mathf.PI;
		}

		for (float i = 0; i < length; i = i + 0.2f)
		{
			tp = calculateRoadPoint(i);
			GameObject r = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
			r.transform.position = tp;
			r.transform.localScale = new Vector3 (1.5f, 0.5f, 1.5f);
			r.tag = "road";
			Rigidbody rigidBody = r.AddComponent<Rigidbody>();
			rigidBody.isKinematic = true;
//			if (degreeAngle > 90) {
//				r.GetComponent<Renderer>().material.color = new Color(0, 255, 0); 
//			}
		}

		if (acutallyTurnedRight) {
			td = Quaternion.AngleAxis (360 * length / perimeter, Vector3.up) * startDirectrion;
		} else {
			td = Quaternion.AngleAxis (-360 * length / perimeter, Vector3.up) * startDirectrion;
		}

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

		Debug.Log (td);
		Debug.Log (tp);
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
		Debug.Log("MaxRadius = "+ maxRadius);
		Debug.Log("MinRadius = "+ minRadius);
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



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
