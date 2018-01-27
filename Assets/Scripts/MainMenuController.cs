using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
	[SerializeField] private GameObject Credits;
	[SerializeField] private GameObject Songs;
	[SerializeField] private Transform SongList;
	[SerializeField] private Animator Anim;

	[SerializeField] private Transform[] MenuButtons;
	[SerializeField] private Transform[] SongButtons;
	[SerializeField] private Transform BackButton;
	[SerializeField] private Transform SelectImage;

	public int buttonIndex = 0;
	public Button currentButton;

	public float AnimSpeed;
	float highestPoint;
	public Vector3 nextMovePosition;
	bool isRunning = false;

	void Start()
	{
		highestPoint = (SongList.localPosition.y + (400f * SongList.childCount - 1));
		nextMovePosition = SongList.localPosition;

		SelectImage.position = MenuButtons[0].position;
		currentButton = MenuButtons[0].GetComponent<Button>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (!Songs.activeSelf && !Credits.activeSelf)
			{
				if (buttonIndex > 0)
				{
					buttonIndex--;
				}
				else
				{
					buttonIndex = MenuButtons.Length - 1;
				}

				MoveSelectBox();
			}
			else if (Songs.activeSelf)
			{
				MoveDown();
			}
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (!Songs.activeSelf && !Credits.activeSelf)
			{
				if (buttonIndex + 1 < MenuButtons.Length)
				{
					buttonIndex++;
				}
				else
				{
					buttonIndex = 0;
				}

				MoveSelectBox();
			}
			else if (Songs.activeSelf)
			{
				MoveUp();
			}
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (Songs.activeSelf || Credits.activeSelf)
			{
				if (!SelectImage.gameObject.activeSelf) SelectImage.gameObject.SetActive(true);

				MoveSelectBox(BackButton);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			SelectImage.gameObject.SetActive(false);
			currentButton.onClick.Invoke();
		}
			
	}

	public void PressStart()
	{
		Songs.SetActive(true);
		Credits.SetActive(false);

		MoveSelectBox(SongButtons[0]);

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

		if (Songs.activeSelf) Songs.SetActive(false);
		if (Credits.activeSelf) Credits.SetActive(false);

		MoveSelectBox(MenuButtons[0]);
	}

	public void PressQuit()
	{
		Application.Quit();
	}

	public void MoveDown()
	{
		if (nextMovePosition.y + 400f <= highestPoint)
		{
			if (buttonIndex + 1 < SongButtons.Length)
			{
				buttonIndex++;
			}
			else
			{
				buttonIndex = 0;
			}

			if (!isRunning)
			{
				StartCoroutine(ShiftSongsUp());
			}
			else
			{
				float tempFloat = highestPoint / SongList.childCount;
				nextMovePosition += (Vector3.up * tempFloat);
				nextMovePosition = new Vector3(0, Mathf.Round(nextMovePosition.y), 0);
			}
		}
	}

	public void MoveUp()
	{
		if (nextMovePosition.y - 400f >= 0f)
		{
			if (buttonIndex > 0)
			{
				buttonIndex--;
			}
			else
			{
				buttonIndex = SongButtons.Length - 1;
			}
				
			if (!isRunning)
			{
				StartCoroutine(ShiftSongsDown());
			}
			else
			{
				float tempFloat = highestPoint / SongList.childCount;
				nextMovePosition -= (Vector3.up * tempFloat);
				nextMovePosition = new Vector3(0, Mathf.Round(nextMovePosition.y), 0);
			}
		}
	}

	IEnumerator ShiftSongsUp()
	{
		isRunning = true;
		float tempFloat = highestPoint / SongList.childCount;
		nextMovePosition += (Vector3.up * tempFloat);
		nextMovePosition = new Vector3(0, Mathf.Round(nextMovePosition.y), 0);

		while (SongList.localPosition.y <= nextMovePosition.y)
		{
			yield return new WaitForSeconds(1 / 60);
			//yield return new WaitForEndOfFrame();
			SongList.localPosition += Vector3.up * AnimSpeed;
		}

		//Snaps the position over just to avoid float errors
		SongList.localPosition = nextMovePosition;
		MoveSelectBox();

		isRunning = false;
		StopCoroutine(ShiftSongsUp());
	}

	IEnumerator ShiftSongsDown()
	{
		isRunning = true;
		float tempFloat = highestPoint / SongList.childCount;
		nextMovePosition -= (Vector3.up * tempFloat);
		nextMovePosition = new Vector3(0, Mathf.Round(nextMovePosition.y), 0);

		while (SongList.localPosition.y >= nextMovePosition.y)
		{
			yield return new WaitForSeconds(1 / 60);
			SongList.localPosition -= Vector3.up * AnimSpeed;
		}

		//Snaps the position over just to avoid float errors
		SongList.localPosition = nextMovePosition;
		MoveSelectBox();

		isRunning = false;
		StopCoroutine(ShiftSongsUp());
	}

	void MoveSelectBox()
	{
		if (!SelectImage.gameObject.activeSelf) SelectImage.gameObject.SetActive(true);

		if (!Songs.gameObject.activeSelf)
		{
			SelectImage.position = MenuButtons[buttonIndex].position;
			currentButton = MenuButtons[buttonIndex].GetComponent<Button>();
		}
		else
		{
			currentButton = SongButtons[buttonIndex].GetComponent<Button>();
		}
	}

	void MoveSelectBox(Transform _nonMenuButtonTarget)
	{
		if (!SelectImage.gameObject.activeSelf) SelectImage.gameObject.SetActive(true);
		SelectImage.position = _nonMenuButtonTarget.position;
		currentButton = _nonMenuButtonTarget.GetComponent<Button>();
	}
}
