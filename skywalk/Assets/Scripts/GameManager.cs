using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameGUI gameGUI;
	private int diamondCount = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playerFailed()
	{
		gameGUI.playedFailed ();
	}

	public void collectedDiamond()
	{
		diamondCount = diamondCount + 1;
		gameGUI.setDiamond (diamondCount);
	}
}
