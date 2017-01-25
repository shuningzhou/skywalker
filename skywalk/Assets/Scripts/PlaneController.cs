using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

	private List<SOPlane> planes = new List<SOPlane>();

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showPlane (SOPlane plane)
	{
		var p = Instantiate (plane, Vector3.zero, Quaternion.identity);

		var lastP = planes[planes.Count - 1];
		lastP.gameObject.SetActive (false);
	
		planes.Add (p);
	}

	public void popPlane ()
	{
		var lastP = planes[planes.Count - 1];
		planes.Remove (lastP);
		Destroy (lastP.gameObject);

		lastP = planes[planes.Count - 1];
		lastP.gameObject.SetActive (true);
	}
}
