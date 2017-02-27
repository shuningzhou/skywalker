using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance = null;
	private AudioSource soundEffectAudio;

	public AudioClip moved;
	public AudioClip collected;
	public AudioClip dropped;
	public AudioClip purchased;
	public AudioClip buttonClicked;
	public AudioClip gameStarted;
	public AudioClip count;
	public AudioClip rating;
	public AudioClip unlock;
	public AudioClip uiFailed;
	public AudioClip rewards;
	public AudioClip coinCollected;
	public AudioClip levelFinished;
	public AudioClip gemCollected;
	public AudioClip haste;

	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}

		AudioSource[] sources = GetComponents<AudioSource>();
		foreach (AudioSource source in sources) {
			if (source.clip == null) {
				soundEffectAudio = source;
			}
		}
	}

	public void PlayOneShot(AudioClip clip) {
		soundEffectAudio.PlayOneShot(clip);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
