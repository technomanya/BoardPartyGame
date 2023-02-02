using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiController : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button moveConfirmButton;
    [SerializeField] private Button endTurnButton;
    void Start()
    {
        moveConfirmButton.onClick.AddListener(MoveConfirmOnClick);
        endTurnButton.onClick.AddListener(EndTurnOnClick);
        pauseButton.onClick.AddListener(PauseButtonOnClick);
    }

    void MoveConfirmOnClick()
    {
        GameManager.Instance.MoveConfirm();
        //GameManager.Instance.currentPlayerTurn.MoveConfirm();
    }

    void EndTurnOnClick()
    {
        GameManager.Instance.ChangeTurn();
    }

    void PauseButtonOnClick()
    {
        UIManager.Instance.PauseGame();
    }
}
