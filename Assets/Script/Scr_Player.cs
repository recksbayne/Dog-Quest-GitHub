using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player : MonoBehaviour {
	// KeyCode Input
	public KeyCode KLeft; 
	public KeyCode KUp;
	public KeyCode KRight;
	public KeyCode KDown;
	public KeyCode KShovel;
	public KeyCode KSniff;
	public KeyCode KBark;
	public KeyCode KAction; // Extra Action button
	public KeyCode KCamRotationL;  // Camera rotation
	public KeyCode KCamRotationR;  // Camera rotation

	// Joystick version
	public KeyCode JLeft;
	public KeyCode JUp;
	public KeyCode JRight;
	public KeyCode JDown;
	public KeyCode JShovel;
	public KeyCode JSniff;
	public KeyCode JBark;
	public KeyCode JAction;
	public KeyCode JCamRotationL;
	public KeyCode JCamRotationR;

	// Status Check
	public string vState; // 'State Machine'
	public string vSubState; // I may need this for other info
	public AnimationCurve aBounce; // Bounce effect
	public float aBounceFrame;
	public AnimationCurve aDigUnder; // Underground effect
	public float aDigUnderFrame;
	private bool vLocked = false; // Is Locked
	public bool vUnderground = false; // Is Locked
	public bool vIsOnSand = false; // Check if is on sand
	private bool vAttack = false; // Is Attacking
	public float vAttackCD; // Attack CoolDown
	private bool vDigging = false; // Is digging
	public bool vActing = false; // Is Acting

	// Variables
	public Vector3 vDirection;
	public float vSpeed = 5;
	public float vAngle;
	public char vAngNESW = 'N'; // Defines if you are facing north east south or west
	public float vTmp = 1f;
	public float vCamAngle;
	private int vHoz;
	private int vVer;
	public GameObject vTakenObj; // Object Taken from another variable
	public bool vObjIsHere; // Object is still on trigger zone
	public Vector3 vVectorPrevious; // xprevious, yprevious, zprevious;

	// Camera Angler
	private bool vCamIsMoving;
	public float vCamTime;
	private Vector3 vPointFrom;
	private Vector3 vPointTo;
	private Vector3 vAngleFrom;
	private Vector3 vAngleTo;
	public Vector3 TempVectorA;
	public Vector3 TempVectorB;

	// Model Variable
	public GameObject vModel;
	public string vActionType; // An action to use

	// Compenents and Objects
	public GameObject vCamera;
	public GameObject vCamLock;
	private CharacterController cc;
	public GameObject vCanvas;
	public Scr_Health cHc;

	// Attack Parts
	public bool vHasAtk; // Does it have the shovel? Y/N
	public GameObject vAtkBox;
	private bool vAtkHere; // Attack Box is present T/F
	private float vAtkTime; // Attack Box Countdown to lifespan
	public float vAtkDis = 1.5f; // Attack Distance
	public float vAtkLS; // Attack Box Life Span
	public Vector3 vAtkSpot;

	// Bark Parts
	public GameObject vBarkShpere;
	public bool vBarkHere; // Bark is present T/F
	private float vBarkTime; // Bark Countdown to lifespan
	public float vBarkLS = 1f; // Bark Life Span
	public float vBarkCD; // Bark Cool Down

	//Animations Variables o_o_o_o_o_o_o_o_o_o_o_o

	private Animator myAnimator; //This object's animator component
	private AnimatorStateInfo myAnimatorStateInfo;
	public float myAnimatorNormalizedTime = 0.0f;


	// There can only be one;
	private bool TheOne;
	void Awake(){
		DontDestroyOnLoad (this.transform.gameObject);
		myAnimator = GetComponent<Animator>();
		GameObject[] Those = GameObject.FindGameObjectsWithTag ("Player");
		int tCount = 0;
		foreach (GameObject That in Those) {
			tCount += 1;
		}
		Debug.Log (tCount);
		if (tCount == 1)
			TheOne = true;
		else if (!TheOne)
			Destroy (this.transform.gameObject);
	}
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		cHc = GetComponent < Scr_Health> ();
		vAtkBox.SetActive (false);
		vBarkShpere.SetActive (false);
		SetNESW (vAngNESW);
		vPointFrom = new Vector3 (20f,18f,20f);
		vAngleFrom = new Vector3 (30f, -135f, 0f);
		vCamera.transform.localPosition = new Vector3(20f,18f,20f);
		vCamera.transform.eulerAngles = new Vector3 (30f, -135f, 0f);
		vCamera.transform.LookAt (this.transform);
	}

	void Respawn(){
	}

	// Update is called once per frame
	void Update () {
		vVectorPrevious = transform.position;
		if (vAtkHere) {
			vAtkTime += Time.deltaTime;
			if (vAtkTime >= vAtkLS) {
				vAtkHere = false;
				vAtkBox.SetActive(false);
			}
		}
		if (vBarkHere) {
			vBarkTime += Time.deltaTime;
			if (vBarkTime >= vBarkLS) {
				vBarkHere = false;
				vBarkShpere.SetActive(false);
			}
		}
		if (vBarkCD >= 0f)
			vBarkCD -= Time.deltaTime;
		FunInputCheck ();
		if (vDirection.magnitude > 0f)
		cc.Move (vDirection * Time.deltaTime * vSpeed);
		if (transform.position.y <= -5f)
			transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
		//Animator State Info
		myAnimatorStateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
		myAnimatorNormalizedTime = myAnimatorStateInfo.normalizedTime;

	}
	// Directions, angles, and fixes // Directions, angles, and fixes // Directions, angles, and fixes // Directions, angles, and fixes // Directions, angles, and fixes // Directions, angles, and fixes // Directions, angles, and fixes
	void LateUpdate(){
		if (vCamIsMoving)
		FunCameraFix ();
		FunModelFix ();
		transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
		//if (transform.position.y > 1f)
		//	transform.position = new Vector3 (transform.position.x,1f,transform.position.z);
		Vector3 tTmp = new Vector3 (0f, -1f, 0f);
		cc.Move (tTmp);
		if (!vIsOnSand && vUnderground) {
			vUnderground = false;
			vActing = true;
			vActionType = "Raise";
		}

	}
	// Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup // Model Setup 
	void FunModelFix(){
		float tTmp = 0f;
		switch (vActionType) {
		case "Dig":
			aBounceFrame = 0f;
			if (aDigUnderFrame < 1f)
				aDigUnderFrame += .025f;
			else {
				aDigUnderFrame = 1f;
				vActing = false;
				vActionType = "Under";
			}
			tTmp = -1 * aDigUnder.Evaluate (aDigUnderFrame) * 2f / 3f;// / 4f;
			break;
		case "Under":
			tTmp = -1 * aDigUnder.Evaluate (2f / 3f);// / 4f;
			break;
		case "Raise":
			aBounceFrame = 0f;
			if (aDigUnderFrame > 0f){
				aDigUnderFrame -= .05f;
			vActing = true;
		}
			else{
				aDigUnderFrame = 0f;
				vActing = false;
				vActionType = "";
			}
			tTmp = -1 * aDigUnder.Evaluate (aDigUnderFrame) * 2f / 3f;// / 4f;

			break;
		default:
			if (!vUnderground) {
				aDigUnderFrame = 0f;
				if (aBounceFrame > 0f)
					aBounceFrame += .05f;
				if (aBounceFrame > 1f)
					aBounceFrame = 0f;
				tTmp = aBounce.Evaluate (aBounceFrame) / 4f;
			}
			break;
		}
		Vector3 Temp = new Vector3 (vDirection.x,tTmp,vDirection.z);
		Temp = transform.position - vDirection;
		if (vDirection.magnitude > 0f)
			vModel.transform.LookAt(transform.position-vDirection);
		vModel.transform.localPosition = new Vector3(0f,tTmp,0f);

	}
	// Camera Setup and rotation // Camera Setup and rotation // Camera Setup and rotation // Camera Setup and rotation // Camera Setup and rotation // Camera Setup and rotation // Camera Setup and rotation // Camera Setup and rotation 
	void FunCamNESW(){
		switch (vAngNESW) {
		case 'N':
			vPointTo = new Vector3(20f,18f,20f);
			vAngleTo = new Vector3 (30f, -135f, 0f);
			if (vAngleFrom.y == 135f)
				vAngleFrom.y = -360f + 135f;
			break;
		case 'E':
			vPointTo = new Vector3(20f,18f,-20f);
			vAngleTo = new Vector3 (30f, -45f, 0f);
			break;
		case 'S':
			vPointTo = new Vector3(-20f,18f,-20f);
			vAngleTo = new Vector3 (30f, 45f, 0f);
			break;
		case 'W':
			vPointTo = new Vector3 (-20f, 18f, 20f);
			vAngleTo = new Vector3 (30f, 135f, 0f);
			if (vAngleFrom.y == -135f)
				vAngleFrom.y = 360f - 135f;
			break;
		}
	}

	void FunCameraFix(){ 
		vCamTime += 0.02f;
		if (vCamTime >= 1f) {
			vCamTime = 1f;
			vCamIsMoving = false;
			vPointFrom = vPointTo;
			vAngleFrom = vAngleTo;
		}
		TempVectorA = Vector3.Lerp (vPointFrom, vPointTo, vCamTime);
		TempVectorB = Vector3.Lerp (vAngleFrom, vAngleTo, vCamTime);
		if (vCamera.transform.localPosition != TempVectorA*vTmp)
			{//vCamera.transform.localPosition = TempVectorA*vTmp;
			//vCamera.transform.eulerAngles = TempVectorB;
			vCamLock.transform.eulerAngles = new Vector3(0f,TempVectorB.y-225f,0f);
			SetNESW (vAngNESW);
		}
		vCamera.transform.LookAt (this.transform);
	}
	// Setting NESW visibility for walls // Setting NESW visibility for walls // Setting NESW visibility for walls // Setting NESW visibility for walls // Setting NESW visibility for walls // Setting NESW visibility for walls 
	void SetNESW(char NESW){
		GameObject[] Those = GameObject.FindGameObjectsWithTag ("Wall");
		foreach (GameObject That in Those) {
			That.SendMessage ("FunSideCheck",vAngNESW);
		}

	}
	// Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input // Input 
	void FunInputCheck(){
		vHoz = 0;
		vVer = 0;
		if (vLocked || vActing) {
			vDirection = new Vector3 (vHoz, 0f, vVer);
			DirConvertToCam ();
			return;
		}

		if (Input.GetKey (KeyCode.Space))
			transform.position = new Vector3(2f,1f,2f);
		if (Input.GetKey (KLeft) || Input.GetKey (JLeft)) {
			vHoz -= 1;
			vVer -= 1;
		}
		if (Input.GetKey (KRight) || Input.GetKey (JRight)) {
			vHoz += 1;
			vVer += 1;
		}
		if (Input.GetKey (KDown) || Input.GetKey (JDown)) {
			vVer -= 1;
			vHoz += 1;
		}
		if (Input.GetKey (KUp) || Input.GetKey (JUp)) {
			vVer += 1;
			vHoz -= 1;
		}

		if (vHoz > 1) vHoz = 1;
		if (vVer > 1) vVer = 1;
		if (vHoz < -1) vHoz = -1;
		if (vVer < -1) vVer = -1;

		vDirection = new Vector3 (vHoz, 0f, vVer);
		DirConvertToCam ();

		if (vDirection.magnitude > 0f)  {
			vAtkSpot = vDirection;
			if (!vAtkHere)
				vAtkBox.transform.localPosition = vAtkSpot*vAtkDis;
		}

		if ((Input.GetKey (KShovel) || Input.GetKey (JShovel)) && !vAtkHere && vHasAtk) {
			if (vIsOnSand) {
				vUnderground = true;
				vActing = true;
				vActionType = "Dig";
			} else {
				vAtkTime = 0f;
				vAtkHere = true;
				vAtkBox.SetActive (true);
			}
		}
		if (Input.GetKey (KSniff) || Input.GetKey (JSniff)){}


		if ((Input.GetKey (KBark) || Input.GetKey (JBark)) && vBarkCD <= 0f){
			vBarkTime = 0f;
			vBarkHere = true;
			vBarkCD = 3f;
			vBarkShpere.SetActive(true);

			GameObject[] Enemies = GameObject.FindGameObjectsWithTag ("Cat");
			for (int i = 0; i < Enemies.Length; i++) {
				Enemies[i].gameObject.SendMessage ("DogBarking");
			}
		}
		if (Input.GetKey (KAction) || Input.GetKey (JAction)) {
		}

		if ((Input.GetKeyDown (KCamRotationL) || Input.GetKeyDown (JCamRotationL)) && !vCamIsMoving) {
			vCamIsMoving = true;
			vCamTime = 0f;
			Debug.Log ("Switch Left");
			switch (vAngNESW) {
			case 'N':
				vAngNESW = 'E';
				break;
			case 'E':
				vAngNESW = 'S';
				break;
			case 'S':
				vAngNESW = 'W';
				break;
			case 'W':
				vAngNESW = 'N';
				break;
			}
			FunCamNESW ();
		}
		if ((Input.GetKeyDown (KCamRotationR) || Input.GetKeyDown (JCamRotationR)) && !vCamIsMoving) {
			vCamIsMoving = true;
			vCamIsMoving = true;
			vCamTime = 0f;
			Debug.Log ("Switch Right");
			switch (vAngNESW) {
			case 'N':
				vAngNESW = 'W';
				break;
			case 'E':
				vAngNESW = 'N';
				break;
			case 'S':
				vAngNESW = 'E';
				break;
			case 'W':
				vAngNESW = 'S';
				break;
			}
			FunCamNESW ();
		}
	}
	// Direction to camera direction converter // Direction to camera direction converter // Direction to camera direction converter // Direction to camera direction converter // Direction to camera direction converter 
	void DirConvertToCam(){
		switch (vAngNESW) {
		case 'N':
			vDirection = new Vector3 (vVer*-1,0f,vHoz);
			break;
		case 'E':
			vDirection = new Vector3 (vHoz, 0f, vVer);
			break;
		case 'S':
			vDirection = new Vector3 (vVer, 0f,vHoz*-1 );
			break;
		case 'W':
			vDirection = new Vector3 (vHoz*-1, 0f, vVer*-1);
			break;
		}
		vDirection = vDirection.normalized;
		if (vDirection.magnitude > 0f) {
			if (aBounceFrame <= 0f)
				aBounceFrame += .001f;
		}
	}
	// Trigger // Trigger  // Trigger  // Trigger  // Trigger  // Trigger  // Trigger  // Trigger  // Trigger  // Trigger // Trigger  // Trigger // Trigger  // Trigger // Trigger  // Trigger // Trigger  // Trigger 


	void OnTriggerStay(Collider Other){
		switch (Other.tag) {
			case "SandSpot":
				vIsOnSand = true;
				break;
			case "DigSpot":
			if (vActionType == "Under")
				Other.SendMessage ("FoundYou");
				break;
		case "Spikes":
			//transform.position = vVectorPrevious;
			Vector3 tDirection = Other.transform.position - transform.position;
			cc.Move (tDirection.normalized*-1f);
			cHc.SendMessage ("GetDamaged");
			Debug.Log ("I Stepped on it");
			break;
			}
			

	}
	void OnTriggerExit(Collider Other){
		switch (Other.tag) {
		case "SandSpot":
			vIsOnSand = false;
			break;
		}
	}
	// Go to next room // Go to next room // Go to next room // Go to next room // Go to next room // Go to next room // Go to next room // Go to next room // Go to next room // Go to next room // Go to next room // Go to next room 

}
