using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System;

[System.Serializable]
public class TileCreated : UnityEvent<GridTile> { }

[System.Serializable]
public class GridGenerator : MonoBehaviour
{
    [SerializeField, Header("Presets")]
    private GridTile tilePreset;
    [SerializeField]
    private Transform holderPreset;

    [SerializeField, Header("Generator Settings")]
    private Vector2Int size;
    [SerializeField]
    private float tileSize = 1;

    [SerializeField, Header("Grid Tiles")]
    private GridRow[] rows;

    [SerializeField]
    private bool recreateAtStart = false;

    [SerializeField]
    public TileCreated OnTileCreated;

    [SerializeField]
    public UnityEvent OnGridCreated;

    private Transform tileHolder;

    public GridRow[] Rows { get => rows; }



    private void Start()
    {
        if (recreateAtStart) CreateGrid();
        else
        {
            OnGridCreated?.Invoke();
        }
    }

    [ExecuteInEditMode, ContextMenu("Create Grid")]
    public GridRow[] CreateGrid()
    {
        return CreateGrid(size.x, size.y);
    }

    public GridRow[] CreateGrid(int xSize, int zSize)
    {
        if (transform.childCount == 0) CreateHolder();
        if (tileHolder == null || tileHolder.childCount > 0) ResetGrid();

        rows = new GridRow[xSize];

        for (int i = 0; i < xSize; i++)
        {
            rows[i] = new GridRow
            {
                Tiles = new GridTile[zSize]
            };

            for (int j = 0; j < zSize; j++)
            {
                int edgeNumber = 0;
                if (i == 0) edgeNumber += GridTile.LEFT_EDGE;
                if (i == xSize - 1) edgeNumber += GridTile.RIGHT_EDGE;
                if (j == 0) edgeNumber += GridTile.BOTTOM_EDGE;
                if (j == zSize - 1) edgeNumber += GridTile.TOP_EDGE;

                CreateTile(i, j, edgeNumber);
            }
        }
#if UNITY_EDITOR
        if (!Application.isPlaying) EditorUtility.SetDirty(gameObject);
#endif
        print("Grid Created.");
        OnGridCreated?.Invoke();
        return rows;
    }

    private void CreateTile(int xPos, int yPos, int edgeNumber)
    {
        var tile = Instantiate(tilePreset, tileHolder);

        // tile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);

        tile.transform.localPosition = new Vector3(xPos * tileSize, yPos * tileSize, 0);
        tile.name = "Tile - " + (xPos + 1) + " " + (yPos + 1);
        tile.SetPosition(new Vector2Int(xPos, yPos), edgeNumber);

        rows[xPos].Tiles[yPos] = tile;

        OnTileCreated?.Invoke(tile);
    }

    [ExecuteInEditMode, ContextMenu("Reset Grid")]
    private void ResetGrid()
    {
        if (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
            tileHolder = null;
        }
        CreateHolder();
    }

    private void CreateHolder()
    {
        var holder = Instantiate(holderPreset, gameObject.transform);
        holder.localPosition = Vector3.zero;

        tileHolder = holder;
    }
}
