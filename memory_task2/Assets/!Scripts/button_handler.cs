using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System;
using System.Globalization;

public class button_handler : MonoBehaviour, IVirtualButtonEventHandler {

	//declaring game object
	GameObject vbutton;
	//declaring gamer's ID
	//transferring this task to timer.cs
//	int subject;

	timer timer;

	IEnumerator Requests_accept (string c, string toc, string comm) {
		string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + timer.subject + "&toc=" + (timer.totalTime - timer.timeLeft) + "&comm=" + comm;
		Debug.Log (url);

		WWW www = new WWW (url);
		yield return www;

		string html = www.text;
		Debug.Log (html);
	}

	void Start () {
		timer = GetComponentInParent<timer> ();
		//setting up ID of the person
		//task transfer to timer.cs
//		subject = Convert.ToInt16(UnityEngine.Random.value * 10000);
		//initializing game object vbutton to the game object this component (script) is attached to
		vbutton = gameObject;
		//registering event handler with the virtualbuttonbehavior component of VirtualButton
		vbutton.GetComponentInChildren<VirtualButtonBehaviour> ().RegisterEventHandler (this);
		//sending a marker to server that memory task has started

		//incorporating this task to timer.cs to avoid multiple entries for 'starting up!'
//		string localDate1 = DateTime.Now.ToString();
//		StartCoroutine (Requests_accept("GAME_INITIALIZED", localDate1, "starting up!"));
	}
		
	public void OnButtonPressed (VirtualButtonAbstractBehaviour vb) {
		Debug.Log ("Button Down!");
	}

	public void OnButtonReleased (VirtualButtonAbstractBehaviour vb) {
		Debug.Log ("Button Released!");
		if (vbutton.transform.childCount == 2) {
			GameObject frame = vbutton.transform.FindChild ("frame").gameObject;
			GameObject button = vbutton.transform.FindChild ("VirtualButton").gameObject;
			GameObject captured = frame.transform.FindChild ("3_M4A1AssaultRifle").gameObject;
			string capture = captured.GetComponent<MeshRenderer> ().materials [0].ToString ();
			capture = capture.Replace(" (Instance) (UnityEngine.Material)","");
			string localDate = DateTime.Now.ToString();
			Debug.Log ("You have captured: " + capture + " at:" + localDate);
			frame.SetActive (false);
			button.GetComponent<VirtualButtonBehaviour> ().VirtualButton.SetEnabled (false);
			//start the procedure for website reporting.
			StartCoroutine (Requests_accept(capture, localDate, "captured"));
		}
	}
}
