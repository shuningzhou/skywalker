using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : SOPanel {
	
	public Button Button1;
	public Button Button2;
	public Button Button3;
	public Button Button4;

	public Purchaser purchaser;

	// Use this for initialization
	void Start () {
		Button1.GetComponentInChildren<Text>().text = purchaser.getPriceForStackOfToken ();
		Button2.GetComponentInChildren<Text>().text = purchaser.getPriceForChestOfToken ();
		Button3.GetComponentInChildren<Text>().text = purchaser.getPriceForChestOfToken ();
		Button4.GetComponentInChildren<Text>().text = purchaser.getPriceForChestOfToken ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void purchaseStackOfTokens()
	{
		purchaser.BuyStackOfToken ();
	}

	public void puchaseChestOfTokens()
	{
		purchaser.BuyChestOfTokens ();
	}
}
