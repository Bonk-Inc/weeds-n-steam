using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridDangerCreator : MonoBehaviour
{
    [SerializeField]
    private GridGenerator generator;

    [SerializeField]
    private int minSafeArea = 1;

    private Dictionary<Vector2Int, TilePart> tiles;

    public void InitatializeSetup() {
        tiles = new Dictionary<Vector2Int, TilePart>();
        foreach (var row in generator.Rows)
        {
            foreach (var tile in row.Tiles)
            {
                tiles.Add(tile.Position, new TilePart(tile, tile.GetComponent<TileSafety>()));
            }
        }
        SetDangerous();
    }

    public TilePart SetDangerous(float delay = 3) {
        var tilePart = tiles.ElementAt(Random.Range(0, tiles.Count)).Value;
        tilePart.safety.SetTileSafety(false, delay);
        return tilePart;
    }
}

public class TilePart {
    
    public GridTile tile;
    public TileSafety safety;

    public TilePart(GridTile gridTile, TileSafety tileSafety) {
        tile = gridTile;
        safety = tileSafety;
    }

}