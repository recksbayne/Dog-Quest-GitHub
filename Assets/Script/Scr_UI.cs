using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_UI : MonoBehaviour {

	public Scr_Health PlayerHealth;
	public Scr_Player Doggie;
	public bool Prevcanvas;
	public bool GameStarted;

	// ui objects
	public Image Background;
	public Image StartScreen;

	public Image BossKey;
	public Image RegularKey;
	public Text RegularKeyText;
	public Image Bone;
	public Text BoneText;
	public Image StartB;
	public Image BarkBarCooldown;
	public float Barscale;


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

	public AudioSource StartSound;


	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		Prevcanvas = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		BarkBarCooldown.transform.localScale = new Vector3((3f -Doggie.vBarkCD)/ 3f,1f,1f);
		//Doggie.vBarkTime / Doggie.vBarkLS;
		if (Input.GetMouseButton (0) && !Prevcanvas && !GameStarted) {
			StartGame ();
		}
		if(PlayerHealth.vHealth >= 10) {
			l10.enabled = true;
			l9.enabled = true;
			l8.enabled = true;
			l7.enabled = true;
			l6.enabled = true;
			l5.enabled = true;
			l4.enabled = true;
			l3.enabled = true;
			l2.enabled = true;
			l1.enabled = true;
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
		if (Doggie.vBossKey) {
			BossKey.enabled = true;
		} else {
			BossKey.enabled = false;
		}
		if (Doggie.vBoneCount > 0) {
			Bone.enabled = true;
			BoneText.enabled = true;
			BoneText.text = Doggie.vBoneCount.ToString();

		} else {
			Bone.enabled = false;
			BoneText.enabled = false;
		}
		if (Doggie.vKeyCount > 0) {
			RegularKey.enabled = true;
			RegularKeyText.enabled = true;
			RegularKeyText.text = Doggie.vKeyCount.ToString();

		} else {
			RegularKey.enabled = false;
			RegularKeyText.enabled = false;
		}
	}
	public void StartGame(){
		StartSound.Play ();
		GameStarted = true;
		Background.CrossFadeAlpha (0.0f, 1.0f, false);
		StartScreen.CrossFadeAlpha (0.0f, 1.0f, false);
		StartB.CrossFadeAlpha (0.0f, 1.0f, false);
		Time.timeScale = 1;
	}
}
