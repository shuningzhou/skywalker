using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {
	public GameObject gem;
	public GameObject coin;

	public GameObject growth;
	public GameObject haste;
	public GameObject magnet;
	public GameObject levitation;

	public GameManager gameManager;
	public GameObject chest;

	public CharacterMovement player;

	private float collectableDistance = 0;

	public float collectableSpace = 3;
	public float floatDistance = 1.5f;
	public int poolSize = 100;

	public int collectableRowCount = 5;
	public float collectableX = 0;
	public float collectableZ = 0;

	public Skill magnetSkill;
	public Skill levitationSkill;
	public Skill growthSkill;
	public Skill hasteSkill;

	public float scatternessSeed = 5f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	float gemScatterness()
	{
		return 3f * LevelManager.sharedManager.currentLevel.gemScatterness;
	}

	float coinScatterness()
	{
		return 3f * LevelManager.sharedManager.currentLevel.coinScatterness;
	}

	float dropletScatterness()
	{
		return 3f * LevelManager.sharedManager.currentLevel.dropletScatterness;
	}
		
	public void moved(float step, Vector3 position, float width)
	{
		collectableDistance = collectableDistance + (step);

		float w = width / 2;

		if (shouldCreateGem()) 
		{
			Vector3 collectablePosition = new Vector3 (position.x + collectableX, position.y + floatDistance, position.z + collectableZ);
			createCollectableAt (collectablePosition);
			collectableDistance = 0;
			collectableRowCount = collectableRowCount - 1;
			collectableSpace = 1;

			GameManager.sharedManager.totalGemThisRound = GameManager.sharedManager.totalGemThisRound + 1;

			if (collectableRowCount <= 0) 
			{
				collectableX = gemScatterness() * Random.Range ((-scatternessSeed)-w, gemScatterness()+w);
				collectableZ = gemScatterness() * Random.Range ((-scatternessSeed)-w, gemScatterness()+w);
				collectableRowCount = Random.Range (1, 10);
				collectableSpace = 3;
			}
		} 

		if (shouldCreateCoin ()) 
		{
			float randX = coinScatterness() * Random.Range ((-scatternessSeed)-w, coinScatterness()+w);
			float randZ = coinScatterness() * Random.Range ((-scatternessSeed)-w, coinScatterness()+w);

			Vector3 collectablePosition = new Vector3 (position.x + randX, position.y + floatDistance, position.z + randZ);
			createCoinAt (collectablePosition);
		} 

		if (shouldCreateHaste ())
		{
			float randX = dropletScatterness() * Random.Range ((-scatternessSeed)-w, dropletScatterness()+w);
			float randZ = dropletScatterness() * Random.Range ((-scatternessSeed)-w, dropletScatterness()+w);

			Vector3 collectablePosition = new Vector3 (position.x + randX, position.y + floatDistance, position.z + randZ);
			createHasteAt (collectablePosition);
		}

		if (shouldCreateGrowth ())
		{
			float randX = dropletScatterness() * Random.Range ((-scatternessSeed)-w, dropletScatterness()+w);
			float randZ = dropletScatterness() * Random.Range ((-scatternessSeed)-w, dropletScatterness()+w);

			Vector3 collectablePosition = new Vector3 (position.x + randX, position.y + floatDistance, position.z + randZ);
			createGrowthAt (collectablePosition);
		}

		if (shouldCreateMagnet ())
		{
			float randX = dropletScatterness() * Random.Range ((-scatternessSeed)-w, dropletScatterness()+w);
			float randZ = dropletScatterness() * Random.Range ((-scatternessSeed)-w, dropletScatterness()+w);

			Vector3 collectablePosition = new Vector3 (position.x + randX, position.y + floatDistance, position.z + randZ);
			createMagnetAt (collectablePosition);
		}

		if (shouldCreateLevitation ())
		{
			float randX = dropletScatterness() * Random.Range ((-scatternessSeed)-w, dropletScatterness()+w);
			float randZ = dropletScatterness() * Random.Range ((-scatternessSeed)-w, dropletScatterness()+w);

			Vector3 collectablePosition = new Vector3 (position.x + randX, position.y + floatDistance, position.z + randZ);
			createLevitationAt (collectablePosition);
		}
	}

	public bool shouldCreateHaste ()
	{
		if (hasteSkill.info.isLocked == 1)
		{
			return false;
		}

		float r = Random.Range (0f, 1f);

		if (r < LevelManager.sharedManager.currentLevel.hasteDropRate)
		{
			return true;
		} else {
			return false;
		}
	}

	public bool shouldCreateGrowth ()
	{
		if (growthSkill.info.isLocked == 1)
		{
			return false;
		}

		float r = Random.Range (0f, 1f);

		if (r < LevelManager.sharedManager.currentLevel.growthDropRate)
		{
			return true;
		} else {
			return false;
		}
	}

	public bool shouldCreateMagnet ()
	{
		if (magnetSkill.info.isLocked == 1)
		{
			return false;
		}

		float r = Random.Range (0f, 1f);

		if (r < LevelManager.sharedManager.currentLevel.magnetDropRate)
		{
			return true;
		} else {
			return false;
		}
	}

	public bool shouldCreateLevitation ()
	{
		if (levitationSkill.info.isLocked == 1)
		{
			return false;
		}

		float r = Random.Range (0f, 1f);

		if (r < LevelManager.sharedManager.currentLevel.levitationDropRate)
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
		float r = Random.Range (0f, 1f);

		if (r < LevelManager.sharedManager.currentLevel.coinDropRate)
		{
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
		GameObject c = Instantiate (gem, position, Quaternion.identity);
		Collectable collectable = c.GetComponent<Collectable> ();
		collectable.player = player;
	}

	public void createCoinAt(Vector3 position)
	{
		GameObject c = Instantiate (coin, position, coin.transform.rotation);
		Collectable collectable = c.GetComponent<Collectable> ();
		collectable.player = player;
	}

	public void createHasteAt(Vector3 position)
	{
		GameObject c = Instantiate (haste, position, coin.transform.rotation);
		Collectable collectable = c.GetComponent<Collectable> ();
		collectable.player = player;
	}

	public void createGrowthAt(Vector3 position)
	{
		GameObject c = Instantiate (growth, position, coin.transform.rotation);
		Collectable collectable = c.GetComponent<Collectable> ();
		collectable.player = player;
	}

	public void createMagnetAt(Vector3 position)
	{
		GameObject c = Instantiate (magnet, position, coin.transform.rotation);
		Collectable collectable = c.GetComponent<Collectable> ();
		collectable.player = player;
	}

	public void createLevitationAt(Vector3 position)
	{
		GameObject c = Instantiate (levitation, position, coin.transform.rotation);
		Collectable collectable = c.GetComponent<Collectable> ();
		collectable.player = player;
	}
}
