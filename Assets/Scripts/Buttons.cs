using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

	private Data data;

	public Image restartButton;
	public Sprite idleButtonTexture;
	public Sprite pressingButtonTexture;
	public Sprite deadButtonTexture;
	public Sprite winButtonTexture;

	void Start()
	{
		data = FindObjectOfType<Data>();	
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void ResetGame()
	{

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void MainMenu()
	{
		Destroy(data.gameObject);
		SceneManager.LoadScene("MainMenu");
	}
}
