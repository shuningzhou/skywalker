using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Row : MonoBehaviour {

	public Text rankText;
	public Text nameText;
	public Text scoreText;

	public string rank;
	public string name;
	public string score;

	// Use this for initialization
	void Start () {
		rankText.text = rank;
		nameText.text = name;
		scoreText.text = score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
