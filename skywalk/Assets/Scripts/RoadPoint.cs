using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPoint : MonoBehaviour {
	public Vector3 position1;
	public Vector3 direction1;
	public Vector3 position2;
	public Vector3 direction2;
	public float width;
	public float thickness;

	public float dropDelay;
	private bool stopDropped = false;

	public RoadPoint nextRoadPoint;

	private Mesh mesh;
	private List<Vector3> points = new List<Vector3>();
	private List<Vector3> vertices = new List<Vector3>();
	private List<int> triangles = new List<int>();

	// Use this for initialization
	void Start () {
		MeshFilter filter = gameObject.GetComponent<MeshFilter>(); 
		//MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>(); 
		MeshCollider collier = gameObject.AddComponent<MeshCollider>();
		collier.convex = true;

		if (filter.sharedMesh == null)
		{
			filter.sharedMesh = new Mesh ();
		}

		mesh = filter.sharedMesh;
		mesh.Clear ();

		createMesh ();

		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();

		Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
		rigidBody.isKinematic = true;

		mesh.RecalculateNormals ();

		collier.sharedMesh = mesh;
	}

	public void drop()
	{
		GameManager.sharedManager.currentDroppingRoadPoint = this;
		StartCoroutine (doDrop ());
		StartCoroutine (doDestroy ());
	}
		
	public void stopDropping()
	{
		stopDropped = true;
	}

	IEnumerator doDrop()
	{
		yield return new WaitForSeconds(dropDelay);

		if (!stopDropped) 
		{
			Rigidbody body = GetComponent<Rigidbody> ();
			body.useGravity = true;
			body.isKinematic = false;

			if (nextRoadPoint) 
			{
				nextRoadPoint.drop ();
			}
		}
	}

	IEnumerator doDestroy()
	{
		yield return new WaitForSeconds(6);
		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	void createMesh()
	{
		float halfWidth = width / 2;

		Vector3 rightNormal1 = getRightNormal (direction1, true);
		Vector3 leftNormal1 = getRightNormal (direction1, false);

		Vector3 rightPoint1 = new Vector3 (position1.x + halfWidth * rightNormal1.x,
			position1.y + halfWidth * rightNormal1.y, 
			position1.z + halfWidth * rightNormal1.z);
		Vector3 leftPoint1 = new Vector3 (position1.x + halfWidth * leftNormal1.x,
			position1.y + halfWidth * leftNormal1.y, 
			position1.z + halfWidth * leftNormal1.z);

		Vector3 rightBottomPoint1 = new Vector3 (rightPoint1.x, rightPoint1.y - thickness, rightPoint1.z);
		Vector3 leftBottomPoint1 = new Vector3 (leftPoint1.x, leftPoint1.y - thickness, leftPoint1.z);

		points.Add (leftBottomPoint1);
		points.Add (leftPoint1);

		points.Add (rightBottomPoint1);
		points.Add (rightPoint1);

		Vector3 rightNormal2 = getRightNormal (direction2, true);
		Vector3 leftNormal2 = getRightNormal (direction2, false);

		Vector3 rightPoint2 = new Vector3 (position2.x + halfWidth * rightNormal2.x,
			position2.y + halfWidth * rightNormal2.y, 
			position2.z + halfWidth * rightNormal2.z);
		Vector3 leftPoint2 = new Vector3 (position2.x + halfWidth * leftNormal2.x,
			position2.y + halfWidth * leftNormal2.y, 
			position2.z + halfWidth * leftNormal2.z);

		Vector3 rightBottomPoint2 = new Vector3 (rightPoint2.x, rightPoint2.y - thickness, rightPoint2.z);
		Vector3 leftBottomPoint2 = new Vector3 (leftPoint2.x, leftPoint2.y - thickness, leftPoint2.z);

		points.Add (leftBottomPoint2);
		points.Add (leftPoint2);

		points.Add (rightBottomPoint2);
		points.Add (rightPoint2);

		int start = points.Count - 8;
		int cubeCount = points.Count / 4 - 1 - 1;

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
}
