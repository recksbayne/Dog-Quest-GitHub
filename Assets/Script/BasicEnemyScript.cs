using System.Collections;
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
	public float ScareRadius; //radius area where the cat will run in the oposite direction of the dog
	public float barkRadius; // set the radius of the bark
	public float lifepoints; //if we need them
	public int EnemyCode;// 1 for club-cat 2 for spear cat 3 for possible boss
	public float ScareCooldown;//if the scare has a cooldown
	public float timeScared;//time the cat is beaing in getbark state
	public Vector3 OrbLocation;//locate the orb
	public Vector3 OrbDirection; //Orb direction

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


	void Awake(){
		timeScared = 0f;
		proDog = GameObject.FindGameObjectWithTag ("Player");
		cc = gameObject.GetComponent<CharacterController> ();
		barkRadius = testingBark.barkRadius;
		//barkRadius = doggie.barkRadius;
		transform.position = new Vector3(transform.position.x,1f,transform.position.z); // Height Fix  8===D
		// Attack  8===D
		vAtkBox.SetActive (false);
	}
	// Use this for initialization
	void Start () {
		GameObject CurrentOrb = GameObject.FindGameObjectWithTag ("Orb");
		OrbLocation = CurrentOrb.gameObject.transform.position;
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
		if (impactObject.name == "DogWeapon" && currentState != "isGettingHit") {
			StartGetHit ();
		}
		//if (impactObject.name == "Obj_Bark") // 8===D
		//StartGetFear ();
	}

	void RunStates(){
		if (currentState == "isInsideOrb") {
			Orb ();
		}
		else if (DogDistance > ScareRadius && EnemyCode == 1 && currentState == "isGettingFear") {
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

		//Run ongoing states 8===D
		switch (currentState) {
		case "isGettingFear": 		GetFear (); 	break;
		case "isGettingScared":  	GetScare (); 	break;
		case "isGettingHit":  		GetHit (); 		break;
		case "isAttacking":  		Attack (); 		break;
		case "isWalking":  			Walk (); 		break;
		case "isIdling":  			Idle (); 		break;
		case "isInsideOrb":			Orb ();			break;
		}
		/*
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
		*/
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
		rotation = Quaternion.LookRotation(Direction); // Added 8===D
	}
	void StopIdle(){
		currentState = "none";
	}
	//Atthack States
	void StartAttack(){
		currentState = "isAttacking";
		Debug.Log ("Attack Started");  // 8===D
		if (!vAtkHere)
		{
			Debug.Log ("Attacked");
			vAtkTime = 0f;
			vAtkHere = true;
			vAtkBox.SetActive (true);
			}
		//activate weapon collider
	}
	void Attack(){
		//animation condition 8===D
		if (!vAtkHere)
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
		speed = scareSpeed;
		ScapeDog ();
		rotation = Quaternion.LookRotation(Direction);
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
		OrbDirection = OrbLocation - transform.position;
		rotation = Quaternion.LookRotation(OrbDirection);
		Direction = Vector3.zero;
		Debug.Log ("enter");
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
	void EnteringOrb(){
		if (EnemyCode == 1) {
			StartOrb ();
		}
	}
}



/*
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
		proDog = GameObject.FindGameObjectWithTag ("Player");
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
		// cc.Move(Direction * speed * Time.deltaTime); // Went to Walk() 8===D

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

		//Run ongoing states 8===D
		switch (currentState) {
		case "isGettingFear": 		GetFear (); 	break;
		case "isGettingScared":  	GetScare (); 	break;
		case "isGettingHit":  		GetHit (); 		break;
		case "isAttacking":  		Attack (); 		break;
		case "isWalking":  			Walk (); 		break;
		case "isIdling":  			Idle (); 		break;
		}

		//if (currentState == "isGettingFear")
		//	GetFear ();
		//else if (currentState == "isGettingScared")
		//	GetScare ();
		//else if (currentState == "isGettingHit")
		//	GetHit ();
		//else if (currentState == "isAttacking")
		//	Attack ();
		//else if (currentState == "isWalking")
		//	Walk ();
		//else if (currentState == "isIdling")
		//	Idle ();
		
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
		rotation = Quaternion.LookRotation(Direction); // Added 8===D
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
		cc.Move(Direction * speed * Time.deltaTime); // Will move only at walk 8===D
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




*/