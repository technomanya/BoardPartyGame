using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUiController : MonoBehaviour
{
    [SerializeField] TMP_Text winnerNameText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;
    void Start()
    {
        replayButton.onClick.AddListener(ReplayButtonOnClick);
        mainMenuButton.onClick.AddListener(MainMenuButtonOnClick);
    }

    public void UpdateWinnerName(string _winnerName)
    {
        winnerNameText.text = _winnerName;
    }

    void ReplayButtonOnClick()
    {
        UIManager.Instance.PlayButtonSound();
        UIManager.Instance.RestartGame();
    }

    void MainMenuButtonOnClick()
    {
        UIManager.Instance.PlayButtonSound();
        UIManager.Instance.MainMenu();
    }
}
