using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour {

	public Row row;
	public Row row1;
	public Row row2;
	public Row row3;
	public Row row4;
	public Row row5;

	public RankData rd;
	public RankData rd1;
	public RankData rd2;
	public RankData rd3;
	public RankData rd4;
	public RankData rd5;

	// Use this for initialization
	void Start () {
		row.rank = rd.rank;
		row.name = rd.name;
		row.score = rd.score;

		row1.rank = rd1.rank;
		row1.name = rd1.name;
		row1.score = rd1.score;

		row2.rank = rd2.rank;
		row2.name = rd2.name;
		row2.score = rd2.score;

		row3.rank = rd3.rank;
		row3.name = rd3.name;
		row3.score = rd3.score;

		row4.rank = rd4.rank;
		row4.name = rd4.name;
		row4.score = rd4.score;

		row5.rank = rd5.rank;
		row5.name = rd5.name;
		row5.score = rd5.score;
	}

	public void refreshRankings()
	{
		row.rank = rd.rank;
		row.name = rd.name;
		row.score = rd.score;

		row1.rank = rd1.rank;
		row1.name = rd1.name;
		row1.score = rd1.score;

		row2.rank = rd2.rank;
		row2.name = rd2.name;
		row2.score = rd2.score;

		row3.rank = rd3.rank;
		row3.name = rd3.name;
		row3.score = rd3.score;

		row4.rank = rd4.rank;
		row4.name = rd4.name;
		row4.score = rd4.score;

		row5.rank = rd5.rank;
		row5.name = rd5.name;
		row5.score = rd5.score;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void sharePressed()
	{
	}

	public void PurchasePressed()
	{
	}

	public void startPressed()
	{
	}

	public void startForFreePressed()
	{
	}
}

public class RankData
{
	public string rank;
	public string name;
	public string score;
}
