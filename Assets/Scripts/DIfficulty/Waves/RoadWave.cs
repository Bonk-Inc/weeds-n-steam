using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadWave : Wave
{
    [SerializeField]
    private GridGenerator grid;

    [SerializeField]
    private int spacing = 4;

    [SerializeField]
    private float startTime, speed;

    private Coroutine roadCor;

    private Vector2Int currentPosition;
    private List<Vector2Int> road = new List<Vector2Int>();

    public override void StartWave() {
        road.Clear();
        road.Add(new Vector2Int(Random.Range(0, grid.Size.x-1), Random.Range(0, grid.Size.y-1)));
        for (int i = road.Count; i < spacing; i++)
        {
            road.Add(ChooseNextPosition());
        }
        roadCor = StartCoroutine(RoadMover()); 
    }

    public override void UpdateWave() {}

    public override void EndWave() {
        base.EndWave();
        if(roadCor != null) StopCoroutine(roadCor);
    }

    private IEnumerator RoadMover() {

        dangerCreator.SetAllDangerousWithException(road, startTime);
        yield return new WaitForSeconds(startTime+speed);
        
        while(true) {
            
            var nextPosition = ChooseNextPosition();
            dangerCreator.SetSaferous(nextPosition);
            road.Add(nextPosition);

            dangerCreator.SetDangerous(road[0], speed);
            road.RemoveAt(0);
            yield return new WaitForSeconds(speed);
        }
    }

    private Vector2Int ChooseNextPosition() {
        var current = road[road.Count-1];
        var possible = GetAdjacent(current);
        var chosen = possible.ElementAt(Random.Range(0, possible.Count-1));
        return chosen;
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
        return (pos.x >= 0 && pos.x < grid.Size.x && pos.y >= 0 && pos.y < grid.Size.y && !road.Contains(pos));
    }
}
