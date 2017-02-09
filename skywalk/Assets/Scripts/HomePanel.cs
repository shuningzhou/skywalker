using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : SOPanel {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void potionButtonPressed()
	{
		Debug.Log ("Potion button Pressed");
		PanelManager.sharedManager.showPanel (PanelManager.sharedManager.potionPanel);
	}

	public void questButtonPressed()
	{
		Debug.Log ("Quest button Pressed");
		PanelManager.sharedManager.showPanel (PanelManager.sharedManager.questPanel);
	}

	public void storeButtonPressed()
	{
		Debug.Log ("Store button Pressed");
		PanelManager.sharedManager.showPanel (PanelManager.sharedManager.storePanel);
	}
}
