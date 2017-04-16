using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System;
using System.Globalization;
using System.IO;

public class button_handler : MonoBehaviour, IVirtualButtonEventHandler {

	Text text;
	GameObject vbutton;
	timer timer;

	IEnumerator Requests_accept (string c, string toc, string comm) {
		float time1 = (timer.totalTime - timer.timeLeft);
		string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + timer.subject + "&toc=" + toc + " " + time1 + "&comm=" + comm;
		string aa = "\n" + c + " , " + timer.subject + " , " + toc + " , " + time1 + " , " + comm + "\n";
		if (Application.platform != RuntimePlatform.Android) {
			using(System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\akshi\Desktop\logs_memory.txt", true)) { //we can make the path generic to Application.persistentDataPath + "\logs.txt"
				file.WriteLine(aa);
			}
		} else {
			using(System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + @"/logs_memory.txt", true)) {
				file.WriteLine(aa);
			}
		}
		Debug.Log (url);
		WWW www = new WWW (url);
		yield return www;
		string html = www.text;
		Debug.Log (html);
		text.text = "";
	}

	void Start () {
		text = GameObject.FindGameObjectWithTag("capture").GetComponent<Text> () as Text;
		timer = GetComponentInParent<timer> ();
		vbutton = gameObject; 		//initializing game object vbutton to the game object this component (script) is attached to
		vbutton.GetComponentInChildren<VirtualButtonBehaviour> ().RegisterEventHandler (this); //registering event handler with the virtualbuttonbehavior component of VirtualButton
	}
		
	public void OnButtonPressed (VirtualButtonAbstractBehaviour vb) {
		Debug.Log ("Button Down!");
		text.text = "<color=lime>CAPTURED!</color>";
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
			StartCoroutine (Requests_accept(capture, localDate, "captured"));
		}
	}
}
