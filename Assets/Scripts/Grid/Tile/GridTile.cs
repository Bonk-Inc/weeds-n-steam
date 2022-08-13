using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    // TODO Tile Visual
    // TODO Manages Type of tile?

    [SerializeField]
    private TileSafety tileSafety;

    [SerializeField]
    private new SpriteRenderer renderer;

    private Vector2Int position;

    public Vector2Int Position => position;
    public SpriteRenderer Renderer => renderer;

    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }

    public void SetTileSafety(bool safe = true, float delay = 0) {
        tileSafety.SetTileSafety(safe, delay);
    }
}
