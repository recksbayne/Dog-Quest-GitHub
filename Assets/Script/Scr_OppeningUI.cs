using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scr_OppeningUI : MonoBehaviour {

	public Image Slide1;
	public Image Slide2;
	public Image Slide3;
	public int SlideCount;
	public Scr_UI nextUI;
	// Use this for initialization
	void Start () {
		SlideCount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && nextUI.Prevcanvas) {
			NextSlide ();
		}
		if (SlideCount == 2) {
			Slide1.enabled = false;
		}
		if (SlideCount == 3) {
			Slide2.enabled = false;
		}
		if (SlideCount == 4) {
			Slide3.enabled = false;
		}
		if (SlideCount == 5) {
			nextUI.Prevcanvas = false;
			Destroy (this.gameObject);
		}
	}
	public void NextSlide(){
		SlideCount += 1;
	}
}
