using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateWave : Wave
{
    [SerializeField]
    private GridGenerator grid;

    [SerializeField]
    private float startTime, dangerDelay;

    private Coroutine locateCor;

    private List<Vector2Int> locations = new List<Vector2Int>();

    public override void StartWave() {
        locateCor = StartCoroutine(LocationMover()); 
    }

    public override void UpdateWave() {}

    public override void EndWave() {
        base.EndWave();
        if(locateCor != null) StopCoroutine(locateCor);
    }

    private IEnumerator LocationMover() {
        while(true) {
            CreateNextLocation();
            dangerCreator.SetAllDangerousWithException(locations, startTime);
            yield return new WaitForSeconds(startTime + dangerDelay);

            dangerCreator.SetAllSaferous();
            yield return new WaitForSeconds(2f); // For anims
        }
    }

    private void CreateNextLocation() {
        locations.Clear();
        var center = new Vector2Int(Random.Range(0, grid.Size.x-1), Random.Range(0, grid.Size.y-1));
        locations.Add(center);
        locations.AddRange(GetAround(center));
    }

    private List<Vector2Int> GetAround(Vector2Int pos) {
        var adjacent = new List<Vector2Int>();
        
        //TODO Ugly!!
        var current = new Vector2Int(pos.x-1, pos.y);
        if(CheckValidRoadPiece(current)) adjacent.Add(current);

        current = new Vector2Int(pos.x+1, pos.y);
        if(CheckValidRoadPiece(current)) adjacent.Add(current);

        current = new Vector2Int(pos.x, pos.y-1);
        if(CheckValidRoadPiece(current)) adjacent.Add(current);

        current = new Vector2Int(pos.x, pos.y+1);
        if(CheckValidRoadPiece(current)) adjacent.Add(current);

        current = new Vector2Int(pos.x+1, pos.y+1);
        if(CheckValidRoadPiece(current)) adjacent.Add(current);

        current = new Vector2Int(pos.x-1, pos.y-1);
        if(CheckValidRoadPiece(current)) adjacent.Add(current);

        current = new Vector2Int(pos.x-1, pos.y+1);
        if(CheckValidRoadPiece(current)) adjacent.Add(current);

        current = new Vector2Int(pos.x+1, pos.y-1);
        if(CheckValidRoadPiece(current)) adjacent.Add(current);

        return adjacent;
    }

    private bool CheckValidRoadPiece(Vector2Int pos) {
        return (pos.x >= 0 && pos.x < grid.Size.x && pos.y >= 0 && pos.y < grid.Size.y && !locations.Contains(pos));
    }
}
