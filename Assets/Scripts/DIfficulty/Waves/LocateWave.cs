using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateWave : Wave
{
    [SerializeField]
    private GridGenerator grid;

    [SerializeField]
    private int locationSize = 2;

    [SerializeField]
    private float startTime, dangerDelay;

    private Coroutine locateCor;

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
            yield return new WaitForSeconds(startTime);

            yield return new WaitForSeconds(dangerDelay);
            dangerCreator.SetAllSaferous();
        }
    }

    private Vector2Int ChooseNextPosition() {
        // var current = road[road.Count-1];
        // var possible = GetAdjacent(current);
        // var chosen = possible.ElementAt(Random.Range(0, possible.Count-1));
        // return chosen;
        return Vector2Int.zero;
    }

    private List<Vector2Int> GetAdjacent(Vector2Int pos) {
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

        return adjacent;
    }

    private bool CheckValidRoadPiece(Vector2Int pos) {
        return (pos.x >= 0 && pos.x < grid.Size.x && pos.y >= 0 && pos.y < grid.Size.y );//&& !road.Contains(pos)
    }
}
