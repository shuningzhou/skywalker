using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardPanel : MonoBehaviour {

	public Row row;
	public GameObject scrollContent;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void refreshGUI()
	{
		foreach (RankData rd in App42Helper.Instance.rankDatas) {
			Row r = Instantiate (row);
			r.gameObject.SetActive (true);
			r.userName = rd.userName;
			r.rank = rd.userRank;
			r.score = rd.userScore;
			r.gameObject.transform.SetParent (scrollContent.transform);
		}
	}
}
