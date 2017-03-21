using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BossRoom : MonoBehaviour {
	private bool CatsAareDone;
	public GameObject vNewCats;
	public GameObject vRewardA;
	public GameObject vRewardB;
	public bool vMakeItRain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int tInt = 0;
		GameObject tGO;
		GameObject[] Those = GameObject.FindGameObjectsWithTag ("Cat");
		foreach (GameObject That in Those) {
			tInt += 1;
		}
		if (tInt <= 0) {
			if (!CatsAareDone) {
				tGO = Instantiate (vNewCats);
				tGO.SendMessage ("StartMoving", true);
				tGO.transform.position = new Vector3 (.5f, 2f, -5f);
				tGO = Instantiate (vNewCats);
				tGO.SendMessage ("StartMoving", true);
				tGO.transform.position = new Vector3 (-6f, 2f, 1.5f);
				tGO = Instantiate (vNewCats);
				tGO.SendMessage ("StartMoving", true);
				tGO.transform.position = new Vector3 (7f, 2f, 1.5f);
				CatsAareDone = true;
			} else{
				vMakeItRain = true;
				tGO = GameObject.FindGameObjectWithTag("Canvas");
				GameObject tGOsub = tGO.transform.FindChild("Spr_Congrats").gameObject;
				tGOsub.SetActive(true);
		}
		}
		tInt = 0;
		Those = GameObject.FindGameObjectsWithTag ("Collectable");
		foreach (GameObject That in Those) {
			tInt += 1;
		}
		if (vMakeItRain && tInt < 40 && Random.Range(0,30) == 1) {
			if (Random.value < .5f)
				tGO = Instantiate (vRewardA);
			else
				tGO = Instantiate (vRewardB);
			tGO.transform.position = new Vector3 (Random.Range(-2,3), 6f,Random.Range(-2,3));

		}

	}
}
