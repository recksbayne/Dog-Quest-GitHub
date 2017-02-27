using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour {

	//dog gameobject variable
	private GameObject proDog;

	//Native Atributes
	public float speed;
	public Vector3 Direction;
	public CharacterController cc;
	public bool DogBark; 

	void Awake(){
		this.proDog = GameObject.Find ("Dog");
		this.cc = this.gameObject.GetComponent<CharacterController> ();
	}
	// Use this for initialization
	void Start () {
		this.DogBark = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Find Dog function
		PursuitDog();
		if (this.DogBark) {
			ScapeDog ();
		}
		this.cc.Move(Direction * speed * Time.deltaTime);
		this.transform.LookAt (this.proDog.transform.position);


	}
	void DogBarking(){
		this.DogBark = true;
	}
	void PursuitDog(){
		this.Direction = this.proDog.transform.position - this.transform.position;
		this.Direction = this.Direction.normalized;
	}
	void ScapeDog(){
		this.Direction *= -1;
	}
}
