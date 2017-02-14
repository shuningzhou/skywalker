using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : Collectable {

	public enum DropletType {
		Haste = 0,
		Levitation = 1,
		Growth = 2,
		Magnet = 3,
		Conjure = 4,
	};

	public DropletType type;


	public override void onCollision(Vector3 position)
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.collected);
		SkillManager.sharedManager.collected (this);
	}
}
