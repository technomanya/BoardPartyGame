using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileData : MonoBehaviour
{
    public Transform closestTileToEnd;

    private void Start()
    {
        var aiPossibleDirections = new List<Transform>();
        aiPossibleDirections.Add(FindNeighbours(transform.forward));
        aiPossibleDirections.Add(FindNeighbours(transform.right));
        aiPossibleDirections.Add(FindNeighbours(-transform.forward));
        aiPossibleDirections.Add(FindNeighbours(-transform.right));
        closestTileToEnd = aiPossibleDirections[0];
        var minDistance = Mathf.Infinity;
        var gm = GameManager.Instance;
        foreach (var aiDirection in aiPossibleDirections)
        {
            if (Vector3.Distance(gm.endTile.position, aiDirection.position) < minDistance)
            {
                minDistance = Vector3.Distance(gm.endTile.position, aiDirection.position);
                closestTileToEnd = aiDirection;
            }
        }
    }

    private Transform FindNeighbours(Vector3 direction)
    {
        RaycastHit hit;
        var resultTransform = transform;
        
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
            resultTransform = hit.transform;
        
        return resultTransform;
    }
}
