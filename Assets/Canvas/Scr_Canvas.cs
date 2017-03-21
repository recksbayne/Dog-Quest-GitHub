using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Canvas : MonoBehaviour {
	public GameObject vPlayer;
	private Scr_Player vPlayerComp;
	public GameObject vHole;
	public GameObject vBlackN;
	public GameObject vBlackE;
	public GameObject vBlackS;
	public GameObject vBlackW;
	public bool vTransition; // If start to transition
	public bool vOpen; // If opening or closing
	public float vScale;
	public float vScaleSpeed = 2f;
	public float vDistance;
	public int vNextRoom;
	public int vPrevRoom;
	public int vCurtRoom = 0;
	private bool vStartReset;

	// Use this for initialization
	void Awake(){
		
		vTransition = true;
		vScale = 0f;
		vOpen = true;
		vHole.transform.localScale = new Vector3 (vScale, vScale, 1f);
		vBlackN.transform.localPosition = new Vector3 (0f, vScale* 5f+500f, 0f);
		vBlackS.transform.localPosition = new Vector3 (0f, -vScale* 5f-500f, 0f);
		vBlackE.transform.localPosition = new Vector3 (vScale* 5f+500f, 0f,0f);
		vBlackW.transform.localPosition = new Vector3 (-vScale* 5f-500f, 0f,0f);
		vPlayerComp = vPlayer.GetComponent<Scr_Player> ();
	}
		
	// Update is called once per frame
	void GotoNextRoom(int vNMessage){
		//vPrevRoom = vCurtRoom;
		if (vNMessage == -1){
			vNextRoom = vCurtRoom;
		vStartReset = true;
	}
		else {
			vNextRoom = vNMessage;
		}
		vTransition = true;
		vOpen = false;
	}
	void Update () {
		if (vTransition) {
			CatsMove (false);
			if (!vOpen) {
				if (vScale > 0f) {
					vScale -= vScaleSpeed;
					MakeThemAct (true);
				}
				else {
					vScale = 0f;
					vOpen = true;
					this.SendMessage ("LightUp");
					if (vStartReset) {
						vPlayer.SendMessage ("ResetLife");
					}
					vStartReset = false;
					FilterRoom();
					Debug.Log ("I should change");
				}
			} else {
				if (vScale < 260f) {
					vScale += vScaleSpeed;
					MakeThemAct (true);
				}
				else {
					vScale = 260f;
					vTransition = false;
					MakeThemAct (false);
					CatsMove (true);
				}
			}
			vHole.transform.localScale = new Vector3 (vScale, vScale, 1f);
			vBlackN.transform.localPosition = new Vector3 (0f, vScale* 5f+500f, 0f);
			vBlackS.transform.localPosition = new Vector3 (0f, -vScale* 5f-500f, 0f);
			vBlackE.transform.localPosition = new Vector3 (vScale* 5f+500f, 0f,0f);
			vBlackW.transform.localPosition = new Vector3 (-vScale* 5f-500f, 0f,0f);
		}
	}
	void MakeThemAct(bool TF){
		GameObject[] Those;
		Those = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject That in Those) {
			vPlayerComp = That.GetComponent<Scr_Player> ();
			vPlayerComp.vActing = TF;}
		/*Those = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject That in Those) {
			vPlayerComp = That.GetComponent<Scr_Player> ();
			vPlayerComp.vActing = TF;
		}*/

	}
	void CatsMove(bool TF){
		GameObject[] cats;
		cats = GameObject.FindGameObjectsWithTag ("Cat");
		foreach (GameObject That in cats) {
			That.gameObject.SendMessage ("StartMoving", TF);
		}
	}


	void FilterRoom(){
		Debug.Log ("FilterRoom");
		//vPrevRoom = vCurtRoom;
		vCurtRoom = SceneManager.GetActiveScene ().buildIndex;
		//if (vNextRoom == 0)
			
		switch (vNextRoom) {
		case 18:
			vPlayer.transform.position = new Vector3 (0f, 2f, -1f);
			break;
		case 0:
			vPlayer.transform.position = new Vector3 (0f, 2f, -1f);
			break;
		case 1:
			if (vCurtRoom == 18)
				vPlayer.transform.position = new Vector3 (-.5f, 2f,6f);
			if (vCurtRoom == 0)
				vPlayer.transform.position = new Vector3 (-.5f, 2f,6f);
			if (vCurtRoom == 2)
				vPlayer.transform.position = new Vector3 (3f, 2f, -0.5f);
			if (vCurtRoom == 3)
				vPlayer.transform.position = new Vector3 (-4f, 2f, -.5f);
			if (vCurtRoom == 4)
				vPlayer.transform.position = new Vector3 (0.5f, 2f, -6f);
			break;
		case 2:
			vPlayer.transform.position = new Vector3 (-1f, 2f, -0.5f);
			break;
		case 3:
			vPlayer.transform.position = new Vector3 (1f, 2f, 1.5f);
			break;
		case 4:
			if (vCurtRoom == 1)
				vPlayer.transform.position = new Vector3 (-.5f, 2f, 6f);
			if (vCurtRoom == 5)
				vPlayer.transform.position = new Vector3 (-.5f, 2f, -5f);
			break;
		case 5:
			if (vCurtRoom == 4)
				vPlayer.transform.position = new Vector3 (.5f, 2f, 3f);
			if (vCurtRoom == 6)
				vPlayer.transform.position = new Vector3 (-2f, 2f, 0f);
			break;
		case 6:
			if (vCurtRoom == 5)
				vPlayer.transform.position = new Vector3 (2f, 2f, -2f);
			if (vCurtRoom == 7)
				vPlayer.transform.position = new Vector3 (-.5f, 2f, 4f);
			if (vCurtRoom == 14)
				vPlayer.transform.position = new Vector3 (-6f, 2f, -1.5f);
			if (vCurtRoom == 15)
				vPlayer.transform.position = new Vector3 (5f, 2f, -1.5f);
			if (vCurtRoom == 17)
				vPlayer.transform.position = new Vector3 (-.5f, 2f, -7f);
			break;
		case 7:
			if (vCurtRoom == 6)
				vPlayer.transform.position = new Vector3 (-.5f, 2f, -4f);
			if (vCurtRoom == 8)
				vPlayer.transform.position = new Vector3 (-.5f, 2f, 5f);
			break;
		case 8:
			if (vCurtRoom == 7)
				vPlayer.transform.position = new Vector3 (.5f, 2f, -3f);
			if (vCurtRoom == 9)
				vPlayer.transform.position = new Vector3 (.5f, 2f, 8f);
			break;
		case 9:
				vPlayer.transform.position = new Vector3 (-.5f, 2f, -2f);
			break;
		case 10:
			if (vCurtRoom == 7)
				vPlayer.transform.position = new Vector3 (3f, 2f, -5f);
				vPlayer.GetComponent<Scr_Player>().vIsOnSand = false;
			if (vCurtRoom == 11)
				vPlayer.transform.position = new Vector3 (.5f, 2f, 8f);
			if (vCurtRoom == 13)
			vPlayer.transform.position = new Vector3 (-3f, 2f, -4.5f);
			break;
		case 11:
			if (vCurtRoom == 10)
				vPlayer.transform.position = new Vector3 (.5f, 2f, -5f);
			if (vCurtRoom == 12)
				vPlayer.transform.position = new Vector3 (.5f, 2f, 5f);
			break;
		case 12:
				vPlayer.transform.position = new Vector3 (-.5f, 2f, -8f);
			break;
		case 13:
			if (vCurtRoom == 10)
				vPlayer.transform.position = new Vector3 (-1.5f, 2f, -1.5f);
			if (vCurtRoom == 14)
				vPlayer.transform.position = new Vector3 (-3.5f, 2f, -2f);
			break;
		case 14:
			if (vCurtRoom == 13)
				vPlayer.transform.position = new Vector3 (-2.5f, 2f, 1f);
			if (vCurtRoom == 6)
				vPlayer.transform.position = new Vector3 (-.5f, 2f, -1.5f);
			break;
		case 15:
			if (vCurtRoom == 16)
				vPlayer.transform.position = new Vector3 (-.5f, 2f, 8.5f);
			if (vCurtRoom == 6)
				vPlayer.transform.position = new Vector3 (-2f, 2f, -4f);
			break;
		case 16:
			vPlayer.transform.position = new Vector3 (-2f, 2f, -3f);
			break;
		case 17:
			vPlayer.transform.position = new Vector3 (.5f, 2f, 8f);
			break;
		}
		SceneManager.LoadScene(vNextRoom);

		//vCurtRoom = 
	}
	
}
