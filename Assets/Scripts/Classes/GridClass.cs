using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridClass : MonoBehaviour {

	private static bool finished, win;
	private static List<TileClass> hitRecord = new List<TileClass>();
	private static RaycastHit hit;
	private static int width;
	private static int height;

	public Data data;
	public Buttons buttons;

	public static TileClass[,] tiles;

	public static int Width { get { return width; } set { width = value; }}
	public static int Height { get { return height; } set { height = value; }}
	public static List<TileClass> HitRecord { get { return hitRecord; } }

	private void Awake()
	{
		data = FindObjectOfType<Data>();
		width = data.MapWidth;
		height = data.MapHeight;
		tiles = new TileClass[width, height];
	}

	void Start()
	{
		finished = false;
		win = false;
		buttons = FindObjectOfType<Buttons>();
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Changes tile and restart button sprite when mouse pressed
		if (Input.GetMouseButton(0))
		{
			if (isFinished()) return;

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.GetComponent<TileClass>())
				{
					TileClass tile = hit.transform.GetComponent<TileClass>();

					if (!tile.isCovered() || tile.Flag) return;

					hitRecord.Add(tile);

					tile.SprRen.sprite = tile.ClickedTileTexture;
					buttons.restartButton.sprite = buttons.pressingButtonTexture;
				}
			}
		}

		// Changes tile and restart button sprite when mouse released
		if (Input.GetMouseButtonUp(0))
		{
			if (hitRecord.Count > 0) hitRecord[hitRecord.Count - 1].UncoverTile();
			hitRecord.Clear();

			if (isFinished())
			{
				buttons.restartButton.sprite = buttons.deadButtonTexture;
			}
			else
			{
				buttons.restartButton.sprite = buttons.idleButtonTexture;
			}
		}

		// Switch tile flag on/off
		if (Input.GetMouseButtonDown(1))
		{
			if (isFinished()) return;

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.GetComponent<TileClass>())
				{
					TileClass tile = hit.transform.GetComponent<TileClass>();

					if (!tile.isCovered()) return;

					tile.Flag = !tile.Flag;
					if (tile.Flag)
					{
						tile.ChangeTexture(0);
					}
					else
					{
						tile.ChangeTexture(9);
					}
				}
			}
		}

		if (win) buttons.restartButton.sprite = buttons.winButtonTexture;
	}

	// Uncovers all mines on the field
	public static void UncoverMines()
	{
		foreach (TileClass tile in tiles)
		{
			if (tile.Mine)
			{
				tile.Flag = false;
				tile.ChangeTexture(0);
				finished = true;
			}
		}
	}

	// Checks if mine at location [x, y]
	public static bool MineAt(int x, int y)
	{
		if (x >= 0 && y >= 0 && x < width && y < height) return tiles[x, y].Mine;
		return false;
	}

	// Counts surrounding mines
	public static int AdjecentMines(int x, int y)
	{
		int count = 0;

		if (MineAt(x, y + 1)) ++count; // top
		if (MineAt(x + 1, y + 1)) ++count; // top right
		if (MineAt(x + 1, y)) ++count; // right
		if (MineAt(x + 1, y - 1)) ++count; // bot right
		if (MineAt(x, y - 1)) ++count; // bot
		if (MineAt(x - 1, y - 1)) ++count; // bot left
		if (MineAt(x - 1, y)) ++count; // left
		if (MineAt(x - 1, y + 1)) ++count; // top left

		return count;
	}

	// Floodfills all empty tiles
	public static void FloodFill(int x, int y, bool[,] visited)
	{
		if (x >= 0 && y >= 0 && x < width && y < height)
		{
			if (visited[x, y]) return;

			tiles[x, y].ChangeTexture(AdjecentMines(x, y));

			if (AdjecentMines(x, y) > 0) return;

			visited[x, y] = true;

			FloodFill(x, y + 1, visited);
			FloodFill(x + 1, y + 1, visited);
			FloodFill(x + 1, y, visited);
			FloodFill(x + 1, y - 1, visited);
			FloodFill(x, y - 1, visited);
			FloodFill(x - 1, y - 1, visited);
			FloodFill(x - 1, y, visited);
			FloodFill(x - 1, y + 1, visited);
		}
	}

	// Checks if "game over" or "win"
	public static bool isFinished()
	{
		if (finished) return true;

		foreach (TileClass tile in tiles)
		{
			if (tile.isCovered() && !tile.Mine) return false;
		}

		win = true;

		return true;
	}
}
