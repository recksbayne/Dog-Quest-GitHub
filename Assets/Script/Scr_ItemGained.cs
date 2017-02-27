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

	// Use this for initialization
	void Start () {
		vRotation = Random.Range (0f, 360f);
		vClick = 3;
		vTimer = 1f;
	}
	// Update is called once per frame
	void Update () {
		vRotation -= 2.5f;
		if (vTimer >= 0f) {
			vTimer -= .01f;
		} else { vTimer = .2f;
			if (vOn) {
				gameObject.GetComponent<MeshRenderer> ().enabled = false;
				if (vClick <= 0)
					Destroy (this);
				vClick -= 1;
				vOn = false;
			} else {
				vOn = true;
				gameObject.GetComponent<MeshRenderer> ().enabled = true;
			}
		}
		transform.eulerAngles = new Vector3 (0f, vRotation, 0f);
		transform.position = new Vector3 (transform.position.x, transform.position.y + vHeight, transform.position.z);
	}
}
