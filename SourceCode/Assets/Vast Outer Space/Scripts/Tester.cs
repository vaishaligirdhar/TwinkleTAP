using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SmfLite;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

public class Tester : MonoBehaviour
{
	// Source MIDI file asset.
	public TextAsset sourceFile;
	public GameObject indicators;
	public GameObject animKick;
	public GameObject animHat;
	public GameObject animOHat;
	public GameObject animSnare;
	public GameObject endGameScene;

	GameObject indicatorInstance;
	GameObject tempPlayer;
	string songToPlay;
	bool first = false;

	int count = 0;
	int score = 0;

	// Test settings.
    float bpm = 60;

	// MIDI objects.
    MidiFileContainer song;
    MidiTrackSequencer sequencer;

	void Awake(){

	}

	void onLoad(){
		
	}
    // Start function (MonoBehaviour).
	IEnumerator Start ()
	{
		string tempSong = PlayerPrefs.GetString ("song").Split('.')[0] + "." + PlayerPrefs.GetString("song").Split('.')[1];
		sourceFile = Resources.Load (tempSong) as TextAsset;

		//sourceFile = (TextAsset) UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Music/" + PlayerPrefs.GetString("song"), typeof(TextAsset));

		sphearScript.first = true;

		GameObject.Find ("bgMusic").GetComponent<AudioSource>().volume = 0.05f;

		Regex myRegEx = new Regex ("[-_]+");

		songToPlay = PlayerPrefs.GetString ("song").Split('.')[0];
		string myStr = myRegEx.Replace (songToPlay, " ");
		GameObject.Find ("TitleBoard/Title").GetComponents<UnityEngine.UI.Text> () [0].text = myStr;

		indicatorInstance = Instantiate (indicators, new Vector3 (-1, count, 0), Quaternion.identity);
		indicatorInstance.transform.position = new Vector3(23.792f, 14.434f, 0.209f);
		indicatorInstance.transform.localScale = new Vector3 (0.15f, 0.15f, 0.15f);
		// Load the MIDI song.
		song = MidiFileLoader.Load (sourceFile.bytes);

		PlayerPrefs.SetInt ("score", score);
		PlayerPrefs.SetInt ("max", 0);

        // Wait for one second to avoid stuttering.
		yield return new WaitForSeconds (1.0f);

		first = true;

        // Start sequencing.
        ResetAndPlay (0);
	}

    // Reset and start sequecing.
    void ResetAndPlay (float startTime)
    {   // Play the audio clip.
		AudioClip myClip = (AudioClip) Resources.Load (songToPlay) as AudioClip;
		AudioSource audio = this.GetComponent<AudioSource>();
		audio.clip = myClip;
		audio.volume = 10.0f;

		if (PlayerPrefs.GetString ("song").Contains ("twinkle")) {
			audio.pitch = 0.6f;
		} else if (PlayerPrefs.GetString ("song").Contains ("row")) {
			audio.pitch = 0.4f;
		} else {
			audio.pitch = 0.55f;
		}

        audio.Play();
        //audio.time = startTime;

        // Start the sequencer and dispatch events at the beginning of the track.
        sequencer = new MidiTrackSequencer (song.tracks [1], song.division, bpm);
        DispatchEvents (sequencer.Start (startTime));
    }

    // Update function (MonoBehaviour).
    void Update ()
    {
		first = false;
        if (sequencer != null && sequencer.Playing) {
            // Update the sequencer and dispatch incoming events.
            DispatchEvents (sequencer.Advance (Time.deltaTime));
        }
		if (sequencer != null && !sequencer.Playing) {
			if (GameObject.Find ("endGame(Clone)") == null) {
				GameObject endGameSecneInstance = Instantiate (endGameScene);
				endGameSecneInstance.transform.Find("finalScene").Find("Score")
					.GetComponents<UnityEngine.UI.Text>() [0].text = "Score: " + PlayerPrefs.GetInt ("score");
				GameObject.Find ("ScoreBoard").SetActive (false);
				GameObject.Find ("Indicators(Clone)").SetActive (false);
				GameObject.Find ("bgMusic").GetComponent<AudioSource>().volume = 1.0f;
			}
			//Time.timeScale = 0;
		} else {
			int score = PlayerPrefs.GetInt ("score");
			GameObject.Find ("ScoreBoard/Score").GetComponents<UnityEngine.UI.Text> () [0].text = "Score: " + score.ToString();
		}
    }

    // Dispatch incoming MIDI events. 
    void DispatchEvents (List<MidiEvent> events)
    {
        if (events != null) {
			foreach (var e in events) {
                if ((e.status & 0xf0) == 0x90) {

					byte[] cNotes = {0x00, 0x0c, 0x18, 0x24, 0x30, 0x3c, 0x48, 0x54, 0x60, 0x6c, 0x78};
					byte[] dNotes = {0x02, 0x0e, 0x1a, 0x26, 0x32, 0x3e, 0x4a, 0x56, 0x62, 0x6e, 0x7a};
					byte[] eNotes = {0x04, 0x10, 0x1c, 0x28, 0x34, 0x40, 0x4c, 0x58, 0x64, 0x70, 0x7c};
					byte[] fNotes = {0x05, 0x11, 0x1d, 0x29, 0x35, 0x41, 0x4d, 0x59, 0x65, 0x71, 0x7d};
					byte[] gNotes = {0x07, 0x13, 0x1f, 0x2b, 0x37, 0x43, 0x4f, 0x5b, 0x67, 0x73, 0x7f};
					byte[] aNotes = {0x09, 0x15, 0x21, 0x2d, 0x39, 0x45, 0x51, 0x5d, 0x69, 0x75};
					byte[] bNotes = {0x0b, 0x17, 0x23, 0x2f, 0x3b, 0x47, 0x53, 0x5f, 0x6b, 0x77};

					int cPos = Array.IndexOf (cNotes, e.data1);
					int dPos = Array.IndexOf (dNotes, e.data1);
					int ePos = Array.IndexOf (eNotes, e.data1);
					int fPos = Array.IndexOf (fNotes, e.data1);
					int gPos = Array.IndexOf (gNotes, e.data1);
					int aPos = Array.IndexOf (aNotes, e.data1);
					int bPos = Array.IndexOf (bNotes, e.data1);

					if (cPos > -1) {
						Queue<GameObject> objectQueue = new Queue<GameObject> ();
						objectQueue.Enqueue (Instantiate (animKick));
						//GameObject temp = Instantiate (animKick);
						//temp.transform.SetParent (indicatorInstance.transform);
						Destroy (objectQueue.Peek(), objectQueue.Peek().GetComponents<Animator>()[0].GetCurrentAnimatorStateInfo(0).length);

						//GameObject.Find ("Indicators(Clone)/Kick").SendMessage ("OnNoteOn");
					} else if (dPos > -1) {
						Queue<GameObject> objectQueue = new Queue<GameObject> ();
						objectQueue.Enqueue (Instantiate (animHat));
						Destroy (objectQueue.Peek(), objectQueue.Peek().GetComponents<Animator>()[0].GetCurrentAnimatorStateInfo(0).length);

						//GameObject.Find ("Indicators(Clone)/Hat").SendMessage ("OnNoteOn");
					} else if (ePos > -1) {
						Queue<GameObject> objectQueue = new Queue<GameObject> ();
						objectQueue.Enqueue (Instantiate (animOHat));
						Destroy (objectQueue.Peek(), objectQueue.Peek().GetComponents<Animator>()[0].GetCurrentAnimatorStateInfo(0).length);
							
						//GameObject.Find ("Indicators(Clone)/OHat").SendMessage ("OnNoteOn");
					} else if (fPos > -1) {
						Queue<GameObject> objectQueue = new Queue<GameObject> ();
						objectQueue.Enqueue (Instantiate (animSnare));
						Destroy (objectQueue.Peek(), objectQueue.Peek().GetComponents<Animator>()[0].GetCurrentAnimatorStateInfo(0).length);

						//GameObject.Find ("Indicators(Clone)/Snare").SendMessage ("OnNoteOn");
					} else if (gPos > -1) {
						Queue<GameObject> objectQueue = new Queue<GameObject> ();
						objectQueue.Enqueue (Instantiate (animKick));
						Destroy (objectQueue.Peek(), objectQueue.Peek().GetComponents<Animator>()[0].GetCurrentAnimatorStateInfo(0).length);

						//GameObject.Find ("Indicators(Clone)/Kick").SendMessage ("OnNoteOn");
					} else if (aPos > -1) {
						Queue<GameObject> objectQueue = new Queue<GameObject> ();
						objectQueue.Enqueue (Instantiate (animHat));
						Destroy (objectQueue.Peek(), objectQueue.Peek().GetComponents<Animator>()[0].GetCurrentAnimatorStateInfo(0).length);

						//GameObject.Find ("Indicators(Clone)/Hat").SendMessage ("OnNoteOn");
					} else if (bPos > -1) {
						Queue<GameObject> objectQueue = new Queue<GameObject> ();
						objectQueue.Enqueue (Instantiate (animOHat));
						Destroy (objectQueue.Peek(), objectQueue.Peek().GetComponents<Animator>()[0].GetCurrentAnimatorStateInfo(0).length);

						//GameObject.Find ("Indicators(Clone)/OHat").SendMessage ("OnNoteOn");
					} else {
						Queue<GameObject> objectQueue = new Queue<GameObject> ();
						objectQueue.Enqueue (Instantiate (animSnare));
						Destroy (objectQueue.Peek(), objectQueue.Peek().GetComponents<Animator>()[0].GetCurrentAnimatorStateInfo(0).length);

						//GameObject.Find (count.ToString () + "/Snare").SendMessage ("OnNoteOn");
					}
					//GameObject.Find ("Indicators(Clone)").transform.position = new Vector3(0, count++, 0);
                }
            }
        }
    }

    // OnGUI function (MonoBehaviour).
    void OnGUI ()
    {
        /*if (GUI.Button (new Rect (0, 0, 300, 50), "Reset (startTime = 0)")) {
            ResetAndPlay (0.0f);
        }
        if (GUI.Button (new Rect (0, 50, 300, 50), "Reset (startTime = 1.3)")) {
            ResetAndPlay (1.3f);
        }*/
    }
}