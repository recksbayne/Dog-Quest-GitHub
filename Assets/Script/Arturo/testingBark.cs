using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingBark : MonoBehaviour {

	public bool Barking;
	public GameObject[] Enemies;
	public static float barkRadius = 5f;
	// Use this for initialization
	void Start () {
		Enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space))
			{
			for (int i = 0; i < Enemies.Length; i++) {
				Enemies[i].gameObject.SendMessage ("DogBarking");
				}
			}
	}

}
