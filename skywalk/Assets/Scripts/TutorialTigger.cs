using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTigger : MonoBehaviour {

	public bool isLastTigger = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (!GameManager.sharedManager.isOnLastTutorialTrigger) 
		{
			GameManager.sharedManager.tutorialTriggered (isLastTigger);
		}

		Destroy (gameObject);
	}
}
