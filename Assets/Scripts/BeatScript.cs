using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScript : MonoBehaviour {

    public float f_speed;
    public Vector3 endPoint;
	// Use this for initialization
	void Start () {
		
	}

    private void OnGUI()
    {
        if (transform.position.x > endPoint.x - SCORE_Manager.m_instance.f_HitSpacing && BeatSpawner.m_instance.Queue_List[0] == this.gameObject)
        {
            if (Event.current.Equals(Event.KeyboardEvent("space")))
            {
                if (Vector3.Distance(endPoint, transform.position) <= SCORE_Manager.m_instance.f_HitSpacing)
                {
                    Debug.Log(Vector3.Distance(endPoint, transform.position));
                    SCORE_Manager.m_instance.SCORE();
                    BeatSpawner.m_instance.Queue_List.RemoveAt(0);
                    gameObject.SetActive(false);
                }
            }
        }
        
    }

	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(-1, 0, 0) * f_speed *Time.deltaTime;
        transform.position += movement;

        
        if (transform.position.x < endPoint.x - SCORE_Manager.m_instance.f_HitSpacing )
        {
            SCORE_Manager.m_instance.TOTALMISS();
            BeatSpawner.m_instance.Queue_List.RemoveAt(0);
            gameObject.SetActive(false);
        }
    }
}
