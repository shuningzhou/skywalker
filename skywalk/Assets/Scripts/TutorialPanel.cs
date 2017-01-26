using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialPanel : MonoBehaviour {

	public Image tapImage;
	private float alphaChangeSpeed = 1f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (tapImage.color.a >= 0.95f) {
			tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, 0f);
		} else {
			tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, tapImage.color.a + alphaChangeSpeed * Time.deltaTime);
		}
	}

}
