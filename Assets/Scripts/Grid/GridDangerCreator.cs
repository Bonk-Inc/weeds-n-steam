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

    private List<Vector2Int> dangers;

    public void InitatializeSetup() {
        tiles = new Dictionary<Vector2Int, TilePart>();
        dangers = new List<Vector2Int>();

        foreach (var row in generator.Rows)
        {
            foreach (var tile in row.Tiles)
            {
                tiles.Add(tile.Position, new TilePart(tile, tile.GetComponent<TileSafety>()));
            }
        }
    }

    public int GetSafeTile() {
        return tiles.Sum(tile => { return (tile.Value.safety.Safe) ? 1 : 0; });
    }

    public TilePart SetDangerous(float delay = 3) {
        var tile = tiles.ElementAt(Random.Range(0, tiles.Count));
        return SetDangerous(tile.Key, delay);
    }

    public TilePart SetDangerous(Vector2Int position, float delay = 3) {
        var tilePart = tiles[position];
        if(GetSafeTile() <= minSafeArea) return null;

        tilePart.safety.SetTileSafety(false, delay);

        dangers.Add(position);
        return tilePart;
    }

    public TilePart SetSaferous() {
        if(dangers.Count == 0) return null; 
        var danger = dangers[Random.Range(0, dangers.Count)];
        return SetSaferous(danger);
    }

    public TilePart SetSaferous(Vector2Int position) {
        var tilePart = tiles[position];
        tilePart.safety.SetTileSafety(true);

        dangers.Remove(position);
        return tilePart;
    }

    // TODO error checking - return false?
    public bool SetAllSaferous() {
        foreach (var danger in dangers)
        {
            SetSaferous(danger);
        }
        return true;
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