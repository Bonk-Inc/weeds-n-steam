using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEdgeCalculator : MonoBehaviour
{
    private Bounds gridBounds;
    private bool boundsStarted;

    [SerializeField]
    private EdgeCollider2D edge;

    public void OnTileCreated(GridTile tile)
    {
        if (boundsStarted)
        {
            gridBounds.Encapsulate(tile.Renderer.bounds);
        }
        else
        {
            gridBounds = tile.Renderer.bounds;
            boundsStarted = true;
        }
        Debug.Log("ass");
    }

    public void CreateBox()
    {
        edge.transform.position = Vector3.zero;
        var max = gridBounds.max;
        var min = gridBounds.min;
        var points = new List<Vector2>(){
            new Vector2(max.x, max.y),
            new Vector2(min.x, max.y),
            new Vector2(min.x, min.y),
            new Vector2(max.x, min.y),
            new Vector2(max.x, max.y)
        };

        edge.SetPoints(points);
    }
}
