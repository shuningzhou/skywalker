using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour {
	
	public SkillHaste skillHaste;

	public GameObject player;

	public static SkillManager sharedManager = null;

	private int hasteDropletCount = 0;
//	private int growthDropletCount = 0;
//	private int levitationDropletCount = 0;
//	private int conjureDropletCount = 0;
//	private int MagnetDropletCount = 0;

	public Image hastImage;

	void Awake()
	{

		if (sharedManager == null) 
		{
			sharedManager = this;
		} else if (sharedManager != this) {
			Destroy(gameObject);
			return;
		}

		Skill.OnTimeIsUp += Skill_OnTimeIsUp;
		Skill.ThreeSecBeforeTimeIsUp += Skill_ThreeSecBeforeTimeIsUp;
	}

	void Skill_ThreeSecBeforeTimeIsUp (Skill skill)
	{
		
	}

	void Skill_OnTimeIsUp (Skill skill)
	{
		if (skill.thisSkillInfo.playerOrGame) {
			skill.Deactivate (player);
		} else {
			
		}
	}

	void OnDestroy()
	{
		Skill.OnTimeIsUp -= Skill_OnTimeIsUp;
		Skill.ThreeSecBeforeTimeIsUp -= Skill_ThreeSecBeforeTimeIsUp;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void useHaste()
	{
		if (hasteDropletCount >= skillHaste.thisSkillInfo.requiredDroplets) 
		{
			skillHaste.Activate (player);
			hasteDropletCount = 0;
			Update ();
		}
	}

	public void collected (Droplet droplet)
	{
		switch (droplet.type)
		{
		case Droplet.DropletType.Haste:
			
			if (hasteDropletCount < skillHaste.thisSkillInfo.requiredDroplets) 
			{
				hasteDropletCount = hasteDropletCount + 1;
				updateGUI ();
			}

			break;
		default:

			break;
		}


	}
		
	public void updateGUI()
	{
		int dropletsRequiredForHaste = skillHaste.thisSkillInfo.requiredDroplets;
		float hastFillRate = (float) hasteDropletCount / (float)dropletsRequiredForHaste;
		hastImage.fillAmount = hastFillRate;
	}
}
