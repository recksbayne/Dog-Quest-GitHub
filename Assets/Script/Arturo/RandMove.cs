using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandMove : MonoBehaviour {

	public float speed;
	public float maxPathLegth;
	public float wallAvoidDistance;
	public bool reachpoint;
	public Vector3 target = Vector3.zero;
	public bool ChangeDir = false;
	public bool coliding;
	private CharacterController controller;
	public float timeCounter = 0;
	public float WalkingTime;
	public float IdleTime;
	// Use this for initialization
	void Start () {
		SetPoint ();
		controller = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.position = Random.insideUnitCircle;
		timeCounter += Time.deltaTime;
		if (timeCounter > WalkingTime + IdleTime) {
			SetPoint ();
			timeCounter = 0;
		}
		if (ChangeDir) {
			SetPoint ();
			ChangeDir = false;
		}
		if(timeCounter < WalkingTime){
			controller.Move (target * speed * Time.deltaTime);
		}
	}
	void OnControllerColliderHit(ControllerColliderHit hit){
		ChangeDir = true;
		Debug.Log ("collision");
	}
	void SetPoint(){
		Vector3 tempPoint = Random.insideUnitCircle;
		Vector3 transPoint = Vector3.zero;
		transPoint.x = tempPoint.x;
		transPoint.z = tempPoint.y;
		this.transform.Rotate (0f,Vector3.Angle (target, transPoint),0f);
		target.x = tempPoint.x;
		target.z = tempPoint.y;
		target = target.normalized;

	}
}
