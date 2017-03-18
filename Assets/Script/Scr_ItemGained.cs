using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ItemGained : MonoBehaviour {
	private float vRotation;
	public float vHeight =.001f;
	private bool vOn;
	private float vTimer;
	public int vClick;
	public string vItem;
	public GameObject vModel;

	// Use this for initialization
	void Start () {
		vRotation = Random.Range (0f, 360f);
		vClick = 3;
		vTimer = 1f;
		/*GameObject Temp = null;
		Temp = transform.FindChild ("Obj_Model").gameObject;
		if (Temp != null) {
			vModel = Temp;
			Debug.Log (" I got the Model");
		}*/
		GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
		if (vItem == "Key")
			tGO.GetComponent<Scr_Player> ().vKeyCount += 1;
		if (vItem == "BossKey")
			tGO.GetComponent<Scr_Player> ().vBossKey = true;
		if (vItem == "Bone")
			tGO.GetComponent<Scr_Player> ().vBoneCount += 1;
		if (vItem == "Spade"){
			tGO.SendMessage ("ReceiveShovel");
		}
	}
	// Update is called once per frame
	void Update () {
		MeshRenderer[] tMR;
		vRotation -= 2.5f;
		if (vTimer >= 0f) {
			vTimer -= .01f;
		} else { vTimer = .2f;
			if (vOn) {
				tMR = GetComponentsInChildren<MeshRenderer>( );
				foreach (MeshRenderer forMR in tMR)
					forMR.enabled = false;
				
				if (vClick <= 0) {
					vHeight += 100;
					Destroy (this);
				}
				vClick -= 1;
				vOn = false;
			} else {
				vOn = true;
				tMR = GetComponentsInChildren<MeshRenderer>( );
				foreach (MeshRenderer forMR in tMR)
					forMR.enabled = true;

			}
		}
		//transform.eulerAngles = new Vector3 (0f, vRotation, 0f);
		transform.position = new Vector3 (transform.position.x, transform.position.y + vHeight, transform.position.z);
	}
}
