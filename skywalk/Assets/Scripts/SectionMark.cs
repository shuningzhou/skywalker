using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionMark : MonoBehaviour {
	public RoadGenerator rd;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) 
	{
		//rd.generateNextRoadSections ();
		Destroy (gameObject);
	}
}
