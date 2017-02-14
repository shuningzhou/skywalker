﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {
	public GameObject gem;
	public GameObject coin;

	public GameObject haste;

	public GameManager gameManager;
	public GameObject chest;

	private float collectableDistance;
	private float coinDistance;

	public float collectableSpace = 3;
	public float coinSpace = 10;
	public float floatDistance = 1.5f;
	public int poolSize = 100;

	public int collectableRowCount = 5;
	public float collectableX = 0;
	public float collectableZ = 0;

	private List<GameObject> gems = new List<GameObject> ();
	private List<GameObject> coins = new List<GameObject> ();
	private List<GameObject> hastes = new List<GameObject> ();

	float hasteDropRate = 0.05f;


	void Awake()
	{
		collectableDistance = 0;
		coinDistance = 0;

		Vector3 startPosition = new Vector3 (0, -50, 0);

		for (int i = 0; i < poolSize; i++)
		{
			GameObject c = Instantiate (gem, startPosition, Quaternion.identity);
			c.SetActive(false);
			gems.Add(c);
		}

		for (int i = 0; i < poolSize; i++)
		{
			GameObject c = Instantiate (coin, startPosition, coin.transform.rotation);
			c.SetActive(false);
			coins.Add(c);
		}

		for (int i = 0; i < poolSize; i++)
		{
			GameObject c = Instantiate (haste, startPosition, coin.transform.rotation);
			c.SetActive(false);
			hastes.Add(c);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void moved(float step, Vector3 position, float width)
	{
		collectableDistance = collectableDistance + (step);
		coinDistance = coinDistance + step;

		float w = width / 2;

		if (shouldCreateGem()) 
		{
			Vector3 collectablePosition = new Vector3 (position.x + collectableX, position.y + floatDistance, position.z + collectableZ);
			createCollectableAt (collectablePosition);
			collectableDistance = 0;
			collectableRowCount = collectableRowCount - 1;
			collectableSpace = 2;

			GameManager.sharedManager.totalGemThisRound = GameManager.sharedManager.totalGemThisRound + 1;

			if (collectableRowCount <= 0) 
			{
				collectableX = Random.Range (0, 3f+w);
				collectableZ = Random.Range (0, 3f+w);
				collectableRowCount = Random.Range (1, 10);
				collectableSpace = 5;
			}
		} 

		if (shouldCreateCoin ()) 
		{
			float randX = Random.Range (-3f-w, 3f+w);
			float randZ = Random.Range (-3f-w, 3f+w);

			Vector3 collectablePosition = new Vector3 (position.x + randX, position.y + floatDistance, position.z + randZ);
			createCoinAt (collectablePosition);
			coinDistance = 0;
			coinSpace = Random.Range (50, 60);
		} 

		if (shouldCreateHaste ())
		{
			float randX = Random.Range (-3f-w, 3f+w);
			float randZ = Random.Range (-3f-w, 3f+w);

			Vector3 collectablePosition = new Vector3 (position.x + randX, position.y + floatDistance, position.z + randZ);
			createHasteAt (collectablePosition);
		}
	}

	public bool shouldCreateHaste ()
	{
		float r = Random.Range (0f, 1f);

		if (r < hasteDropRate)
		{
			return true;
		} else {
			return false;
		}
	}

	public void createChest(Vector3 position)
	{
		Vector3 collectablePosition = new Vector3 (position.x, position.y + floatDistance, position.z);
		Instantiate (chest, collectablePosition, Quaternion.identity);
	}

	public bool shouldCreateCoin()
	{
		if (coinDistance > coinSpace ) {
			return true;
		} else {
			return false;
		}
	}

	public bool shouldCreateGem()
	{
		if ( collectableDistance > collectableSpace ) {
			return true;
		} else {
			return false;
		}
	}

	public void createCollectableAt(Vector3 position)
	{
		for (int i = 0; i < poolSize; i++) 
		{
			GameObject c = gems [i];

			if (c.activeSelf == false) 
			{
				c.SetActive(true);
				c.transform.position = position;
				break;
			}

		}
	}

	public void createCoinAt(Vector3 position)
	{
		for (int i = 0; i < poolSize; i++) 
		{
			GameObject c = coins [i];

			if (c.activeSelf == false) 
			{
				c.SetActive(true);
				c.transform.position = position;
				break;
			}
		}
	}

	public void createHasteAt(Vector3 position)
	{
		for (int i = 0; i < poolSize; i++) 
		{
			GameObject c = hastes [i];

			if (c.activeSelf == false) 
			{
				c.SetActive(true);
				c.transform.position = position;
				break;
			}
		}
	}
}
