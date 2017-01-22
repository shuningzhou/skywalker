using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App42Helper : MonoBehaviour {
	
	public static App42Helper Instance = null;

	// Use this for initialization
	void Start()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}

	string createUserName()
	{
		return "Guest";
	}

	string createUserPassword()
	{
		return "Guestpw";
	}

	void createGuestUser()
	{
		
	}

	void uploadScoreForUser(float score)
	{
	}

	void getTop5Score()
	{
	}
}
