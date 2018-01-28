using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class XmlManager : MonoBehaviour
{
	[Header ("")]
	public float A_AudioDelay;

	bool XmlLoaded = false;
	XmlDocument XMLDoc;

	[Header ("the path or name of the particular XML")]
	public string[] xmlpath;

	bool b_Respond = false;

	public bool SpaceBar = false;
	bool UpArrow = false;
	bool DownArrow = false;
	bool LeftArrow = false;
	bool RightArrow = false;

	public GameObject testManualStick;

	public GameObject gameOverScreen;
	public Text ScoreText;
	public Button retryButton;
	public Button mainMenuButton;


	// Use this for initialization
	void Start ()
	{
		LoadXML (0);
		StartCoroutine (PRINTXML_NOTES (0));
		Debug.Log ("finish");

		gameOverScreen = GameObject.FindGameObjectWithTag ("EndScreen");
		gameOverScreen.SetActive (false);
		ScoreText = gameOverScreen.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		retryButton = gameOverScreen.transform.GetChild (1).GetComponent<Button> ();
		mainMenuButton = gameOverScreen.transform.GetChild (2).GetComponent < Button> ();

	}

	private void Update ()
	{
		if (b_Respond) {
			if (Input.GetKeyDown (KeyCode.Space))
				SpaceBar = true;
			if (Input.GetKeyDown (KeyCode.UpArrow))
				UpArrow = true;
			if (Input.GetKeyDown (KeyCode.DownArrow))
				DownArrow = true;
			if (Input.GetKeyDown (KeyCode.LeftArrow))
				LeftArrow = true;
			if (Input.GetKeyDown (KeyCode.RightArrow))
				RightArrow = true;
		}
	}

	IEnumerator WRITEXML (int i_level)
	{
		b_Respond = true;
		bool b_stop = false;
		float f_Timer = 0;
		AudioManager.m_instace.Select_Song (i_level);
		yield return new WaitUntil (() => (SpaceBar == true));

		yield return new WaitForEndOfFrame ();
		SpaceBar = false;

		Debug.Log ("Start");
		AudioManager.m_instace.PlaySong ();
		b_stop = false;

		yield return new WaitForEndOfFrame ();
		do {
			f_Timer += Time.deltaTime;
			if (UpArrow) {
				testManualStick.transform.position += new Vector3 (0, 1, 0);
				UpArrow = false;
				AddNewNote ("UP", f_Timer.ToString ());
			}
			if (DownArrow) {
				testManualStick.transform.position += new Vector3 (0, -1, 0);
				DownArrow = false;
				AddNewNote ("DOWN", f_Timer.ToString ());
			}
			if (LeftArrow) {
				testManualStick.transform.position += new Vector3 (-1, 0, 0);
				LeftArrow = false;
				AddNewNote ("LEFT", f_Timer.ToString ());
			}
			if (RightArrow) {
				testManualStick.transform.position += new Vector3 (1, 0, 0);
				RightArrow = false;
				AddNewNote ("RIGHT", f_Timer.ToString ());
			}

			if (SpaceBar) {
				SpaceBar = false;
				b_stop = true;
			}
			yield return new WaitForEndOfFrame ();
		} while (!b_stop);

		Debug.Log ("Save");
		SaveXML (i_level);
		yield return null;
	}

	IEnumerator SpawnNotes (XmlNodeList nodeList)
	{
		bool b_stop = false;
		float f_Timer = 0 + SCORE_Manager.m_instance.f_NoteSpeed + A_AudioDelay;
		int i = 0;
		do {
			f_Timer += Time.deltaTime;
			if (nodeList [i] != null) {
				if (float.Parse (nodeList [i].SelectSingleNode ("time").InnerText) <= f_Timer) {
					// Debug.Log(nodeList[i].SelectSingleNode("Direction").InnerText);
					switch (nodeList [i].SelectSingleNode ("Direction").InnerText) {
					case "UP":
						BeatSpawner.m_instance.SpawnNote (int.Parse (nodeList [i].SelectSingleNode ("stick").InnerText), "UP");
						break;
					case "DOWN":
						BeatSpawner.m_instance.SpawnNote (int.Parse (nodeList [i].SelectSingleNode ("stick").InnerText), "DOWN");
						break;
					case "RIGHT":
						BeatSpawner.m_instance.SpawnNote (int.Parse (nodeList [i].SelectSingleNode ("stick").InnerText), "RIGHT");
						break;
					case "LEFT":
						BeatSpawner.m_instance.SpawnNote (int.Parse (nodeList [i].SelectSingleNode ("stick").InnerText), "LEFT");
						break;
					case "END":
						b_stop = true;
						break;
					}

					i++;
				}
			}
			yield return new WaitForEndOfFrame ();
		} while (!b_stop);
		yield return null;
	}

	IEnumerator PRINTXML_NOTES (int i_level)
	{
		b_Respond = true;
		bool b_stop = false;
		float f_Timer = 0;
		AudioManager.m_instace.Select_Song (i_level);
		yield return new WaitUntil (() => (SpaceBar == true));
		SpaceBar = false;

		XmlNodeList nodeList = XMLDoc.SelectSingleNode ("XML").ChildNodes;
		yield return new WaitForEndOfFrame ();
		Debug.Log ("Start");
		AudioManager.m_instace.PlaySong ();
		b_stop = false;
		int i = 0;
		yield return new WaitForEndOfFrame ();
		StartCoroutine (SpawnNotes (nodeList));
		do {
			f_Timer += Time.deltaTime;
			if (nodeList [i] != null) {
				if (float.Parse (nodeList [i].SelectSingleNode ("time").InnerText) <= f_Timer) {
					// Debug.Log(nodeList[i].SelectSingleNode("Direction").InnerText);
					switch (nodeList [i].SelectSingleNode ("Direction").InnerText) {
					case "UP":
						testManualStick.transform.position += new Vector3 (0, 1.7f, 0);
						break;
					case "DOWN":
						testManualStick.transform.position += new Vector3 (0, -1.7f, 0);
						break;
					case "RIGHT":
						testManualStick.transform.position += new Vector3 (1.3f, 0, 0);
						testManualStick.transform.localEulerAngles += new Vector3 (0, 0, -13);
						break;
					case "LEFT":
						testManualStick.transform.position += new Vector3 (-1.3f, 0, 0);
						testManualStick.transform.localEulerAngles += new Vector3 (0, 0, 13);
						break;
					case "END":
						ScoreText.text = "Highest combo: " + GetComponent<SCORE_Manager> ().HighScore;
						gameOverScreen.SetActive (true);
						b_stop = true;
						break;
					}

					i++;
				}
			}
			yield return new WaitForEndOfFrame ();
		} while (!b_stop);

		yield return null;
	}

	IEnumerator DelayTest ()
	{
		Debug.Log ("Start");
		bool bbbb = true;
		bool baba = true;
		float temptemp = 0;
		do {
			yield return new WaitUntil (() => (SpaceBar == true));
			Debug.Log ("Time");
			yield return new WaitForEndOfFrame ();
			SpaceBar = false;
			baba = true;
			BeatSpawner.m_instance.SpawnNote (5, "UP");
			temptemp = 0;
			do {
				temptemp += Time.deltaTime;

				if (SpaceBar) {
					SpaceBar = false;
					baba = false;
				}
				if (BeatSpawner.m_instance.Queue_List [0].transform.localScale.x <= 1) {
					SpaceBar = false;
					baba = false;
				}
				yield return new WaitForEndOfFrame ();
			} while (baba);

			Debug.Log (temptemp);

		} while (bbbb);
	}

	void LoadXML (int i) //Load the XML out to be used and access
	{
		if (!XmlLoaded) {
			XMLDoc = new XmlDocument (); // create new xml document;
			XMLDoc.Load (Path.Combine (Application.dataPath, xmlpath [i])); // replace online file with local file
			XmlLoaded = true;
		}
	}

	void SaveXML (int i) // save any changes done to it.
	{
		XMLDoc.Save (Path.Combine (Application.dataPath, xmlpath [i]));
	}

	void AddNewNote (string s_Direction, string s_time) // Add node under the main node
	{
		XmlElement notes = XMLDoc.CreateElement ("note");

		XmlElement time = XMLDoc.CreateElement ("time");
		XmlElement DIRECTION = XMLDoc.CreateElement ("Direction");

		time.InnerText = s_time;
		DIRECTION.InnerText = s_Direction;

		notes.AppendChild (time);
		notes.AppendChild (DIRECTION);

		XMLDoc.DocumentElement.AppendChild (notes);

	}
    
}
