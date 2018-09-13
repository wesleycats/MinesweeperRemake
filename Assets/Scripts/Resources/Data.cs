using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

#pragma warning disable 0414 // variable assigned but not used.
	private int mapWidth;
#pragma warning disable 0414 // variable assigned but not used.
	private int mapHeight;

	public enum Difficulty { Easy, Medium, Hard };
	private Difficulty level;

	public int MapWidth { get { return mapWidth; }}
	public int MapHeight { get { return mapHeight; }}
	public Difficulty Level	{ get { return level; }	set { level = value; }}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		level = Difficulty.Easy;	
	}

	public void SetMapSize(int width, int height)
	{
		mapWidth = width;
		mapHeight = height;
	}
}
