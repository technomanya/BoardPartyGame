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
                if (transform.parent == selfPreview.previewListForTurn[^1])
                {
                    isMoving = false;
                    OnTurnEnd();
                }
            }
        }

    }
    
   

    public override void OnTurnStart()
    {
        selfPreview.previewListForTurn.Clear();
        selfPreview.previewListForTurn = new List<Transform>();
        diceRolled = false;
        if (specialDiceCoolDown == 0)
        {
            UpdateCurrentDice(GameManager.Instance.specialDiceObj.GetComponent<DiceBehaviour>());
            GameManager.Instance.specialDiceObj.SetActive(true);
        }
        else 
        {
            UpdateCurrentDice(GameManager.Instance.basicDiceObj.GetComponent<DiceBehaviour>());
            GameManager.Instance.specialDiceObj.SetActive(false);
        }

        isTurn = true;
        UIManager.Instance.UpdateCurrentPlayerNameText(playerName);
        currentDice.RollDice();
    }
    
}
