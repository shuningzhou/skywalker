using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {
	public GameObject collectable;

	private int distance;
	public int space = 100;
	public float floatDistance = 1.5f;
	public int poolSize = 30;

	private List<GameObject> collectables = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		distance = 0;
		Vector3 startPosition = new Vector3 (0, -100, 0);
		for (int i = 0; i < 100; i++)
		{
			GameObject c = Instantiate (collectable, startPosition, Quaternion.identity);
			c.SetActive(false);
			collectables.Add(c);
		}
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
			space = Random.Range (40, 200);
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
		for (int i = 0; i < 100; i++) 
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
