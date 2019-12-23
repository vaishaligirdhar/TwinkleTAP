using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePlay : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addKick(){
		int score = PlayerPrefs.GetInt ("score");
		Vector3 objScaleKick = GameObject.Find ("Indicators(Clone)/Kick").transform.localScale;
		if (objScaleKick.x > 1.2f) {
			Debug.Log (score);
			score += (int)objScaleKick.x;
			PlayerPrefs.SetInt ("score", score);
		}
	}

	public void addHat(){
		int score = PlayerPrefs.GetInt ("score");
		Vector3 objScaleSnare = GameObject.Find ("Indicators(Clone)/Hat").transform.localScale;
		if (objScaleSnare.x > 1.2f) {
			Debug.Log (score);
			score += (int)objScaleSnare.x;
			PlayerPrefs.SetInt ("score", score);
		}
	}

	public void addOHat(){
		int score = PlayerPrefs.GetInt ("score");
		Vector3 objScaleKick = GameObject.Find ("Indicators(Clone)/OHat").transform.localScale;
		if (objScaleKick.x > 1.2f) {
			Debug.Log (score);
			score += (int)objScaleKick.x;
			PlayerPrefs.SetInt ("score", score);
		}
	}

	public void addSnare(){
		int score = PlayerPrefs.GetInt ("score");
		Vector3 objScaleSnare = GameObject.Find ("Indicators(Clone)/Snare").transform.localScale;
		if (objScaleSnare.x > 1.2f) {
			Debug.Log (score);
			score += (int)objScaleSnare.x;
			PlayerPrefs.SetInt ("score", score);
		}
	}
		
}
