using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Door : MonoBehaviour {
	public bool vHasDoor = true;
	public bool vLocked;
	public int vNextRoom;
	private GameObject vCanvas;
	public string vKeyCode;
	public bool vWiggle;
	public AnimationCurve vStretchA;
	public float vFrame;
	// Use this for initialization
	void Start () {
		vCanvas = GameObject.FindGameObjectWithTag ("Canvas");
		if (vHasDoor) {
			GameObject Temp = null;
			Temp = transform.FindChild ("Obj_Door").gameObject;
			if (Temp != null) {
				if (!vLocked)
					Destroy (Temp);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		float tTmp = 0f;
		if (vWiggle) {
			if (vFrame == 0f)
			vCanvas.SendMessage("GotoNextRoom",vNextRoom);
			vFrame += 0.025f;
			tTmp = vStretchA.Evaluate (vFrame) / 2f;
			if (vFrame >= .7f)
			{
				if (vLocked) {
				} else {
					// GotoNextRoom

				}
			}
			if (vFrame >= 1f){
				vFrame = 1f;
				vWiggle = false;
			}

		}
		//this.transform.localScale = new Vector3(1f+tTmp*1.25f,1f-tTmp*1.25f,1f+tTmp*1.25f);
	}

	void GetHit() {
		if (!vWiggle) {
			//vWiggle = true;
		}

	}

	void OnTriggerEnter(Collider Other){
		if (Other.tag == "Player")
			if (!vWiggle) {
				vWiggle = true;
				vFrame = 0f;
			}

	}
}
