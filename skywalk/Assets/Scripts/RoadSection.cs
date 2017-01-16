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

	private Mesh mesh;
	private List<Vector3> points = new List<Vector3>();
	private List<Vector3> vertices = new List<Vector3>();
	private List<int> triangles = new List<int>();

	public void drawRoad()
	{
		prepareAllNecessaryValues ();

		MeshFilter filter = gameObject.GetComponent<MeshFilter>(); 
		//MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>(); 
		MeshCollider collier = gameObject.AddComponent<MeshCollider>();

		if (filter.sharedMesh == null)
		{
			filter.sharedMesh = new Mesh ();
		}
			
		mesh = filter.sharedMesh;
		mesh.Clear ();


		for (float i = 0f; i < length; i = i + 0.2f)
		{
			currentPoint = calculateRoadPoint(i);

			if (acutallyTurnedRight) 
			{
				currentDirection = Quaternion.AngleAxis (360 * i / perimeter, Vector3.up) * startDirectrion;
			} else {
				currentDirection = Quaternion.AngleAxis (-360 * i / perimeter, Vector3.up) * startDirectrion;
			}

			addRoadPoint (currentPoint, currentDirection);

//			GameObject r = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
//			r.transform.position = currentPoint;
//			r.transform.localScale = new Vector3 (1.5f, 0.5f, 1.5f);
//			r.tag = "road";
//			Rigidbody rigidBody = r.AddComponent<Rigidbody>();
//			rigidBody.isKinematic = true;
//			if (degreeAngle > 90) {
//				r.GetComponent<Renderer>().material.color = new Color(0, 255, 0); 
//			}
		}

		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();

		Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
		rigidBody.isKinematic = true;

		mesh.RecalculateNormals ();

		collier.sharedMesh = mesh;

		calculateValuesForNextSection ();
	}

	void addRoadPoint(Vector3 point, Vector3 direction)
	{
		Debug.Log ("Adding: " + point + " " + direction);

		float halfWidth = width / 2;

		Vector3 rightNormal = getRightNormal (direction, true);
		Vector3 leftNormal = getRightNormal (direction, false);

		Vector3 rightPoint = new Vector3 (point.x + halfWidth * rightNormal.x,
			point.y + halfWidth * rightNormal.y, 
			point.z + halfWidth * rightNormal.z);
		Vector3 leftPoint = new Vector3 (point.x + halfWidth * leftNormal.x,
			point.y + halfWidth * leftNormal.y, 
			point.z + halfWidth * leftNormal.z);

		Vector3 rightBottomPoint = new Vector3 (rightPoint.x, rightPoint.y - thickness, rightPoint.z);
		Vector3 leftBottomPoint = new Vector3 (leftPoint.x, leftPoint.y - thickness, leftPoint.z);

		points.Add (leftBottomPoint);
		points.Add (leftPoint);

		points.Add (rightBottomPoint);
		points.Add (rightPoint);

		//Draw Body
		if (points.Count >= 8) 
		{
			int start = points.Count - 8;
			int cubeCount = points.Count / 4 - 1 - 1;
			Debug.Log ("Start = " + start);

			Vector3 p0 = points [start + 0];
			Vector3 p1 = points [start + 1];
			Vector3 p2 = points [start + 2];
			Vector3 p3 = points [start + 3];
			Vector3 p4 = points [start + 4];
			Vector3 p5 = points [start + 5];
			Vector3 p6 = points [start + 6];
			Vector3 p7 = points [start + 7];

			//left
			vertices.Add(p4);//0
			vertices.Add(p5);//1
			vertices.Add(p1);//2
			vertices.Add(p0);//3
			//right
			vertices.Add(p2);//4
			vertices.Add(p3);//5
			vertices.Add(p7);//6
			vertices.Add(p6);//7
			//front
			vertices.Add(p0);//8
			vertices.Add(p1);//9
			vertices.Add(p3);//10
			vertices.Add(p2);//11
			//back
			vertices.Add(p6);//12
			vertices.Add(p7);//13
			vertices.Add(p5);//14
			vertices.Add(p4);//15
			//top
			vertices.Add(p1);//16
			vertices.Add(p5);//17
			vertices.Add(p7);//18
			vertices.Add(p3);//19
			//bot
			vertices.Add(p2);//20
			vertices.Add(p6);//21
			vertices.Add(p4);//22
			vertices.Add(p0);//23

			//left
			triangles.Add(0 + 24 * cubeCount);
			triangles.Add(1 + 24 * cubeCount);
			triangles.Add(2 + 24 * cubeCount);
			triangles.Add(2 + 24 * cubeCount);
			triangles.Add(3 + 24 * cubeCount);
			triangles.Add(0 + 24 * cubeCount);
			//right
			triangles.Add(4 + 24 * cubeCount);
			triangles.Add(5 + 24 * cubeCount);
			triangles.Add(6 + 24 * cubeCount);
			triangles.Add(6 + 24 * cubeCount);
			triangles.Add(7 + 24 * cubeCount);
			triangles.Add(4 + 24 * cubeCount);
			//front
			triangles.Add(8 + 24 * cubeCount);
			triangles.Add(9 + 24 * cubeCount);
			triangles.Add(10 + 24 * cubeCount);
			triangles.Add(10 + 24 * cubeCount);
			triangles.Add(11 + 24 * cubeCount);
			triangles.Add(8 + 24 * cubeCount);
			//back
			triangles.Add(12 + 24 * cubeCount);
			triangles.Add(13 + 24 * cubeCount);
			triangles.Add(14 + 24 * cubeCount);
			triangles.Add(14 + 24 * cubeCount);
			triangles.Add(15 + 24 * cubeCount);
			triangles.Add(12 + 24 * cubeCount);
			//top
			triangles.Add(16 + 24 * cubeCount);
			triangles.Add(17 + 24 * cubeCount);
			triangles.Add(18 + 24 * cubeCount);
			triangles.Add(18 + 24 * cubeCount);
			triangles.Add(19 + 24 * cubeCount);
			triangles.Add(16 + 24 * cubeCount);
			//bot
			triangles.Add(20 + 24 * cubeCount);
			triangles.Add(21 + 24 * cubeCount);
			triangles.Add(22 + 24 * cubeCount);
			triangles.Add(22 + 24 * cubeCount);
			triangles.Add(23 + 24 * cubeCount);
			triangles.Add(20 + 24 * cubeCount);
		}
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
		Debug.Log (center);
		Debug.Log ("Radius = " + radius);
		Debug.Log ("Length = " + length);
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

		Debug.Log (td);
		Debug.Log (tp);
		Debug.DrawRay (tp, td * 10, Color.cyan, 100);
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
