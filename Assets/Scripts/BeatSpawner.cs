using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour {

    public static BeatSpawner m_instance;

    public GameObject EndPoint;
    public GameObject StartPoint;

    public GameObject BAR;
    public List<GameObject> BAR_List;

    [Header("The List for the bars on queue")]
    public List<GameObject> Queue_List;

    private void Awake()
    {
        m_instance = this;
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 10; i++)
        {
			GameObject tempObj = GameObject.Instantiate(BAR, transform);
            tempObj.SetActive(false);
            BAR_List.Add(tempObj);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SpawnNote()
    {
        foreach(GameObject obj in BAR_List)
        {
            if(obj.activeSelf == false)
            {
                obj.transform.position = StartPoint.transform.position;
                obj.GetComponent<BeatScript>().f_speed = Vector3.Distance(EndPoint.transform.position, StartPoint.transform.position) / SCORE_Manager.m_instance.f_NoteSpeed;
                obj.GetComponent<BeatScript>().endPoint = EndPoint.transform.position;
                Queue_List.Add(obj);
                obj.SetActive(true);
                break;
            }
        }
    }
}
