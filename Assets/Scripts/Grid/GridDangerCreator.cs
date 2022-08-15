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
        var possibleTiles = tiles.Where(tile => tile.Value.safety.Safe).ToList();
        if(possibleTiles.Count == 0) return null;
        var tile = possibleTiles.ElementAt(Random.Range(0, possibleTiles.Count-1)); //TODO Should this be -1?
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

    public bool SetAllSaferous() {
        var tilesToSafe = tiles.Where(tile => !tile.Value.safety.Safe).ToArray();
        foreach (var tile in tilesToSafe)
        {
            tile.Value.safety.SetTileSafety(true);
        }
        dangers.Clear();
        return true;
    }

    public bool SetAllDangerous(float delay) {
        var tilesToEndanger = tiles.Where(tile => tile.Value.safety.Safe).ToArray();
        return SetAllDangerous(tilesToEndanger, delay);
    }

    public bool SetAllDangerousWithException(List<Vector2Int> exceptions, float delay) {
        var tilesToEndanger = tiles.Where(tile => !exceptions.Contains(tile.Key)).ToArray();
        return SetAllDangerous(tilesToEndanger, delay);
    }

    public bool SetAllDangerous(KeyValuePair<Vector2Int, TilePart>[] tilesToEndanger, float delay) {
        dangers.Clear();
        foreach (var tile in tilesToEndanger)
        {
            tile.Value.safety.SetTileSafety(false, delay);
            dangers.Add(tile.Key);
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