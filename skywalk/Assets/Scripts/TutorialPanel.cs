using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialPanel : MonoBehaviour {

	public Image tapImage;

	// Use this for initialization
	void Start () {
		StartBlinking();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartBlinking()
	{
		StopAllCoroutines();
		StartCoroutine("Blink");
	}

	void StopBlinking()
	{
		StopAllCoroutines();
	}

	IEnumerator Blink()
	{
		while (true)
		{
			switch(tapImage.color.a.ToString())
			{
			case "0":
				tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, 0.5f);
				//Play sound
				yield return new WaitForSeconds(0.5f);
				break;
			case "1":
				tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, 0.2f);
				//Play sound
				yield return new WaitForSeconds(0.5f);
				break;
			}
		}
	}
}
