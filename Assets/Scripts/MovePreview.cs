using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePreview : MonoBehaviour
{
    public List<Transform> previewListForTurn;
    public int moveAmount;
    
    [SerializeField]private PlayerBaseController player;

    public void PreviewMovingAi()
    {
        for (int i = player.moveAmount; i > 0 ; i--)
        {
            var gridTile = transform.parent.GetComponent<GridTileData>();
            if (gridTile && gridTile.possibleDirections.Length > 0)
            {
                var randomDirectionId = Random.Range(0, gridTile.possibleDirections.Length);
                transform.LookAt(gridTile.possibleDirections[randomDirectionId]);
                var transformLocalRotation = transform.localRotation;
                transformLocalRotation.eulerAngles =
                    new Vector3(0,transformLocalRotation.eulerAngles.y, 0);
                transform.localRotation = transformLocalRotation;
            
                
            }
            Debug.Log("MovedAi "+ player.moveAmount);
            PreviewMoving(transform.forward);
        }
        GameManager.Instance.MoveConfirm();
    }

    public void PreviewMoving(Vector3 direction)
    {
        RaycastHit hit;
        bool isHit = false;
        isHit = Physics.Raycast(transform.position, direction, out hit, 1f);
        //Debug.Log(isHit);
        if (isHit && hit.collider.CompareTag("Tile"))
        {
            if (previewListForTurn.Count == 0)
            {
                player.moveAmount--;
                previewListForTurn.Add(hit.transform);
                transform.parent = hit.transform;
                transform.localPosition = Vector3.zero;
            }
            else
            {
                if (player.moveAmount>0 && !previewListForTurn.Contains(hit.transform))
                {
                    player.moveAmount--;
                    previewListForTurn.Add(hit.transform);
                    transform.parent = hit.transform;
                    transform.localPosition = Vector3.zero;
                }
                else if(previewListForTurn.Contains(hit.transform))
                {
                    player.moveAmount++;
                    previewListForTurn.Remove(previewListForTurn[^1]);
                    transform.parent = hit.transform;
                    transform.localPosition = Vector3.zero;
                }
            }
                
        }
        
    }
}
