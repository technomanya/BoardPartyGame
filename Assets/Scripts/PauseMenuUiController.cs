using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUiController : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButoon;

    private void Start()
    {
        continueButton.onClick.AddListener(ContinueButtonOnClick);
        mainMenuButoon.onClick.AddListener(MainMenuButtonOnClick);
        restartButton.onClick.AddListener(RestartButtonOnClick);
    }

    void ContinueButtonOnClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.ContinueGame();
    }
    void RestartButtonOnClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.RestartGame();
    }
    void MainMenuButtonOnClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.MainMenu();
    }
}
