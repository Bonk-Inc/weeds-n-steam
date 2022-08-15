using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    // TODO Tile Visual
    // TODO Manages Type of tile?

    public const int TOP_EDGE = 1 << 0;
    public const int BOTTOM_EDGE = 1 << 1;
    public const int LEFT_EDGE = 1 << 2;
    public const int RIGHT_EDGE = 1 << 3;


    [SerializeField]
    private TileSafety tileSafety;

    [SerializeField]
    private SpriteRenderer rend;

    [SerializeField]
    private Vector2Int position;

    public Vector2Int Position => position;
    public SpriteRenderer Renderer => rend;

    [SerializeField]
    private GameObject top, bottom, left, right;

    public void SetPosition(Vector2Int pos, int edge)
    {
        position = pos;
        HandleEdgeSprite(edge);
    }

    private void HandleEdgeSprite(int edgeNumber)
    {
        if ((edgeNumber & TOP_EDGE) > 0) top.SetActive(true);
        if ((edgeNumber & BOTTOM_EDGE) > 0) bottom.SetActive(true);
        if ((edgeNumber & LEFT_EDGE) > 0) left.SetActive(true);
        if ((edgeNumber & RIGHT_EDGE) > 0) right.SetActive(true);
    }

    public void SetTileSafety(bool safe = true, float delay = 0)
    {
        tileSafety.SetTileSafety(safe, delay);
    }
}
