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
	public string currentState;
	public Quaternion rotation;
	public float attackRange; // enemy weapon range
	public float DogDistance; // distance between the enemy and the dog
	public float PursuitRange; // min distance to enter in pursuit mode
	public float ScareRadius; //radius area where the cat will run in the oposite direction of the dog
	public float barkRadius; // set the radius of the bark
	public float lifepoints; //if we need them
	public int EnemyCode;// 1 for club-cat 2 for spear cat 3 for possible boss
	public float ScareCooldown;//if the scare has a cooldown
	public float timeScared;//time the cat is beaing in getbark state


	void Awake(){
		timeScared = 0f;
		proDog = GameObject.Find ("Dog");
		cc = gameObject.GetComponent<CharacterController> ();
		barkRadius = testingBark.barkRadius;
		//barkRadius = doggie.barkRadius;
	}
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//Distance Dog function
		GetDogDistance();
		//Dog Direction
		DogDirection();
		//State Maachine
		RunStates ();
		//Pursuit Dog function
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f); // rotation code i found that actually works pretty good
		cc.Move(Direction * speed * Time.deltaTime);

	}
	void OnTriggerEnter(Collider impactObject){
		if (impactObject.name == "DogWeapon" && currentState != "isGettingHit") {
			StartGetHit ();
		}
	}

	void RunStates(){
		if (DogDistance > ScareRadius && EnemyCode == 1 && currentState == "isGettingFear") {
			startGetScare ();
		}
		else if(currentState == "isIdling" && DogDistance <= attackRange && currentState != "isGettingFear"){
			StartAttack ();
		}
		//Walk
		else if (currentState != "isWalking" && currentState != "isGettingHit" && currentState != "isAttacking" && DogDistance > attackRange && currentState != "isGettingFear" && currentState != "isGettingScared") {
			StartWalk ();
		}
		//Idle
		else if (currentState == "none")
		{StartIdle();}

		//Run ongoing states

		if (currentState == "isGettingFear")
			GetFear ();
		else if (currentState == "isGettingScared")
			GetScare ();
		else if (currentState == "isGettingHit")
			GetHit ();
		else if (currentState == "isAttacking")
			Attack ();
		else if (currentState == "isWalking")
			Walk ();
		else if (currentState == "isIdling")
			Idle ();
		//Turn every frame, overlapping state
		//Turn();
	}
	void ResetStates(){
		StopIdle ();
		StopAttack ();
		StopWalk ();
		StopGetHit ();
		StopGetFear ();
		StopGetScare ();
	}

	//Idle States
	void StartIdle(){
		ResetStates ();
		currentState = "isIdling";
	}
	void Idle(){
	}
	void StopIdle(){
		currentState = "none";
	}
	//Atthack States
	void StartAttack(){
		currentState = "isAttacking";
		//activate weapon collider
	}
	void Attack(){
		//animation condition
		StopAttack();
	}
	void StopAttack(){
		currentState = "none";
		//disable weapon collider
	}
	//Walking states
	void StartWalk(){
		ResetStates ();
		currentState = "isWalking";
	}
	void Walk(){
		rotation = Quaternion.LookRotation(Direction);
		if (DogDistance <= attackRange) {
			StopWalk ();
			Direction = Vector3.zero;
		}
	}
	void StopWalk(){
		currentState = "none";
	}
	//Getting damage states
	void StartGetHit(){
		ResetStates ();
		currentState = "isGettingHit";
	}
	void GetHit(){
		//animation condition
		StopGetHit();
	}
	void StopGetHit(){
		currentState = "none";
		//set animation here
	}
	//Getting Bark states
	void StartGetFear(){
		ResetStates ();
		currentState = "isGettingFear";
	}
	void GetFear(){
		ScapeDog ();
		rotation = Quaternion.LookRotation(Direction);
		timeScared += Time.deltaTime;
		if (EnemyCode == 2 && timeScared > ScareCooldown) {
			ScapeDog ();
			timeScared = 0;
			StopGetFear ();
		}
	}
	void StopGetFear(){
		currentState = "none";
	}
	//Club cat Scared Mode
	void startGetScare(){
		Debug.Log ("enter");
		ResetStates ();
		currentState = "isGettingScared";
	}
	void GetScare(){
		ScapeDog ();
		rotation = Quaternion.LookRotation(Direction);
		Direction = Vector3.zero;
	}
	void StopGetScare(){
		currentState = "none";
	}

	void GetDogDistance(){
		DogDistance = Vector3.Distance(proDog.transform.position,transform.position);
	}

	void DogBarking(){
		if(DogDistance < barkRadius)
			StartGetFear ();
	}
	void DogDirection(){
		Direction = proDog.transform.position - transform.position;
		Direction.y = 0;
		Direction = Direction.normalized;
	}
	void ScapeDog(){
		Direction *= -1;
	}
}




