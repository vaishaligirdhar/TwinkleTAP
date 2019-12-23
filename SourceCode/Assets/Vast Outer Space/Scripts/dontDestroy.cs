using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour {

	private static dontDestroy _instance;

	void Awake(){
		if (!_instance)
			_instance = this;
		else
			Destroy (this.gameObject);
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
