using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryPlayHelper : MonoBehaviour 
{
	public static EveryPlayHelper Instance = null;
	private string debugText;

	private string searchUrl = "/games/current/videos?client_id=49c37d5f8a6976340652e32be8e314c0278a7fcb&limit=1&meta_data={demo:1}";
	private Dictionary<string, object> featuredVideo;

	void Awake() {
		/* Get events from Everyplay */
		Everyplay.ReadyForRecording += OnReadyForRecording;
		Everyplay.RecordingStarted += RecordingStartedDelegate;
		Everyplay.RecordingStopped += RecordingStoppedDelegate;
		Everyplay.SetLowMemoryDevice (true);

		GameManager.onGamePlay += onGamePlay;
		GameManager.onGameOver += onGameOver;
	}

	void OnDestroy() {
		/* Remove event receivers */
		Everyplay.RecordingStarted -= RecordingStartedDelegate;
		Everyplay.RecordingStopped -= RecordingStoppedDelegate;
		Everyplay.ReadyForRecording -= OnReadyForRecording;

		GameManager.onGamePlay -= onGamePlay;
		GameManager.onGameOver -= onGameOver;
	}

	void Start()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}

		Everyplay.MakeRequest("get", searchUrl, null, delegate(string data) {
			List<System.Object> videos = EveryplayMiniJSON.Json.Deserialize(data) as List<System.Object>;
			append(videos.ToString());
			if (videos.Count == 1) 
			{
				foreach (Dictionary<string, object> video in videos) {
					featuredVideo = video;
				}
			}
		}, delegate(string error) {
			append("feature video not found" + error);
		});
	}

	public void onGamePlay ()
	{
		startRecording ();
	}

	public void onGameOver ()
	{
		stopRecording ();
	}

	public void show()
	{
		append ("show");
		Everyplay.Show ();
	}

	public void startRecording()
	{
		append ("start Recording");
		Everyplay.StartRecording ();
	}

	public void stopRecording()
	{
		append ("stop Recording");
		Everyplay.StopRecording ();
	}

	public void setDemo()
	{
		append ("stop Recording");
		Everyplay.SetMetadata ("demo", 1);
	}

	public void playLastRecording()
	{
		append ("play last recording");
		Everyplay.PlayLastRecording();
	}

	public void playDemo()
	{
		if (featuredVideo != null ) {
			Everyplay.PlayVideoWithDictionary(featuredVideo);
		} else {
			append("Search didn't return any video.");
		}
	}

	public void OnReadyForRecording(bool enabled) {
		if (enabled) {
			append ("Ready to Record");
			// The recording is supported
			//myGameObject.SetUpRecording();
		} else {
			append ("Not Ready to Record");
		}
	}

	public void RecordingStartedDelegate() {
		append ("Recording was started");
		/* The recording is now started, show the red "REC" in the upper hand corner */
		//MyGameEngine.ShowRecordingIndicator();
	}

	public void RecordingStoppedDelegate() {
		append ("Recording ended");
		/* Remove visual indicator from the user */
		//MyGameEngine.RemoveRecordingIndicator();
	}
		
	public void append(string txt)
	{
		debugText = txt + "\n" + debugText;
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		string text = debugText;
		GUI.Label(rect, text, style);
	}


}
