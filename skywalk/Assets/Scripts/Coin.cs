using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable {
	
	public override void onCollision(Vector3 position)
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.collected);
		GameManager.sharedManager.collectedCoin ();
	}

	public override void doParticle(Vector3 position)
	{
		Instantiate(explosionPrefab, position, Quaternion.identity);
	}
}
