using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    // Manages Type of tile
    // Tile Safety
    // Tile Visual

    [SerializeField]
    private new SpriteRenderer renderer;

    private Vector2Int position;

    public Vector2Int Position => position;
    public SpriteRenderer Renderer => renderer;

    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }
}
