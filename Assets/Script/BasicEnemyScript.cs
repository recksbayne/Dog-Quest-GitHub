﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour {

	//dog gameobject variable
	private GameObject proDog;

	//Native Atributes
	public float speed;
	public float normalSpeed;
	public float scareSpeed;
	public Vector3 Direction;
	public CharacterController cc;
	public string currentState;
	public Quaternion rotation;
	public float attackRange; // enemy weapon range
	public float DogDistance; // distance between the enemy and the dog
	public float PursuitRange; // min distance to enter in pursuit mode
	public float barkRadius;
	public Scr_Health catHealth; //if we need them
	public int EnemyCode;// 1 for club-cat 2 for spear cat 3 for possible boss
	public float ScareCooldown;//if the scare has a cooldown
	public float timeScared;//time the cat is beaing in getbark state
	public Vector3 OrbLocation;//locate the orb
	public Vector3 OrbDirection; //Orb direction
	public Vector3 OrbLookpos;
	public bool OrbDetected;// Bool that defines the orb detection
	public bool vMoving; //bool that is set when camera charges
	public AudioSource meow;
	public AudioSource Swing;

	// Attack  8===D
	public GameObject vAtkBox;
	public bool vAtkHere; // Attack Box is present T/F
	public float vAtkTime; // Attack Box Countdown to lifespan
	public float vAtkDis = 1.5f; // Attack Distance
	public float vAtkLS; // Attack Box Life Span

	// Model 8===D
	public GameObject vModel;
	public AnimationCurve aBounce; // Bounce effect
	public float aBounceFrame;

	//Animations Variables
	public GameObject catModel;
	private Animator myAnimator;
	public float BlockCooldown;
	private float Blockingtime;

	// Bones
	public GameObject Bone;
	public GameObject Skull;

	//Particles
	public ParticleSystem Sweat;

	void Awake(){
		timeScared = 0f;
		proDog = GameObject.FindGameObjectWithTag ("Player");
		cc = gameObject.GetComponent<CharacterController> ();
		barkRadius = testingBark.barkRadius;
		myAnimator = catModel.gameObject.GetComponent<Animator>();
		//barkRadius = doggie.barkRadius;
		transform.position = new Vector3(transform.position.x,1f,transform.position.z); // Height Fix  8===D
		// Attack  8===D
		vAtkBox.SetActive (false);

	}
	// Use this for initialization
	void Start () {
		Sweat.gameObject.SetActive (false);
		vMoving = true;
		OrbDetected = false;
		Blockingtime = 0f;
		currentState = "none";
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
		if (currentState != "isInsideOrb") {
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 10f); // rotation code i found that actually works pretty good
		} else {
			gameObject.transform.LookAt (OrbLookpos);
		}
		// cc.Move(Direction * speed * Time.deltaTime); // Went to Walk() 8===D

		if (vAtkHere) { // 8===D
			vAtkTime += Time.deltaTime;
			if (vAtkTime >= vAtkLS) {
				vAtkHere = false;
				vAtkBox.SetActive (false);
			}
		}

		// Bounce 8===D

			if (aBounceFrame > 0f)
				aBounceFrame += .05f;
			if (aBounceFrame > 1f)
				aBounceFrame = 0f;
			float tTmp = aBounce.Evaluate (aBounceFrame) / 3f;

		vModel.transform.localPosition = new Vector3(0f,tTmp,0f);
	}

	void LateUpdate(){
		transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
	}
	void OnTriggerEnter(Collider impactObject){
		if (impactObject.name == "DogWeapon" && currentState != "isGettingHit" && EnemyCode == 1) {
			Debug.Log ("collider Working");
			if (impactObject.GetComponent<Scr_Hitbox>().vIsWeapon == true)
			StartGetHit ();
		}
		if (impactObject.name == "DogWeapon" && currentState == "isGettingFear" && currentState != "isGettingHit" && EnemyCode == 2) {
			Debug.Log ("collider Working");
			StartGetHit ();
		}
		if (impactObject.name == "DogWeapon" && currentState != "isGettingFear" && currentState != "isGettingHit" && EnemyCode == 2) {
			StartBlock ();
		}
	}

	void RunStates(){
		if (currentState == "isInsideOrb") {
			Orb ();
		}
		else if (timeScared > ScareCooldown && EnemyCode == 1 && currentState == "isGettingFear" && currentState !="isGettingHit" ) {
			startGetScare ();
		}
		else if(currentState == "isIdling" && DogDistance <= attackRange && currentState != "isGettingFear" && vMoving){
			StartAttack ();
		}
		//Walk
		else if ( currentState == "isIdling" && DogDistance > attackRange && vMoving) {
			StartWalk ();
		}
		//Idle
		else if (currentState == "none")
		{StartIdle();}

		//Run ongoing states 8===D
		switch (currentState) {
		case "isGettingFear": 		GetFear (); 	break;
		case "isGettingScared":  	GetScare (); 	break;
		case "isGettingHit":  		GetHit (); 		break;
		case "isAttacking":  		Attack (); 		break;
		case "isWalking":  			Walk (); 		break;
		case "isIdling":  			Idle (); 		break;
		case "isInsideOrb":			Orb ();			break;
		case "isBlocking":			Blocking ();	break;
		}
	}
	void ResetStates(){
		StopIdle ();
		StopAttack ();
		StopWalk ();
		StopGetHit ();
		StopGetFear ();
		StopGetScare ();
		timeScared = 0;
		myAnimator.SetBool ("Idle", false);
		myAnimator.SetBool ("Attacking", false);
		if(EnemyCode == 2)
			myAnimator.SetBool ("Blocking", false);
	}

	//Idle States
	void StartIdle(){
		ResetStates ();
		currentState = "isIdling";
		myAnimator.SetBool ("Idle", true);
	}
	void Idle(){
		rotation = Quaternion.LookRotation(Direction); // Added 8===D
	}
	void StopIdle(){
		currentState = "none";
	}
	//Atthack States
	void StartAttack(){
		ResetStates ();
		currentState = "isAttacking";
		if (!vAtkHere)
		{
			vAtkTime = 0f;
			vAtkHere = true;
			vAtkBox.SetActive (true);
			Swing.Play ();
			}
		myAnimator.SetBool ("Attacking", true);
		//activate weapon collider
	}

	void Attack(){
		//animation condition 8===D
		if (!vAtkHere)
			StopAttack();
	}
	void StopAttack(){
		currentState = "none";
		myAnimator.SetBool ("Attacking", false);
		//disable weapon collider
	}
	//Walking states
	void StartWalk(){
		ResetStates ();
		currentState = "isWalking";
		myAnimator.SetBool ("Idle", true);
	}
	void Walk(){
		Sweat.Stop ();
		speed = normalSpeed;
		rotation = Quaternion.LookRotation(Direction);
		cc.Move(Direction * speed * Time.deltaTime); // Will move only at walk 8===D
		if (aBounceFrame <= 0f) // 8===D
			aBounceFrame += .001f; // 8===D
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
		myAnimator.SetBool ("Idle", true);
		ResetStates ();
		currentState = "isGettingHit";
	}
	void GetHit(){
		catHealth.GetDamaged ();
	}
	void StopGetHit(){
		currentState = "none";
		//set animation here
	}
	//Getting Bark states
	void StartGetFear(){
		ResetStates ();
		meow.Play ();
		currentState = "isGettingFear";
		myAnimator.SetBool ("Idle", true);
		Sweat.gameObject.SetActive (true);
		Sweat.Play ();
	}
	void GetFear(){
		speed = scareSpeed;
		ScapeDog ();
		rotation = Quaternion.LookRotation(Direction);
		if (OrbDetected) {
			OrbDirection = OrbLocation - transform.position ;
			OrbDirection.y = 0f;
			OrbDirection = OrbDirection.normalized;
			Direction = OrbDirection;
		}
		cc.Move(Direction * speed * Time.deltaTime); // Walk while fear 8===D
		if (aBounceFrame <= 0f) // 8===D
			aBounceFrame += .001f; // 8===D
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
		ResetStates ();
		currentState = "isGettingScared";
		myAnimator.SetBool ("Idle", true);
	}
	void GetScare(){
		ScapeDog ();
		rotation = Quaternion.LookRotation(Direction);
		Direction = Vector3.zero;
	}
	void StopGetScare(){
		currentState = "none";
	}

	// Club Cat Orb mode
	void StartOrb(){
		ResetStates ();
		currentState = "isInsideOrb";
	}
	void Orb(){
			//OrbLocation.y = 0.1f;
			rotation = Quaternion.LookRotation (OrbLookpos);
			Direction = Vector3.zero;
		if (!vAtkHere) {
			myAnimator.SetBool ("Attacking", false);
			myAnimator.SetBool ("Idle", true);
		}
		
	}
	// Spear cat Block
	void StartBlock(){
		ResetStates ();
		currentState = "isBlocking";
		myAnimator.SetBool ("Blocking", true);
	}
	void Blocking(){
		Blockingtime += Time.deltaTime;
		if (Blockingtime >= BlockCooldown) {
			StopBlock ();
			Blockingtime = 0f;
		}
	}
	void StopBlock(){
		currentState = "none";
		myAnimator.SetBool ("Blocking", false);
	}

	void GetDogDistance(){
		DogDistance = Vector3.Distance(proDog.transform.position,transform.position);
	}

	void DogBarking(){
		if(DogDistance < barkRadius && currentState != "isInsideOrb" && currentState != "isGettingHit")
			StartGetFear ();
		if (currentState == "isInsideOrb") {
			if (!vAtkHere)
			{
				vAtkTime = 0f;
				vAtkHere = true;
				vAtkBox.SetActive (true);
			}
			myAnimator.SetBool ("Idle", false);
			myAnimator.SetBool ("Attacking", true);
		}
	}
	void DogDirection(){
		Direction = proDog.transform.position - transform.position;
		Direction.y = 0;
		Direction = Direction.normalized;
	}
	void ScapeDog(){
		Direction *= -1;
	}
	void EnteringOrb(){
		if (EnemyCode == 1) {
			StartOrb ();
		}
	}
	void StartMoving(bool TF){
		vMoving = TF;/*
		if (vMoving)
			vMoving = TF;
		else
			vMoving = TF;*/
	}
	public void OrbDir(Vector3 orbLocated){
		OrbDetected = true;
		OrbLocation = orbLocated;
	}
	public void OrbLook(Vector3 orbLook){
		OrbLookpos = orbLook;
		OrbLookpos.y = 1.2f;
	}
	void isDead(){
		Instantiate (Skull, transform.position, Quaternion.identity);
		if (EnemyCode == 1) {
			Vector3 tempPos = new Vector3(transform.position.x,transform.position.y,transform.position.z + 0.5f);
			Instantiate (Bone, tempPos, Quaternion.identity);
		}
		if (EnemyCode == 2) {
			Vector3 tempPos = new Vector3(transform.position.x,transform.position.y,transform.position.z + 0.5f);
			Instantiate (Bone, tempPos, Quaternion.identity);
			tempPos = new Vector3(transform.position.x - 0.5f,transform.position.y,transform.position.z - 0.5f);
			Instantiate (Bone, tempPos, Quaternion.identity);
			tempPos = new Vector3(transform.position.x + 0.5f,transform.position.y,transform.position.z - 0.5f);
			Instantiate (Bone, tempPos, Quaternion.identity);
		}
	}
}
