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
	public int vCurtRoom = 0;

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
		vNextRoom = vNMessage;
		vTransition = true;
		vOpen = false;
	}
	void Update () {
		if (vTransition) {
			if (!vOpen) {
				if (vScale > 0f) {
					vScale -= vScaleSpeed;
					MakeThemAct (true);
				}
				else {
					vScale = 0f;
					vOpen = true;
					//SceneManager.LoadScene ("Assets/_Scenes/"+vNextRoom+".unity");
					FilterRoom();
					//Scr_FilterRoom Script = GameObject.GetComponent<Scr_FilterRoom>();

					//Script.FilterRoom (vNextRoom, tInt);
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
		Those = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject That in Those) {
			vPlayerComp = That.GetComponent<Scr_Player> ();
			vPlayerComp.vActing = TF;
		}
	}


	void FilterRoom(){
		vCurtRoom = SceneManager.GetActiveScene ().buildIndex;
		switch (vNextRoom) {
		case 0:
			vPlayer.transform.position = new Vector3 (0f, 2f, -3f);
			break;
		case 1:
			if (vCurtRoom == 0)
				vPlayer.transform.position = new Vector3 (0f, 2f,6f);
			if (vCurtRoom == 2)
				vPlayer.transform.position = new Vector3 (3f, 2f, -3f);
			if (vCurtRoom == 3)
				vPlayer.transform.position = new Vector3 (-3f, 2f, -3f);
			if (vCurtRoom == 4)
				vPlayer.transform.position = new Vector3 (0f, 2f, -6f);
			break;
		case 2:
			vPlayer.transform.position = new Vector3 (-1f, 2f, -1f);
			break;
		case 3:
			vPlayer.transform.position = new Vector3 (1f, 2f, -1f);
			break;
		case 4:
			vPlayer.transform.position = new Vector3 (0f, 2f, 8f);
			break;
		}
		SceneManager.LoadScene(vNextRoom);
		//vCurtRoom = 
	}
	
}
