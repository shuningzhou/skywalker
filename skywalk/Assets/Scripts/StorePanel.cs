using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour {
	public Button stackButton;
	public Button chestButton;
	public Purchaser purchaser;

	// Use this for initialization
	void Start () {
		stackButton.GetComponentInChildren<Text>().text = purchaser.getPriceForStackOfToken ();
		chestButton.GetComponentInChildren<Text>().text = purchaser.getPriceForChestOfToken ();
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
