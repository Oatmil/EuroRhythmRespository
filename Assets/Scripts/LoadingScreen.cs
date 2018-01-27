using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour 
{
	public static LoadingScreen _instance {get; private set;}

	[SerializeField] private GameObject loadScreenObj;

	Camera currentCam;
	Scene nextScene;
	Scene previousScene;

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		currentCam = Camera.main;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			LoadLevel("BaseLvl");
		}
	}

	public void LoadLevel(string _levelName)
	{
		StartCoroutine(LoadLevelAdditive(_levelName));
	}

	IEnumerator LoadLevelAdditive(string _levelName)
	{
		//Bring up Loading Screen
		loadScreenObj.SetActive(true);

		//Load next Level Additively
		SceneManager.LoadScene(_levelName, LoadSceneMode.Additive);
		//Set reference to the next level & previous level
		nextScene = SceneManager.GetSceneByName(_levelName);
		previousScene = SceneManager.GetActiveScene();

		yield return new WaitForSeconds(1.0f);

		//SceneManager.MergeScenes(SceneManager.GetActiveScene(), nextScene);
		//currentCam = Camera.main;
		//GetComponent<Canvas>().worldCamera = currentCam;

		SceneManager.UnloadSceneAsync(previousScene);
		//previousScene = null;

		loadScreenObj.SetActive(false);

		StopCoroutine(LoadLevelAdditive(_levelName));
	}
}
