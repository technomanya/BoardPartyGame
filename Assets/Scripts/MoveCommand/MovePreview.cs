using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePreview : MonoBehaviour
{
    public List<Transform> previewListForTurn;
    
    [SerializeField]private PlayerBaseController player;

    public void PreviewMovingAi()
    {
        transform.parent = player.transform.parent;
        for (var i = player.moveAmount; i > 0 ; i--)
        {
            var gridTile = transform.parent.GetComponent<GridTileData>();
            if (gridTile && gridTile.closestTileToEnd)
            {
                Move(gridTile.closestTileToEnd,true);
            }
        }
        GameManager.Instance.MoveConfirm();
    }

    public void PreviewMoving(Vector3 direction)
    {
        if ( Physics.Raycast(transform.position, direction, out var hit, 1f)&& hit.collider.CompareTag("Tile"))
        {
            Debug.Log(hit.transform.name);
            if (previewListForTurn.Count == 0)
            {
                Move(hit.transform,true);
            }
            else
            {
                if (player.moveAmount>0 && !previewListForTurn.Contains(hit.transform))
                {
                    Move(hit.transform,true);
                }
                else if(previewListForTurn.Contains(hit.transform))
                {
                    Move(hit.transform,false);
                }
            }
                
        }

    }

    private void Move(Transform hit,bool forward)
    {
        if (forward)
        {
            player.moveAmount--;
            previewListForTurn.Add(hit);
            transform.parent = hit;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            player.moveAmount++;
            previewListForTurn.Remove(previewListForTurn[^1]);
            transform.parent = hit;
            transform.localPosition = Vector3.zero;
        }
    }
}
