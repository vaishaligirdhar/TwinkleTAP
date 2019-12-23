using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphearScript : MonoBehaviour {

	public static bool first = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (first) {
			GameObject temp = GameObject.Find ("Note Player_twinkle");
			AudioSource tempAudio = temp.GetComponent<AudioSource> ();
			tempAudio.enabled = true;
			tempAudio.Play ();
			first = false;
		}
		

		int temp1 = PlayerPrefs.GetInt ("max");
		PlayerPrefs.SetInt ("max", temp1+1);

		if (col.gameObject.name.Equals("Sphere(Clone)")) {
			GameObject.Find ("Indicators(Clone)/Kick").SendMessage ("OnNoteOn");
			Destroy (col.gameObject);
		}else if(col.gameObject.name.Equals("Sphere (1)(Clone)")){
			GameObject.Find ("Indicators(Clone)/Hat").SendMessage ("OnNoteOn");
			Destroy (col.gameObject);
		}else if(col.gameObject.name.Equals("Sphere (2)(Clone)")){
			GameObject.Find ("Indicators(Clone)/Snare").SendMessage ("OnNoteOn");
			Destroy (col.gameObject);
		}else if(col.gameObject.name.Equals("Sphere (3)(Clone)")){
			GameObject.Find ("Indicators(Clone)/OHat").SendMessage ("OnNoteOn");
			Destroy (col.gameObject);
		}else{
		}
	}
}
