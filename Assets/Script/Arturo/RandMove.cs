using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandMove : MonoBehaviour {

	public float speed;
	public float maxPathLegth;
	public float wallAvoidDistance;
	public bool reachpoint;
	public Vector3 target = Vector3.zero;
	// Use this for initialization
	void Start () {
		SetPoint ();
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.position = Random.insideUnitCircle;

		this.transform.position += target * speed * Time.deltaTime;
	}
	void SetPoint(){
		Vector3 tempPoint = Random.insideUnitCircle * 5;
		target.x = tempPoint.x;
		target.z = tempPoint.y;
		Invoke("SetPoint",10f);
	}
}
