using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Door : MonoBehaviour {
	public bool vHasDoor = true;
	public bool vLocked;
	public int vNextRoom;
	private GameObject vCanvas;
	public bool vGetKey;
	public int vKeyCode;
	public bool vAnimate;
	public Animator Ator;
	public float vFrame;

	public bool vIsBossDoor;
	public GameObject vBossDoor;


	// Use this for initialization
	void Start () {
		if (vIsBossDoor) {
			Ator = vBossDoor.GetComponent<Animator> ();
			Ator.speed = 0f;
		}
		if (vGetKey) {
			GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
			bool tBool = tGO.GetComponent<Scr_Player> ().aDoorsOpened [vKeyCode];
			vLocked = tBool;
			if (!vLocked && vIsBossDoor)
				Ator.speed = 50f;
		}
		vCanvas = GameObject.FindGameObjectWithTag ("Canvas");
		if (vHasDoor) {
			GameObject Temp = null;
			Temp = transform.FindChild ("Obj_Door").gameObject;
			if (Temp != null) {
				if (!vLocked)
					Temp.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (vAnimate) {
			if (vFrame == 0f)
			vCanvas.SendMessage("GotoNextRoom",vNextRoom);
			vFrame += 0.025f;

			if (vFrame >= 1f){
				vFrame = 1f;
				vAnimate = false;
			}

		}
		//this.transform.localScale = new Vector3(1f+tTmp*1.25f,1f-tTmp*1.25f,1f+tTmp*1.25f);
	}

	void GetHit() {
		Debug.Log ("Door got hit");
		if (vGetKey && vLocked) {
			GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
			if (vKeyCode == 4) {
				if (tGO.GetComponent<Scr_Player> ().vBossKey == true) {
					tGO.GetComponent<Scr_Player> ().vBossKey = false;
					vLocked = false;
					tGO.GetComponent<Scr_Player> ().aDoorsOpened [vKeyCode] = false;
					Ator.speed = 1f;
				}
			} else {
				if (tGO.GetComponent<Scr_Player> ().vKeyCount > 0) {
					tGO.GetComponent<Scr_Player> ().vKeyCount -= 1;
					vLocked = false;
					tGO.GetComponent<Scr_Player> ().aDoorsOpened [vKeyCode] = false;
					GameObject Temp = null;
					Temp = transform.FindChild ("Obj_Door").gameObject;
					if (Temp != null) {
						if (!vLocked)
							Temp.SetActive (false);
					}
				}
			}
		}

	}

	void OnTriggerEnter(Collider Other){
		if (Other.tag == "Player")
		if (!vCanvas.GetComponent<Scr_Canvas>().vTransition)
			if (!vAnimate) {
				vAnimate = true;
				vFrame = 0f;
			}

	}
}
