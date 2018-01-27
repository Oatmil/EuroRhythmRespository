using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager m_instace;
    
    [Header ("List of songs to have in the game also use as the levels")]
    public List<AudioClip> Audio_Clips;
    
      
    [Space]
    AudioSource a_source;

    void Awake()
    {
        m_instace = this;
        a_source = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Select_Song(int i_select)
    {
        a_source.clip = Audio_Clips[i_select];
    }

    public void PlaySong()
    {
        a_source.Play();
    }
    
}
