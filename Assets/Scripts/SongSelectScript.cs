using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongSelectScript : MonoBehaviour 
{
	public Text songTitleBubble;

	void LateUpdate()
	{
		songTitleBubble.transform.position = transform.position + new Vector3(Mathf.PerlinNoise(transform.position.x, transform.position.y), 0, 0);
		//Mathf.PerlinNoise
	}
}
