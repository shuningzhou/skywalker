using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour {

	public GameObject player;

	public static SkillManager sharedManager = null;

	public SkillStatus hastStatus;
	public SkillStatus levitationStatus;
	public SkillStatus growthStatus;
	public SkillStatus magnetStatus;

	void Awake()
	{

		if (sharedManager == null) 
		{
			sharedManager = this;
		} else if (sharedManager != this) {
			Destroy(gameObject);
			return;
		}

	}

	void OnDestroy()
	{

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void collected (Droplet droplet)
	{
		switch (droplet.type)
		{
		case Droplet.DropletType.Haste:
			Debug.Log ("haste collected!");
			hastStatus.addDroplet ();
		break;
		case Droplet.DropletType.Growth:
			Debug.Log ("haste collected!");
			growthStatus.addDroplet ();
			break;
		case Droplet.DropletType.Levitation:
			Debug.Log ("haste collected!");
			levitationStatus.addDroplet ();
			break;
		case Droplet.DropletType.Magnet:
			Debug.Log ("haste collected!");
			magnetStatus.addDroplet ();
			break;
		default:

		break;
		}
	}
}
