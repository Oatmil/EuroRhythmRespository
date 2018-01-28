using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public static HandController m_instance;

    [Header("the joystick")]
    public GameObject Target;
    [Header("The Hand to move there")]
    public GameObject ControlChild;

    Vector3 local_Position_to_GRAB = new Vector3(0.2499998f,1.57f, -0.76f);

    Animator anim;
    void Awake()
    {
        m_instance = this;
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void GRABBING()
    {
        ControlChild.transform.parent = Target.transform;
        ControlChild.transform.localPosition = local_Position_to_GRAB;
    }

    void Let_It_Go()
    {
        ControlChild.transform.parent = transform;
        ControlChild.transform.localPosition = new Vector3(0, 0, -7);
        ControlChild.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void TriggerGrab(string Direction)
    {
        switch (Direction) {
            case "UP":
                GRABBING();
                anim.SetTrigger("Up");
                break;
            case "DOWN":
                GRABBING();
                anim.SetTrigger("Down");
                break;
            case "LEFT":
                GRABBING();
                anim.SetTrigger("Left");
                break;
            case "RIGHT":
                GRABBING();
                anim.SetTrigger("Right");
                break;
            case "MISS":
                Let_It_Go();
                anim.SetTrigger("Miss");
                break;
                }
    }
}
