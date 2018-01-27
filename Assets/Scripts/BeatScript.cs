using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScript : MonoBehaviour {

    public float f_speed;
    public Vector3 endPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(-1, 0, 0) * f_speed *Time.deltaTime;
        transform.position += movement;

        if (transform.position.x <= endPoint.x)
        {
            gameObject.SetActive(false);
        }
    }
}
