using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class timer : MonoBehaviour {

	public int subject;
	public float totalTime = 1800.0f;
	public float timeLeft;
	public Text text;

	IEnumerator Requests_accept (string c, string toc, string comm) {
		float time1 = totalTime - timeLeft;
		string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + subject + "&toc=" + toc + " " + (time1) + "&comm=" + comm;
		string aa = "\n" + c + " , " + subject + " , " + toc + " , " + (time1) + " , "+ comm + "\n";
		if (Application.platform != RuntimePlatform.Android) {
			using(System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\akshi\Desktop\logs_memory.txt", true)) { 
				file.WriteLine(aa);
			}
		} 
		else {
			using(System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + @"/logs_memory.txt", true)) {
				file.WriteLine(aa);
			} 
		}
		Debug.Log (url);
		WWW www = new WWW (url);
		yield return www;
		string html = www.text;
		Debug.Log (html);
	}

	void Start () {
		subject = GameObject.FindGameObjectWithTag ("acc").GetComponent<accelerometer_log> ().subject;
		timeLeft = totalTime;
		string localDate1 = DateTime.Now.ToString();
		StartCoroutine (Requests_accept("GAME_INITIALIZED", localDate1, "starting up!"));
	}

	void Update () {
		timeLeft -= Time.deltaTime;	
		float time1 = (int)Math.Ceiling(totalTime - timeLeft);
		text.text = "Timer:" + time1.ToString ();
		if (timeLeft < 0.0f || Input.GetMouseButtonDown(0)) { //in case I interrupt it through mouse, or the time runs out!
			Debug.Log ("Game over!");
			SceneManager.LoadSceneAsync ("!Scenes/2");
			Debug.Log ("Scene changed");
		}	
	}
}
