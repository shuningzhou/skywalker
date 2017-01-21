using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameGUI gameGUI;
	public CameraMovement cameraMovement;
	public RoadGenerator roadGenerator;
	public CharacterMovement characterMovement;

	private bool gameOver = false;
	private int diamondCount = 0;

	// Use this for initialization
	void Awake()
	{
	}

	void Start () 
	{
		gameStart ();
	}

	public void gameStart()
	{
		StartCoroutine (doStart ());
	}

	IEnumerator doStart()
	{
		yield return new WaitForSeconds(1);
		roadGenerator.doGameStart ();
		characterMovement.doGameStart ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public bool isGameOver()
	{
		return gameOver;
	}

	public void playerFailed()
	{
		gameOver = true;
		cameraMovement.playerFailed ();
		gameGUI.playerFailed ();
	}

	public void collectedDiamond()
	{
		diamondCount = diamondCount + 1;
		gameGUI.setDiamond (diamondCount);
	}
}
