using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using LitJson;

public class game_over : MonoBehaviour {

	public Scene scene;
	public string acc_final;

	IEnumerator Requests_go (string c, string toc, string comm) {
		if (c == "GAME_OVER") {
			string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + "gameOver" + "&toc=" + toc + "&comm=" + comm;
			//Debug.Log (url);

			WWW www = new WWW (url);
			yield return www;

			string html = www.text;
			Debug.Log (html);
		} else if (c== "accelerometer_values"){
			string url = "http://akshit.acslab.org/unity/accept.php?c=" + c + "&s=" + "acc_val" + "&toc=" + toc;
			//JSON objects are not being sent to the URL through GET variable. There are double quotes in string. To avoid that we will use post method. 
			WWWForm form = new WWWForm();
			form.AddField ("comm", comm);

			WWW www = new WWW (url, form);
			yield return www;
			Debug.Log (url);
			string html = www.text;
			Debug.Log (html);
		}
	}

	// Use this for initialization
	void Start () {
		acc_final = GameObject.FindGameObjectWithTag("acc").GetComponent<accelerometer_log> ().acc_final;
		string localDate1 = DateTime.Now.ToString();
		StartCoroutine (Requests_go("accelerometer_values", localDate1, acc_final));
		StartCoroutine (Requests_go("GAME_OVER", localDate1, "Game over!"));
	}
}
