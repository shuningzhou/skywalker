using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialPanel : MonoBehaviour {

	public Image tapImage;
	private float alphaChangeSpeed = 0.05f;
	// Use this for initialization
	void Start () {
		StartBlinking();
	}
	
	// Update is called once per frame
	void Update () {

		if (tapImage.color.a >= 0.5f) {
			tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, 0.1f);
		} else {
			tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, tapImage.color.a + alphaChangeSpeed * Time.deltaTime);
		}
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
			case "0.2":
				tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, 0.5f);
				yield return new WaitForSeconds(0.5f);
				break;
			case "0.5":
				tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, 0.2f);
				yield return new WaitForSeconds(0.5f);
				break;
			default:
				tapImage.color = new Color(tapImage.color.r, tapImage.color.g, tapImage.color.b, 0.2f);
				yield return new WaitForSeconds(0.5f);
				break;
			}
		}
	}
}
