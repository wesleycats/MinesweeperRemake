using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMap : MonoBehaviour {

	[SerializeField] private GameObject easyGrid;
	[SerializeField] private GameObject mediumGrid;
	[SerializeField] private GameObject hardGrid;
	[SerializeField] private SpriteRenderer background;
	[SerializeField] private Sprite easyBG;
	[SerializeField] private Sprite mediumBG;
	[SerializeField] private Sprite hardBG;

	private Data data;

	// Use this for initialization
	void Start () {
		data = FindObjectOfType<Data>();
		Instantiate();
	}

	private void Instantiate()
	{
		switch (data.Level)
		{
			case Data.Difficulty.Easy:
				{
					easyGrid.SetActive(true);
					background.sprite = easyBG;
				}
				break;
			case Data.Difficulty.Medium:
				{
					mediumGrid.SetActive(true);
					background.sprite = mediumBG;
				}
				break;
			case Data.Difficulty.Hard:
				{
					hardGrid.SetActive(true);
					background.sprite = hardBG;
				}
				break;
			default:
				{
					easyGrid.SetActive(true);
					background.sprite = easyBG;
				}
				break;
		}
	}
}
