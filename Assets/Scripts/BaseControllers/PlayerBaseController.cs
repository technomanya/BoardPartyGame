using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseController : MonoBehaviour
{
    public string playerName;
    public bool isTurn;
    public bool isMoving;
    public bool diceRolled;
    public int specialDiceCoolDownSet;
    public int specialDiceCoolDown;
    public DiceBehaviour currentDice;
    public int moveAmount;
    public Transform targetTile;
    [SerializeField] private GameObject playerUiPanel;

    public MovePreview selfPreview;

    private void Start()
    {
        specialDiceCoolDown = specialDiceCoolDownSet;
        selfPreview = GetComponentInChildren<MovePreview>();
    }


    public void  UpdateCurrentDice(DiceBehaviour _diceBehaviour)
    {
        currentDice = _diceBehaviour;
    }

    public virtual void OnTurnStart()
    {
        selfPreview.previewListForTurn.Clear();
        selfPreview.previewListForTurn = new List<Transform>();
        Debug.Log(playerName);
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
        if (playerUiPanel)
        {
            playerUiPanel.SetActive(true);
        }
    }

    public void OnTurnEnd()
    {
        isTurn = false;
        specialDiceCoolDown = specialDiceCoolDown > 0 ? --specialDiceCoolDown : specialDiceCoolDownSet;
        
        if (playerUiPanel)
        {
            playerUiPanel.SetActive(false);
        }

    }

    public void PlayerReset()
    {
        transform.parent = GameManager.Instance.startTile;
        transform.localPosition = Vector3.zero;
        selfPreview.transform.parent = transform;
        selfPreview.transform.localPosition = Vector3.zero;
    }
}
