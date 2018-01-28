using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScript : MonoBehaviour {

    public float f_speed;
    public GameObject endPoint;
    public string s_DirectionTogo;

	// Use this for initialization
	void Start () {
		
	}

    private void OnGUI()
    {
        if (transform.GetChild(0).transform.localScale.x > endPoint.transform.localScale.x - SCORE_Manager.m_instance.f_HitSpacing && BeatSpawner.m_instance.Queue_List[0] == this.gameObject)
        {
            if (Event.current.Equals(Event.KeyboardEvent("space")))
            {
            if (Mathf.Abs(transform.GetChild(0).transform.localScale.x - endPoint.transform.localScale.x) <= SCORE_Manager.m_instance.f_HitSpacing)
            {
            //    Debug.Log(Mathf.Abs(transform.localScale.x - endPoint.transform.localScale.x));
                SCORE_Manager.m_instance.SCORE();
                BeatSpawner.m_instance.Queue_List.RemoveAt(0);
                    HandController.m_instance.TriggerGrab(s_DirectionTogo);
                    gameObject.SetActive(false);
            }
            }
        }
    }

	// Update is called once per frame
	void Update () {
        Vector3 scalling = new Vector3(-1, -1, 0) * f_speed *Time.deltaTime;
        transform.GetChild(0).transform.localScale += scalling;

        
        if (transform.GetChild(0).transform.localScale.x < endPoint.transform.localScale.x - SCORE_Manager.m_instance.f_HitSpacing )
        {
      //      Debug.Log(transform.GetChild(0).transform.localScale.x - endPoint.transform.localScale.x);
            SCORE_Manager.m_instance.TOTALMISS();
            BeatSpawner.m_instance.Queue_List.RemoveAt(0);
            HandController.m_instance.TriggerGrab("MISS");
            gameObject.SetActive(false);

        }
    }
}
