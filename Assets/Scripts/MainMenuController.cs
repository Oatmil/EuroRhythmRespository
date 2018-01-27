using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
	[SerializeField] private GameObject Credits;
	[SerializeField] private GameObject Songs;
	[SerializeField] private Animator Anim;

	public void PressStart()
	{
		Songs.SetActive(true);
		Credits.SetActive(false);
		
		Anim.Play("PressedButton");
	}

	public void PressCredits()
	{
		Songs.SetActive(false);
		Credits.SetActive(true);

		Anim.Play("PressedButton");
	}

	public void PressBack()
	{
		Anim.Play("PressedBack");
	}
}
