using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinished : Collectable 
{
	public override void onCollision(Vector3 position)
	{
		GameManager.sharedManager.playWon ();
	}
}
