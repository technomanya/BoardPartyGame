using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AiPlayerController : PlayerBaseController
{
    void Update()
    {
        if (isTurn )
        {
            if(!isMoving)
            {
                if (moveAmount > 0)
                {
                    selfPreview.PreviewMovingAi();
                }
            }
            else
            {
                // Debug.Log("MovingAi");
                // transform.localPosition = Vector3.MoveTowards(transform.localPosition,Vector3.zero, 0.1f);
                // if (transform.localPosition == Vector3.zero)
                // {
                //     if(transform.parent == selfPreview.previewListForTurn[^1])
                //     {
                //         Debug.Log("Stopped");
                //         isMoving = false;
                //         GameManager.Instance.ChangeTurn();
                //     }
                //     else
                //     {
                //         var targetTileIndex = selfPreview.previewListForTurn.LastIndexOf(targetTile);
                //         Debug.Log(targetTileIndex);
                //         targetTile = selfPreview.previewListForTurn[targetTileIndex+1];
                //         transform.parent = targetTile;
                //     }
                // }
            }
        }
        
       

        
    }
    
   

    public override void OnTurnStart()
    {
        if (specialDiceCoolDown == 0)
        {
            UpdateCurrentDice(GameManager.Instance.specialDiceObj.GetComponent<DiceBehaviour>());
            GameManager.Instance.specialDiceObj.SetActive(true);
        }
        else 
        {
            UpdateCurrentDice(GameManager.Instance.basicDiceObj.GetComponent<DiceBehaviour>());
            GameManager.Instance.specialDiceObj.SetActive(true);
        }

        isTurn = true;
        
        Debug.Log(currentDice);
        currentDice.RollDice();
    }
    
}
