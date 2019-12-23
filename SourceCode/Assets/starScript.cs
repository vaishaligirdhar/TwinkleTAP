using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class starScript : MonoBehaviour {

    public double threshold;
    //public Text sampleInput;
	public GameObject noStar;
    public GameObject Star;
	// Use this for initialization
	void Start () {
        displayStars(0);
	}
	
	// Update is called once per frame
	void Update () {
        displayStars(0.35f);
	}

	void displayStars(float percent)
    {
		float xyc = noStar.transform.localScale.x;
		noStar.transform.localScale.Set (0.5f, 1.0f, 1.0f);
    }
}
