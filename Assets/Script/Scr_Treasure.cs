using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Treasure : MonoBehaviour {
	public bool vLocked;
	public bool vOpened;
	public bool vDone;
	public string vItemName = "Empty";
	public GameObject vItem;
	public bool vAnimate;
	public Animator Ani;
	public float vFrame;
	public AudioSource ChestSound;

	public int vChestNumber;
	// Use this for initialization
	void Start () {
		Ani = gameObject.GetComponent<Animator> ();
		Ani.speed = 0f;
		GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
		bool tBool = tGO.GetComponent<Scr_Player> ().aChestOpened [vChestNumber];
		if (!tBool) {
			Ani.speed = 50f;
			vOpened = true;
		}
	}
	void GetHit(){
		if (!vOpened) {
			ChestSound.Play ();
			vAnimate = true;
			vOpened = true;
			Ani.speed = 1f;
		}
		Debug.Log ("I got a hit");
	}
	// Update is called once per frame
	void Update () {
		if (vAnimate) {
			vFrame += 0.025f;
			if (!vDone && vFrame > .7f){
				GameObject tObj;
				tObj = Instantiate (vItem);
				tObj.transform.position = new Vector3 (transform.position.x,transform.position.y+1f,transform.position.z);
				vOpened = true;
				GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
				tGO.GetComponent<Scr_Player> ().aChestOpened [vChestNumber] = false;
				vDone = true;
			}
			if (vFrame >= 1f){
				vFrame = 1f;
				vAnimate = false;
			}
		}
		//Ani["Open"].normalizedTime = vFrame;
		//this.transform.localScale = new Vector3(1f+tTmp*1.25f,1f-tTmp*1.25f,1f+tTmp*1.25f);

	}
}
