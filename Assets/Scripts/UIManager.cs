using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text currentPlayerNameText;
    public GameObject playerPanel;
    public GameObject mainMenuPanel;
    public GameObject endGamePanel;
    public GameObject pausePanel;
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayGame()
    {
        GameManager.Instance.StartGame();
    }
    
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }
    public void MainMenu()
    {
        GameManager.Instance.ResetGame();
        mainMenuPanel.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    
    
    public void OnEndGame(string winnerName)
    {
        endGamePanel.GetComponent<EndGameUiController>().UpdateWinnerName(winnerName);
        endGamePanel.gameObject.SetActive(true);
    }

    public void UpdateCurrentPlayerNameText(string name)
    {
        currentPlayerNameText.text = name;
    }

}