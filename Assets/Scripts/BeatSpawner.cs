using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour {

    public static BeatSpawner m_instance;

    public GameObject EndPoint;
    public GameObject StartPoint;

    public List<GameObject> BAR_Object;
    public List<GameObject> BAR_List;

    [Header("The List for the bars on queue")]
    public List<GameObject> Queue_List;

    private void Awake()
    {
        m_instance = this;
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 3; i++)
        {
            foreach (GameObject tempBar in BAR_Object)
            {
                GameObject tempObj = GameObject.Instantiate(tempBar, transform);
                tempObj.SetActive(false);
                BAR_List.Add(tempObj);
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void SpawnNote(int i_point , string Direction) // start from 1 - 9 from top left and across and than down
    {
        for (int i = i_point - 1; i < BAR_List.Capacity ; i+= BAR_Object.Count)
        {
            if (BAR_List[i].activeSelf == false)
            {
                BAR_List[i].transform.position = StartPoint.transform.position;
                BAR_List[i].transform.GetChild(0).transform.localScale = StartPoint.transform.localScale;

                BAR_List[i].GetComponent<BeatScript>().f_speed = Vector3.Distance(EndPoint.transform.localScale, StartPoint.transform.localScale) / SCORE_Manager.m_instance.f_NoteSpeed;
                BAR_List[i].GetComponent<BeatScript>().endPoint = EndPoint;
                BAR_List[i].GetComponent<BeatScript>().s_DirectionTogo = Direction;
                Queue_List.Add(BAR_List[i]);
                BAR_List[i].SetActive(true);
                break;
            }
        }
        
    }
}
