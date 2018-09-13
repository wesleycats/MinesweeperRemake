using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour {

	[SerializeField] private Sprite[] adjacentTextures;
	[SerializeField] private Sprite defaultTexture;
	[SerializeField] private Sprite clickedTileTexture;
	[SerializeField] private Sprite flagTileTexture;
	[SerializeField] private Sprite mineTexture;
	[SerializeField] private Sprite clickedMineTexture;
	[SerializeField] private float bombChance = 0.15f;

	private bool mine;
	private bool clickedMine;
	private bool flag = false;
	private SpriteRenderer sprRen;
	private Data data;
	private InstantiateMap inst;

	public bool Mine { get { return mine; }}
	public bool Flag { get { return flag; } set { flag = value; }}
	public bool ClickedMine { get { return clickedMine; }}
	public SpriteRenderer SprRen { get { return sprRen; }}
	public Sprite ClickedTileTexture { get { return clickedTileTexture; }}

	// Use this for initialization
	void Start()
	{
		mine = Random.value < bombChance;
		sprRen = GetComponent<SpriteRenderer>();
		int x = (int)transform.localPosition.x;
		int y = (int)transform.localPosition.y;
		GridClass.tiles[x, y] = this;
	}

	public void ChangeTexture(int adjacentMines)
	{
		if (flag)
		{
			sprRen.sprite = flagTileTexture;
		}
		else if (adjacentMines > 8)
		{
			sprRen.sprite = defaultTexture;
		}
		else if (mine && !flag)
		{
			sprRen.sprite = mineTexture;

			if (clickedMine) sprRen.sprite = clickedMineTexture;
		}
		else
		{
			sprRen.sprite = adjacentTextures[adjacentMines];
		}
	}

	public bool isCovered()
	{
		return sprRen.sprite.name == "TileDefault" || sprRen.sprite.name == "TileFlag" || sprRen.sprite.name == "TilePressed"; 
	}

	private void OnMouseEnter()
	{
		if (GridClass.HitRecord.Count > 0) GridClass.HitRecord[GridClass.HitRecord.Count - 1].sprRen.sprite = defaultTexture;
	}

	public void UncoverTile()
	{
		if (!isCovered() || flag || GridClass.isFinished()) return;

		if (mine)
		{
			clickedMine = true;
			GridClass.UncoverMines();
			ChangeTexture(0);
		}
		else
		{
			int x = (int)transform.localPosition.x;
			int y = (int)transform.localPosition.y;
			ChangeTexture(GridClass.AdjecentMines(x, y));

			GridClass.FloodFill(x, y, new bool[GridClass.Width, GridClass.Height]);
		}
	}
}
