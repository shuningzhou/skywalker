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

	public void levelData()
	{
		Level level1 = new Level (1, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level2 = new Level (2, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level3 = new Level (3, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level4 = new Level (4, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level5 = new Level (5, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level6 = new Level (6, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level7 = new Level (7, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level8 = new Level (8, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level9 = new Level (9, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level10 = new Level (10, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);

		Level level11 = new Level (11, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level12 = new Level (12, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level13 = new Level (13, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level14 = new Level (14, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level15 = new Level (15, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level16 = new Level (16, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level17 = new Level (17, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level18 = new Level (18, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level19 = new Level (19, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level20 = new Level (20, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);

		Level level21 = new Level (21, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level22 = new Level (22, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level23 = new Level (23, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level24 = new Level (24, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level25 = new Level (25, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level26 = new Level (26, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level27 = new Level (27, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level28 = new Level (28, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level29 = new Level (29, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level30 = new Level (30, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);

		Level level31 = new Level (31, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level32 = new Level (32, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level33 = new Level (33, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level34 = new Level (34, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level35 = new Level (35, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level36 = new Level (36, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level37 = new Level (37, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level38 = new Level (38, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level39 = new Level (39, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level40 = new Level (40, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);

		Level level41 = new Level (41, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level42 = new Level (42, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level43 = new Level (43, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level44 = new Level (44, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level45 = new Level (45, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level46 = new Level (46, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level47 = new Level (47, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level48 = new Level (48, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level49 = new Level (49, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level50 = new Level (50, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);

		Level level51 = new Level (51, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level52 = new Level (52, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level53 = new Level (53, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level54 = new Level (54, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level55 = new Level (55, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level56 = new Level (56, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level57 = new Level (57, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level58 = new Level (58, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level59 = new Level (59, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level60 = new Level (60, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);

		Level level61 = new Level (61, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);
		Level level62 = new Level (62, 5f, 4f, 0.5f, 6f, 150f, 0, 1, 0, 0, 0, 0, 0);

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
