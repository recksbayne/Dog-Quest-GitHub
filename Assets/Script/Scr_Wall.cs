using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Wall : MonoBehaviour {
	static char sSide;
	public char tTmp;
	public bool vN;
	public bool vE;
	public bool vS;
	public bool vW;
	static MeshRenderer vMR;
	// Use this for initialization
	void Start(){
		vMR = this.GetComponent<MeshRenderer> ();
	}
	void FunSideCheck(char NESW){
		sSide = NESW;
		tTmp = sSide;
		switch (tTmp) {
		case 'N':
			if (vN)
				this.GetComponent<MeshRenderer> ().enabled = false;
			else
				this.GetComponent<MeshRenderer> ().enabled = true;
			break;
		case 'E':
			if (vE)
				this.GetComponent<MeshRenderer> ().enabled = false;
			else
				this.GetComponent<MeshRenderer> ().enabled = true;
			break;
		case 'S':
			if (vS)
				this.GetComponent<MeshRenderer> ().enabled = false;
			else
				this.GetComponent<MeshRenderer> ().enabled = true;
			break;
		case 'W':
			if (vW)
				this.GetComponent<MeshRenderer> ().enabled = false;
			else
				this.GetComponent<MeshRenderer> ().enabled = true;
			break;
		}
	}
}
