using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	[SerializeField] private Image levelButton;
	[SerializeField] private int levelAmount;
	[SerializeField] private List<Sprite> levelButtons = new List<Sprite>();
	[SerializeField] private string levelName = "main";
	[SerializeField] private Data data;
	[SerializeField] private int levelEasyWidth;
	[SerializeField] private int levelMediumWidth;
	[SerializeField] private int levelHardWidth;
	[SerializeField] private int levelHeight = 16;

	void Start ()
	{
		data.SetMapSize(levelEasyWidth, levelHeight);
	}

	public void StartGame()
	{
		SceneManager.LoadScene(levelName);
	}

	public void ChangeDifficulty()
	{
		switch (data.Level)
		{
			case Data.Difficulty.Easy:
				{
					data.Level = Data.Difficulty.Medium;
					levelButton.sprite = levelButtons[1];
					data.SetMapSize(levelMediumWidth, levelHeight);
				}
				break;
			case Data.Difficulty.Medium:
				{
					data.Level = Data.Difficulty.Hard;
					levelButton.sprite = levelButtons[2];
					data.SetMapSize(levelHardWidth, levelHeight);
				}
				break;
			case Data.Difficulty.Hard:
				{
					data.Level = Data.Difficulty.Easy;
					levelButton.sprite = levelButtons[0];
					data.SetMapSize(levelEasyWidth, levelHeight);
				}
				break;
			default:
				{
					data.Level = Data.Difficulty.Easy;
					levelButton.sprite = levelButtons[0];
					data.SetMapSize(levelEasyWidth, levelHeight);
				}
				break;
		}
	}

	public void Quit()
	{
		Application.Quit();
	}
}
