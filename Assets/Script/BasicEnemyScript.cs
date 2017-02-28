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
	public float DogDistance; // distance between the enemy and the dog
	public float PursuitRange; // min distance to enter in pursuit mode
	public float ScareRadius; //radius area where the cat will run in the oposite direction of the dog
	public float lifepoints;
	public float EnemyCode;// 1 for club-cat 2 for spear cat 3 for possible boss
	public float ScareCooldown;//if the scare has a cooldown
	private float timeScared;

	//Bool for dog's bark
	public bool DogBark;

	void Awake(){
		timeScared = 0f;
		proDog = GameObject.Find ("Dog");
		cc = gameObject.GetComponent<CharacterController> ();
	}
	// Use this for initialization
	void Start () {
		DogBark = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Distance Dog function
		GetDogDistance();
	    //Pursuit Dog function
		PursuitDog();
		

		if (DogBark) {
			timeScared += Time.deltaTime;
			ScapeDog ();
			if (DogDistance > ScareRadius && EnemyCode == 1) {
				Direction = Vector3.zero;
			}
			if (EnemyCode == 2 && timeScared > ScareCooldown) {
				ScapeDog ();
				DogBark = false;
				timeScared = 0;
			}
		}
		cc.Move(Direction * speed * Time.deltaTime);
		transform.LookAt (proDog.transform.position);


	}
	void GetDogDistance(){
		DogDistance = Vector3.Distance(proDog.transform.position,transform.position);
	}
	void DogBarking(){
		if(DogDistance < 3.1f)
		DogBark = true;
	}
	void PursuitDog(){
		Direction = proDog.transform.position - transform.position;
		Direction = Direction.normalized;
	}
	void ScapeDog(){
		Direction *= -1;
	}
}
