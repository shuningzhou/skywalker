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
		Button1.GetComponentInChildren<Text>().text = purchaser.getPriceForTwoHundredsToken ();
		Button2.GetComponentInChildren<Text>().text = purchaser.getPriceForStackOfToken ();
		Button3.GetComponentInChildren<Text>().text = purchaser.getPriceForBagOfTokens ();
		Button4.GetComponentInChildren<Text>().text = purchaser.getPriceForChestOfToken ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void purchaseStackOfTokens()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		purchaser.BuyStackOfToken ();
	}

	public void puchaseChestOfTokens()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		purchaser.BuyChestOfTokens ();
	}

	public void puchaseBagOfTokens ()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		purchaser.BuyBagOfTokens ();
	}

	public void purchase200Tokens()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		purchaser.BuyTwoHundredsToken ();
	}
}
