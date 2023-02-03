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
        UIManager.Instance.PlayButtonSound();
        GameManager.Instance.MoveConfirm();
        moveConfirmButton.gameObject.SetActive(false);
        //GameManager.Instance.currentPlayerTurn.MoveConfirm();
    }

    void EndTurnOnClick()
    {
        UIManager.Instance.PlayButtonSound();
        GameManager.Instance.currentPlayerTurn.OnTurnEnd();
        moveConfirmButton.gameObject.SetActive(true);
    }

    void PauseButtonOnClick()
    {
        UIManager.Instance.PlayButtonSound();
        UIManager.Instance.PauseGame();
    }
}
