using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;    
using com.shephertz.app42.paas.sdk.csharp.user;    
using com.shephertz.app42.paas.sdk.csharp.game;   
using AssemblyCSharp;

public class App42Helper : MonoBehaviour {
	
	public static App42Helper Instance = null;
	private static string apiKey = "c8ab91bb0058d3269f30aa04809aa090460a9a0db5155f914dcb9a7dde7f3bb2";
	private static string secretKey = "6251138fc6fd3fbb70f98c2c425f2af665d0cdecea77dcc6510bdfe22cedd5c3";

	private ServiceAPI api = null;
	public Lexic.NameGenerator namegen;

	public string userName;
	private string email;
	private string password;

	public string userRanking = "";

	public List<RankData> rankDatas = new List<RankData> ();

	void Awake()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}

		this.transform.parent = null;

		DontDestroyOnLoad(gameObject);

		api = new ServiceAPI (apiKey, secretKey);

		if (UserData.getUserName ().Length == 0) {
			userName = createUserName ();
		} else {
			userName = UserData.getUserName ();
		}

		email = createUserEmail ();
		password = createUserPassword ();

		Debug.Log ("App42 Awake");
	}

	string gameName()
	{
		return "Pivot";
	}

	string gameDescription()
	{
		return "pivot mobile game";
	}

	string createUserName()
	{
		string timestamp = System.DateTime.Now.ToString ("ddHHmmss");
		string uid = namegen.GetNextRandomName () + timestamp;
		UserData.saveUserName (uid);
		return uid;
	}

	string createUserPassword()
	{
		return userName;
	}

	string createUserEmail()
	{
		return userName + "@sourceoven.com";
	}

	public void createNewUser()
	{
		updatePasswordAndEmail ();
		createGuestUser ();
	}

	public void updatePasswordAndEmail()
	{
		email = createUserEmail ();
		password = createUserPassword ();
	}

	public void createGuestUser()
	{
		UserService service = api.BuildUserService ();

		service.CreateUser (userName, password, email, new App42UserResponse ());
	}

	public void uploadScoreForUser(float score)
	{
		ScoreBoardService service = api.BuildScoreBoardService ();
		service.SaveUserScore (gameName(), userName, score, new App42ScoreBoardResponse());
	}

//	public void getRanksNearUser()
//	{
//		float best = UserData.getLocalBestRecordValue ();
//		ScoreBoardService service = api.BuildScoreBoardService ();
//		service.GetUsersWithScoreRange ();
//	}

	public void getTop10Rankers()
	{
		ScoreBoardService service = api.BuildScoreBoardService ();
		service.GetTopNRankers (gameName (), 20, new App42TopRankingResponse ());
	}

	public void getUserRanking()
	{
		ScoreBoardService service = api.BuildScoreBoardService ();
		service.GetUserRanking (gameName (), userName, new App42UserRankingResponse ());
	}

	public void createGame()
	{
		GameService service = api.BuildGameService ();
		service.CreateGame (gameName(), gameDescription(), new App42CreateGameResponse ());
	}

	public List<RankData> getRankDatas ()
	{
		return rankDatas;
	}
}