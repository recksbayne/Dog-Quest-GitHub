using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BossRoom : MonoBehaviour {
	private bool CatsAareDone;
	public GameObject vNewCats;
	public GameObject vReward;
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
		if (tInt <= 0){
			if (!CatsAareDone) {
				tGO = Instantiate (vNewCats);
				tGO.transform.position = new Vector3 (.5f, 2f, -2f);
				tGO = Instantiate (vNewCats);
				tGO.transform.position = new Vector3 (-3f, 2f, 1.5f);
				tGO = Instantiate (vNewCats);
				tGO.transform.position = new Vector3 (4f, 2f, -2f);
				CatsAareDone = true;
			} else
				vMakeItRain = true;
			}
		if (vMakeItRain) {
			tGO = Instantiate (vNewCats);
			tGO.transform.position = new Vector3 (Random.Range(-2f,3f), 2f,Random.Range(-2f,3f));

		}

	}
}
