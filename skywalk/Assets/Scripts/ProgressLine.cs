using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLine : MonoBehaviour {

	public Image image;
	public Sprite blackVertical;
	public Sprite colorVertical;
	public Sprite blackHorizontal;
	public Sprite colorHorizontal;
	public bool isVertical;
	public int levelIndex;

	// Use this for initialization
	void Start () {
		
		Level level = LevelManager.sharedManager.levels [levelIndex];
		int userProgressLevel = Level.getUerProgressLevel ();

		if (userProgressLevel < level.level) 
		{
			if (isVertical) 
			{
				image.sprite = blackVertical;
			} 
			else 
			{
				image.sprite = blackHorizontal;
			}
		} 
		else 
		{
			if (isVertical) 
			{
				image.sprite = colorVertical;
			} 
			else 
			{
				image.sprite = colorHorizontal;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
