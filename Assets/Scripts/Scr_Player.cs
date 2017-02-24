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
	public AnimationCurve aBounce;
	public float aBounceFrame;
	private bool vLocked = false; // Is Locked
	private bool vAttack = false; // Is Attacking
	private bool vDigging = false; // Is digging
	private bool vActing = false; // Is Acting

	// Variables
	public Vector3 vDirection;
	public float vSpeed = 5;
	public float vAngle;
	public char vAngNESW = 'N'; // Defines if you are facing north east south or west
	public float vTmp = 1f;
	public float vCamAngle;
	private int vHoz;
	private int vVer;

	// Model Variable
	public GameObject vModel;

	// Compenents and Objects
	public GameObject vCamera;
	private CharacterController cc;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		FunInputCheck ();
		cc.Move (vDirection * Time.deltaTime * vSpeed);

	}
	void LateUpdate(){
		FunCameraFix ();
		FunModelFix ();
	}
	void FunModelFix(){
		if (aBounceFrame > 0f)
			aBounceFrame += .05f;
		if (aBounceFrame > 1f)
			aBounceFrame = 0f;
		float tTmp = aBounce.Evaluate (aBounceFrame)/4f;
		vModel.transform.localPosition = new Vector3(0f,tTmp,0f);
		//vModel.transform.localPosition = new Vector3(0f,aBounce.Evaluate (aBounceFrame),0f);
		//aBounceFrame = aBounce.Evaluate (aBounceFrame);

	}

	void FunCameraFix(){ // Camera Set and rotation
		Vector3 GotoVec3 = new Vector3(0f,0f,0f);
		Vector3 vCamAngle = new Vector3 (15f, -135f, 0f);
		switch (vAngNESW) {
		case 'N':
			GotoVec3 = new Vector3(3f,3f,3f);
			vCamAngle = new Vector3 (15f, -135f, 0f);
			break;
		case 'E':
			GotoVec3 = new Vector3(3f,3f,-3f);
			vCamAngle = new Vector3 (15f, -45f, 0f);
			break;
		case 'S':
			GotoVec3 = new Vector3(-3f,3f,-3f);
			vCamAngle = new Vector3 (15f, 45f, 0f);
			break;
		case 'W':
			GotoVec3 = new Vector3(-3f,3f,3f);
			vCamAngle = new Vector3 (15f, 135f, 0f);
			break;
		}
		if (vCamera.transform.localPosition != GotoVec3)
			{vCamera.transform.localPosition = GotoVec3*vTmp;
			vCamera.transform.eulerAngles = vCamAngle;
				//Vector3.lerp (vCamera.transform.localPosition, new Vector3 (3f, 3f, 3f));
			
		}
	}

	void FunInputCheck(){
		// Reseting Variables
		vHoz = 0;
		vVer = 0;

		// Checking for buttons
		if (vLocked || vActing)
			return;

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
		vDirection = new Vector3 (vHoz, 0f, vVer);
		
		if (Input.GetKey (KShovel) || Input.GetKey (JShovel)) {
		}
		if (Input.GetKey (KSniff) || Input.GetKey (JSniff)){}
		if (Input.GetKey (KBark) || Input.GetKey (JBark)){}
		if (Input.GetKey (KAction) || Input.GetKey (JAction)) {
		}

		if (Input.GetKeyDown (KCamRotationL) || Input.GetKeyDown (JCamRotationL)) {
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
		}
		if (Input.GetKeyDown (KCamRotationR) || Input.GetKeyDown (JCamRotationR)) {
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
		}
		DirConvertToCam ();
	}
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
		if (vDirection.magnitude > 0f) {
			if (aBounceFrame <= 0f)
				aBounceFrame += .001f;
		}
	}
}
