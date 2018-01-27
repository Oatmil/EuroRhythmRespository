using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
	[SerializeField] private GameObject Credits;
	[SerializeField] private GameObject Songs;
	[SerializeField] private Transform[] SonglistButtons;
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

	public void UpArrow()
	{
		if (SonglistButtons[SonglistButtons.Length - 1].localPosition.y < 0)
		{
			foreach(Transform t in SonglistButtons)
			{
				t.localPosition += new Vector3(0, 400, 0);
			}
		}
	}

	public void DownArrow()
	{
		if (SonglistButtons[SonglistButtons.Length - 1].localPosition.y > 0)
		{
			foreach(Transform t in SonglistButtons)
			{
				t.localPosition -= new Vector3(0, 400, 0);
			}
		}
	}
}
