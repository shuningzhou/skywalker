using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager sharedManager = null;
	public List<Level> levels = new List<Level> ();
	public Level currentLevel;

	void Awake()
	{
		if (sharedManager == null) 
		{
			PlayerPrefs.DeleteAll ();
			UserData.addCoinsCount (30);
			sharedManager = this;

			levelData ();

		} else if (sharedManager != this) {
			Destroy(gameObject);
			return;
		}

		this.transform.parent = null;

		DontDestroyOnLoad(gameObject);


		Debug.Log ("Level manager awake");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Level getCurrentLevel()
	{
		return currentLevel;
	}

	public void setCurrentLevel(Level cl)
	{
		currentLevel = cl;
	}

	public void playNextLevel()
	{
		int index = levels.IndexOf (currentLevel);
		index = index + 1;
		if (index <= levels.Count) {
			currentLevel = levels [index];
			playCurrentLevel ();
		}
	}

	public void playCurrentLevel()
	{
		SceneManager.LoadScene ("main", LoadSceneMode.Single);



	}

	public void currentLevelFinishedAndRetured()
	{
		SceneManager.LoadScene ("Home", LoadSceneMode.Single);
	}

	public void currentLevelFailed()
	{
		SceneManager.LoadScene ("Home", LoadSceneMode.Single);
	}


//	public Level(	int level, 
//		float startWidth, 
//		float endWidth, 
//		float degradeRate, 
//		int length, 
//		float initialRotateSpeed, 
//		float rotateSpeedChange, 
//		float maxRotateSpeed, 
//		float gapFrequency, 
//		float curvature, 
//		float cameraSpeed, 
//		float dropDelay, 
//		bool turnRight,
//		float gemScatterness, 
//		float dropletScatterness, 
//		float coinScatterness)

	public void levelData()
	{
		float dropDelaySeed = 0.9f;

		Level level1 = new Level (1, 6f, 4f, 1f, 6, 150f, 5f, 250f, 0f, 0.1f, 0, 1f, true, 0, 0, 0);
		Level level2 = new Level (2, 6f, 4f, 1f, 7, 150f, 5f, 250f, 0, 0.2f, 0, Mathf.Pow(dropDelaySeed, 1), true, 0, 0, 0);
		Level level3 = new Level (3, 5.5f, 4f, 1f, 8, 150f, 5f, 250f, 0, 0.3f, 0, Mathf.Pow(dropDelaySeed, 2), false, 0, 0, 0);
		Level level4 = new Level (4, 5.5f, 4f, 1f, 8, 150f, 5f, 250f, 0, 0.4f, 0, Mathf.Pow(dropDelaySeed, 3), true, 0, 0, 0);
		Level level5 = new Level (5, 5f, 4f, 1f, 8, 150f, 5f, 250f, 0, 0.4f, 0, Mathf.Pow(dropDelaySeed, 4), false, 0, 0, 0);
		Level level6 = new Level (6, 5f, 4f, 1f, 8, 150f, 5f, 250f, 0, 0.4f, 0, Mathf.Pow(dropDelaySeed, 5), true, 0, 0, 0);
		Level level7 = new Level (7, 5f, 4f, 1f, 8, 150f, 5f, 250f, 0, 0.4f, 0, Mathf.Pow(dropDelaySeed, 6), false, 0, 0, 0);
		Level level8 = new Level (8, 4.5f, 3.5f, 1f, 9, 150f, 5f, 250f, 0, 0.4f, 0, Mathf.Pow(dropDelaySeed, 7), true, 0, 0, 0);
		Level level9 = new Level (9, 4.4f, 3.5f, 1f, 9, 150f, 5f, 250f, 0, 0.4f, 0, Mathf.Pow(dropDelaySeed, 8), false, 0, 0, 0);
		Level level10 = new Level (10, 4.3f, 3.5f, 1f, 9, 150f, 5f, 250f, 0, 0.4f, 0, Mathf.Pow(dropDelaySeed, 9), true, 0, 0, 0);

		Level level11 = new Level (11, 4.2f, 3.5f, 1f, 20, 160f, 5f, 270f, 0.05f, 0.5f, 0.1f, Mathf.Pow(dropDelaySeed, 10), true, 0, 0, 0);
		Level level12 = new Level (12, 4.0f, 3.5f, 2f, 20, 160f, 5f, 270f, 0.06f, 0.5f, 0.1f, Mathf.Pow(dropDelaySeed, 11), false, 0, 0, 0);
		Level level13 = new Level (13, 3.9f, 3.5f, 2f, 20, 160f, 5f, 270f, 0.07f, 0.5f, 0.1f, Mathf.Pow(dropDelaySeed, 12), true, 0, 0, 0);
		Level level14 = new Level (14, 3.8f, 3.5f, 2f, 20, 160f, 5f, 270f, 0.07f, 0.5f, 0.1f, Mathf.Pow(dropDelaySeed, 13), false, 0, 0, 0);
		Level level15 = new Level (15, 3.7f, 3.4f, 2f, 20, 160f, 5f, 270f, 0.09f, 0.5f, 0.1f, Mathf.Pow(dropDelaySeed, 14), true, 0, 0, 0);
		Level level16 = new Level (16, 3.6f, 3.3f, 2f, 20, 160f, 5f, 270f, 0.1f, 0.5f, 0.1f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level17 = new Level (17, 3.5f, 3.0f, 2f, 20, 160f, 5f, 270f, 0.1f, 0.5f, 0, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level18 = new Level (18, 3f, 3f, 2f, 20, 160f, 5f, 270f, 0.1f, 0.6f, 0, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level19 = new Level (19, 3f, 3f, 2f, 20, 160f, 5f, 270f, 0.1f, 0.6f, 0, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level20 = new Level (20, 3f, 3f, 2f, 20, 160f, 5f, 270f, 0.1f, 0.6f, 0, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);

		Level level21 = new Level (21, 3f, 3f, 1f, 20, 170f, 10f, 280f, 0.1f, 0.6f, 0.2f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level22 = new Level (22, 3f, 3f, 1f, 20, 170f, 10f, 280f, 0.1f, 0.7f, 0.2f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level23 = new Level (23, 3f, 3f, 1f, 20, 170f, 10f, 280f, 0.1f, 0.7f, 0.2f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level24 = new Level (24, 3f, 3f, 1f, 23, 170f, 10f, 280f, 0.1f, 0.7f, 0.2f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level25 = new Level (25, 3f, 3f, 1f, 23, 170f, 10f, 280f, 0.1f, 0.7f, 0, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level26 = new Level (26, 3f, 2.9f, 1f, 23, 170f, 10f, 280f, 0.1f, 0.7f, 0, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level27 = new Level (27, 3f, 2.9f, 1f, 23, 170f, 10f, 280f, 0.1f, 0.7f, 0.3f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level28 = new Level (28, 3f, 2.9f, 1f, 23, 170f, 10f, 280f, 0.1f, 0.7f, 0.3f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level29 = new Level (29, 3f, 2.9f, 1f, 23, 170f, 10f, 280f, 0.1f, 0.7f, 0.3f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level30 = new Level (30, 3f, 2.9f, 1f, 23, 170f, 10f, 280f, 0.1f, 0.7f, 0.3f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);

		Level level31 = new Level (31, 3f, 2.9f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.8f, 0.4f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level32 = new Level (32, 3f, 2.9f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.8f, 0.4f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level33 = new Level (33, 3f, 2.9f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.8f, 0.4f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level34 = new Level (34, 3f, 2.9f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.8f, 0.5f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level35 = new Level (35, 3f, 2.8f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.8f, 0.5f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level36 = new Level (36, 3f, 2.8f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.8f, 0.5f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level37 = new Level (37, 3f, 2.7f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.8f, 0.6f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level38 = new Level (38, 3f, 2.6f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.9f, 0.6f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);
		Level level39 = new Level (39, 3f, 2.5f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.9f, 0.6f, Mathf.Pow(dropDelaySeed, 15), false, 0, 0, 0);
		Level level40 = new Level (40, 2.9f, 2.5f, 1f, 25, 180f, 15f, 290f, 0.2f, 0.9f, 0.6f, Mathf.Pow(dropDelaySeed, 15), true, 0, 0, 0);

		Level level41 = new Level (41, 2.8f, 2.5f, 1f, 28, 200f, 15f, 290f, 0.2f, 0.9f, 0.7f, Mathf.Pow(dropDelaySeed, 16), true, 0, 0, 0);
		Level level42 = new Level (42, 2.7f, 2.5f, 1f, 28, 200f, 15f, 290f, 0.2f, 0.9f, 0.7f, Mathf.Pow(dropDelaySeed, 16), false, 0, 0, 0);
		Level level43 = new Level (43, 2.6f, 2.5f, 1f, 28, 200f, 15f, 290f, 0.2f, 1f, 0.7f, Mathf.Pow(dropDelaySeed, 16), true, 0, 0, 0);
		Level level44 = new Level (44, 2.5f, 2.5f, 1f, 28, 200f, 15f, 290f, 0.3f, 1f, 0.7f, Mathf.Pow(dropDelaySeed, 16), true, 0, 0, 0);
		Level level45 = new Level (45, 2.5f, 2.5f, 1f, 28, 200f, 15f, 300f, 0.3f, 1f, 0.7f, Mathf.Pow(dropDelaySeed, 16), false, 0, 0, 0);
		Level level46 = new Level (46, 2.5f, 2.5f, 1f, 28, 200f, 15f, 300f, 0.3f, 1f, 0.7f, Mathf.Pow(dropDelaySeed, 16), false, 0, 0, 0);
		Level level47 = new Level (47, 2.5f, 2.5f, 1f, 28, 200f, 15f, 300f, 0.3f, 1f, 0.7f, Mathf.Pow(dropDelaySeed, 16), false, 0, 0, 0);
		Level level48 = new Level (48, 2.5f, 2.5f, 1f, 28, 200f, 15f, 300f, 0.3f, 1f, 0.7f, Mathf.Pow(dropDelaySeed, 16), true, 0, 0, 0);
		Level level49 = new Level (49, 2.5f, 2.5f, 1f, 28, 200f, 15f, 300f, 0.3f, 1f, 0.7f, Mathf.Pow(dropDelaySeed, 16), true, 0, 0, 0);
		Level level50 = new Level (50, 2.5f, 2.5f, 1f, 28, 200f, 15f, 300f, 0.3f, 1f, 0.7f, Mathf.Pow(dropDelaySeed, 16), false, 0, 0, 0);

		Level level51 = new Level (51, 2.5f, 2.5f, 2f, 30, 200f, 15f, 300f, 0.4f, 1.1f, 0.8f, Mathf.Pow(dropDelaySeed, 17), true, 0, 0, 0);
		Level level52 = new Level (52, 2.5f, 2.5f, 2f, 30, 200f, 15f, 300f, 0.4f, 1.1f, 0.8f, Mathf.Pow(dropDelaySeed, 17), true, 0, 0, 0);
		Level level53 = new Level (53, 2.5f, 2.5f, 2f, 30, 200f, 15f, 300f, 0.4f, 1.1f, 0.9f, Mathf.Pow(dropDelaySeed, 17), false, 0, 0, 0);
		Level level54 = new Level (54, 2.5f, 2.5f, 2f, 30, 200f, 15f, 300f, 0.4f, 1.1f, 0.9f, Mathf.Pow(dropDelaySeed, 17), true, 0, 0, 0);
		Level level55 = new Level (55, 2.5f, 2.5f, 3f, 30, 200f, 15f, 300f, 0.4f, 1.1f, 1f, Mathf.Pow(dropDelaySeed, 18), false, 0, 0, 0);
		Level level56 = new Level (56, 2.5f, 2.5f, 3f, 30, 200f, 15f, 310f, 0.4f, 1.1f, 1f, Mathf.Pow(dropDelaySeed, 18), true, 0, 0, 0);
		Level level57 = new Level (57, 2.5f, 2.5f, 3f, 30, 200f, 15f, 310f, 0.5f, 1.1f, 1.1f, Mathf.Pow(dropDelaySeed, 18), false, 0, 0, 0);
		Level level58 = new Level (58, 2.5f, 2.5f, 3f, 30, 200f, 20f, 320f, 0.5f, 1.2f, 1.2f, Mathf.Pow(dropDelaySeed, 18), true, 0, 0, 0);
		Level level59 = new Level (59, 2.5f, 2.4f, 4f, 30, 200f, 20f, 320f, 0.5f, 1.2f, 1.3f, Mathf.Pow(dropDelaySeed, 19), true, 0, 0, 0);
		Level level60 = new Level (60, 2.5f, 2.3f, 4f, 30, 200f, 20f, 320f, 0.6f, 1.2f, 1.3f, Mathf.Pow(dropDelaySeed, 19), false, 0, 0, 0);

		Level level61 = new Level (61, 2.5f, 2.2f, 4f, 32, 230f, 25f, 330f, 0.6f, 1.2f, 1.3f, Mathf.Pow(dropDelaySeed, 19), true, 0, 0, 0);
		Level level62 = new Level (62, 2.5f, 2.0f, 4f, 36, 230f, 25f, 340f, 0.7f, 1.2f, 1.3f, Mathf.Pow(dropDelaySeed, 20), false, 0, 0, 0);

		this.levels.Add (level1);
		this.levels.Add (level2);
		this.levels.Add (level3);
		this.levels.Add (level4);
		this.levels.Add (level5);
		this.levels.Add (level6);
		this.levels.Add (level7);
		this.levels.Add (level8);
		this.levels.Add (level9);
		this.levels.Add (level10);

		this.levels.Add (level11);
		this.levels.Add (level12);
		this.levels.Add (level13);
		this.levels.Add (level14);
		this.levels.Add (level15);
		this.levels.Add (level16);
		this.levels.Add (level17);
		this.levels.Add (level18);
		this.levels.Add (level19);
		this.levels.Add (level20);

		this.levels.Add (level21);
		this.levels.Add (level22);
		this.levels.Add (level23);
		this.levels.Add (level24);
		this.levels.Add (level25);
		this.levels.Add (level26);
		this.levels.Add (level27);
		this.levels.Add (level28);
		this.levels.Add (level29);
		this.levels.Add (level30);

		this.levels.Add (level31);
		this.levels.Add (level32);
		this.levels.Add (level33);
		this.levels.Add (level34);
		this.levels.Add (level35);
		this.levels.Add (level36);
		this.levels.Add (level37);
		this.levels.Add (level38);
		this.levels.Add (level39);
		this.levels.Add (level40);

		this.levels.Add (level41);
		this.levels.Add (level42);
		this.levels.Add (level43);
		this.levels.Add (level44);
		this.levels.Add (level45);
		this.levels.Add (level46);
		this.levels.Add (level47);
		this.levels.Add (level48);
		this.levels.Add (level49);
		this.levels.Add (level50);

		this.levels.Add (level51);
		this.levels.Add (level52);
		this.levels.Add (level53);
		this.levels.Add (level54);
		this.levels.Add (level55);
		this.levels.Add (level56);
		this.levels.Add (level57);
		this.levels.Add (level58);
		this.levels.Add (level59);
		this.levels.Add (level60);

		this.levels.Add (level61);
		this.levels.Add (level62);
	}
}
