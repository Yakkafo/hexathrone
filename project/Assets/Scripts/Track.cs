﻿using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {

	private int trackScore = 0;
	public int scoreToPlay = 1;
	public int scoreToStop = 1;

	public int id;
	public Ball target;
	private int lastCrossedBar = 0;

	private AudioManager audioTarget;

	private bool full = false; // True when playing at 1
	public float minimalVolume = 0.001f;

	// Use this for initialization
	void Start () {
		audioTarget = gameObject.GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		// The ball is not on the track and has crossed a bar
		if(target.crossedBars != lastCrossedBar && target.GetCurrentTrack() != id && trackScore > 0) {
			trackScore --;
		}
		
		if(trackScore >= scoreToPlay) {
			full = true;
		}

		else if(trackScore < scoreToStop) {
			full = false;
		} 


		float minimum = 0f;
		float div = 1f;
		if(target.GetCurrentTrack() == id)
			minimum = minimalVolume;
		if(full)
			div = scoreToStop;
		else
			div = scoreToPlay;


		audioTarget.SetVolumeAll(Mathf.Clamp(((float) trackScore)/ div, minimum, 1f));


		lastCrossedBar = target.crossedBars;
	}

	public bool IsPlayingSomething() {
		return audioTarget.IsPlayingSomething();
	}

	public void IncrementeTrackScore() {
		trackScore += 5;
		
	}
}
