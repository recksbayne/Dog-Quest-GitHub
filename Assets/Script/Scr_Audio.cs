using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Scr_Audio : MonoBehaviour {

	static bool AudioBegin = false; 

	void Awake()
	{
		AudioSource audio = GetComponent<AudioSource>();
		if (!AudioBegin) {
			audio.Play();
			DontDestroyOnLoad (gameObject);
			AudioBegin = true;
		} 
	}
	void Update () {
		
	}
}
