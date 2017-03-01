using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parallaxCamera : MonoBehaviour {

	public float parallaxSpeed = 10f;
	public ScrollRect scrollRect;

	Vector3 initialPosition;
	// Use this for initialization

	void Start () {
		initialPosition = transform.position;
		scrollRect.horizontalNormalizedPosition = PlayerPrefs.GetFloat ("level-scroll");
		scrolled (scrollRect);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void scrolled(ScrollRect scrollRect)
	{
		float scrollPosition = scrollRect.horizontalNormalizedPosition;
		float nextX = initialPosition.x + scrollPosition * parallaxSpeed;
		Vector3 nextPosition = initialPosition;
		nextPosition.x = nextX;

		transform.position = nextPosition;

		PlayerPrefs.SetFloat ("level-scroll", scrollPosition);
		PlayerPrefs.Save ();
	}
}
