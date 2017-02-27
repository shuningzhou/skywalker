using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable {
	
	public override void onCollision(Vector3 position)
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.coinCollected);
		GameManager.sharedManager.collectedCoin ();
	}
}
