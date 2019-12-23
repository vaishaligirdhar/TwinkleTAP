using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ChangeScene : MonoBehaviour {

	public void changeScene(int change){
		GameObject.Find ("bgMusic").GetComponent<AudioSource>().volume = 1.0f;
		SceneManager.LoadScene (change);
	}

	// Use this for initialization
	void Start () {
		
	}

	/*public void setSongName(){
		PlayerPrefs.SetString ("song", EventSystem.current.currentSelectedGameObject.name);
	}*/

	// Update is called once per frame
	void Update () {
		
	}
}
