using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_UI : MonoBehaviour {

	public Scr_Health PlayerHealth;
	public Scr_Player Doggie;

	// ui objects
	public Image Background;
	public Image StartScreen;

	public Image BossKey;
	public Image RegularKey;

	//lifepoints
	public Image l1;
	public Image l2;
	public Image l3;
	public Image l4;
	public Image l5;
	public Image l6;
	public Image l7;
	public Image l8;
	public Image l9;
	public Image l10;


	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Return)){
			Background.enabled = false;
			StartScreen.enabled = false;
			Time.timeScale = 1;
		}
		if (PlayerHealth.vHealth < 10) {
			l10.enabled = false;
		}
		if (PlayerHealth.vHealth < 9) {
			l9.enabled = false;
		}
		if (PlayerHealth.vHealth < 8) {
			l8.enabled = false;
		}
		if (PlayerHealth.vHealth < 7) {
			l7.enabled = false;
		}
		if (PlayerHealth.vHealth < 6) {
			l6.enabled = false;
		}
		if (PlayerHealth.vHealth < 5) {
			l5.enabled = false;
		}
		if (PlayerHealth.vHealth < 4) {
			l4.enabled = false;
		}
		if (PlayerHealth.vHealth < 3) {
			l3.enabled = false;
		}
		if (PlayerHealth.vHealth < 2) {
			l2.enabled = false;
		}
		if (PlayerHealth.vHealth < 1) {
			l1.enabled = false;
		}
	}
}
