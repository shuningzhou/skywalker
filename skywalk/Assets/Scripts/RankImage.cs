using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankImage : MonoBehaviour {
	
	public static RankImage Instance = null;

	public Sprite rank1;
	public Sprite rank2;
	public Sprite rank3;
	public Sprite rank4;
	public Sprite rank5;
	public Sprite rank6;
	public Sprite rank7;
	public Sprite rank8;
	public Sprite rank9;
	public Sprite rank10;
	public Sprite rank11;
	public Sprite rank12;
	public Sprite rank13;
	public Sprite rank14;
	public Sprite rank15;
	public Sprite rank16;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake () {

		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
	}

	public Sprite getRankSpriteForScore(float score)
	{
		if (score <= 25f) {
			return rank1;
		} else if (score <= 50f) {
			return rank2;
		} else if (score <= 75f) {
			return rank3;
		} else if (score <= 100f) {
			return rank4;
		} else if (score <= 130f) {
			return rank5;
		} else if (score <= 160f) {
			return rank6;
		} else if (score <= 190f) {
			return rank7;
		} else if (score <= 220f) {
			return rank8;
		} else if (score <= 260f) {
			return rank9;
		} else if (score <= 300f) {
			return rank10;
		} else if (score <= 340f) {
			return rank11;
		} else if (score <= 380f) {
			return rank12;
		} else if (score <= 420f) {
			return rank13;
		} else if (score <= 460f) {
			return rank14;
		} else if (score <= 500f) {
			return rank15;
		} else{
			return rank16;
		} 
	}

	public string getRankTitleForScore(float score)
	{
		if (score <= 100f) {
			return "Recruit";
		}else if (score <= 220f) {
			return "Master";
		} else if (score <= 380f) {
			return "Champion";
		} else{
			return "Legend";
		} 
	}
}
