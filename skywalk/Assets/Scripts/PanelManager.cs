﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

	public static PanelManager sharedManager = null;

	public HomePanel homePanel;
	public StorePanel storePanel;
	public PotionPanel potionPanel;
	public QuestPanel questPanel;

	void Awake()
	{
		if (sharedManager == null) 
		{
			sharedManager = this;
		} else if (sharedManager != this) {
			Destroy(gameObject);
			return;
		}
		Debug.Log ("Panel manager awake");
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () 
	{
		
	}



	public void showPanel(SOPanel panel)
	{
		panel.show (true);
	}

	public void dismissPanel(SOPanel panel)
	{
		panel.dismiss (true);
	}
}
