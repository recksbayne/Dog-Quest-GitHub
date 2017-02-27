using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Treasure : MonoBehaviour {
	public bool vLocked;
	public bool vOpened;
	public string vItemName = "Empty";
	public GameObject vItem;
	public bool vWiggle;
	public AnimationCurve vStretchA;
	public AnimationCurve vStretchB;
	public float vFrame;
	// Use this for initialization
	void Start () {
		
	}
	void GetHit(){
		if (!vWiggle) {
			vWiggle = true;
		}
	}

	// Update is called once per frame
	void Update () {
		float tTmp = 0f;
		if (vWiggle) {
			vFrame += 0.02f;
			tTmp = vStretchA.Evaluate (vFrame) / 2f;
			if (vFrame >= .7f && !vOpened)
				{GameObject tObj;
				tObj = Instantiate (vItem);
				tObj.transform.position = new Vector3 (transform.position.x,transform.position.y+1f,transform.position.z);
				vOpened = true;
			}
			if (vFrame >= 1f){
				vFrame = 0f;
				vWiggle = false;
				vOpened = false;
			}
				
		}
		this.transform.localScale = new Vector3(1f+tTmp*1.25f,1f-tTmp*1.25f,1f+tTmp*1.25f);

	}
}
