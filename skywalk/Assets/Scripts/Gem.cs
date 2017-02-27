using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable {

	public override void onCollision(Vector3 position)
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.gemCollected);
		GameManager.sharedManager.collectedGem ();
	}
}
