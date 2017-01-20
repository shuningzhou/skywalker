using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {
	public GameObject collectable;
	public GameManager gameManager;

	private int distance;
	public int space = 100;
	public float floatDistance = 1.5f;
	public int poolSize = 30;

	private List<GameObject> collectables = new List<GameObject> ();

	void Awake()
	{
		distance = 0;
		Vector3 startPosition = new Vector3 (0, -100, 0);
		for (int i = 0; i < poolSize; i++)
		{
			GameObject c = Instantiate (collectable, startPosition, Quaternion.identity);
			Collectable ca = c.GetComponent<Collectable> ();
			ca.gameManager = gameManager;
			c.SetActive(false);
			collectables.Add(c);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void moved(float step, Vector3 position)
	{
		distance = distance + (int)(step * 10);
		if (shouldCreateCollectable ()) 
		{
			Vector3 collectablePosition = new Vector3 (position.x, position.y + floatDistance, position.z);
			createCollectableAt (collectablePosition);
			distance = 0;
			space = Random.Range (10, 30);
		}
	}

	public bool shouldCreateCollectable()
	{
		int reminder = distance % space;
		if (reminder == 0 ) {
			return true;
		} else {
			return false;
		}
	}

	public void createCollectableAt(Vector3 position)
	{
		for (int i = 0; i < poolSize; i++) 
		{
			GameObject c = collectables [i];

			if (c.activeSelf == false) 
			{
				c.SetActive(true);
				c.transform.position = position;
				break;
			}

		}
	}
}
