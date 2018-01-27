using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
	[SerializeField] private RectTransform menuObjects;

	public void PressStart()
	{
		TransitToSongSelect();
	}

	public void PressCredits()
	{
		TransitToCredits();
	}

	void TransitToSongSelect()
	{
		StartCoroutine(MenuTransition());
	}

	void TransitToCredits()
	{
		StartCoroutine(MenuTransition());
	}

	IEnumerator MenuTransition()
	{
		float tempFloat = 0;
		while (tempFloat < 1)
		{
			yield return new WaitForEndOfFrame();
			tempFloat += .5f * Time.deltaTime;
			menuObjects.anchoredPosition = Vector2.Lerp(Vector2.zero, menuObjects.right * -750, tempFloat);
		}			

		//yield return null;
		StopCoroutine(MenuTransition());
	}
}
