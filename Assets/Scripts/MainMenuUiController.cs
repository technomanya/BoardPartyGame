using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUiController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayButtonOnClick);
        quitButton.onClick.AddListener(QuitButtonOnClick);
    }

    void PlayButtonOnClick()
    {
        UIManager.Instance.PlayGame();
        gameObject.SetActive(false);
    }

    void QuitButtonOnClick()
    {
        UIManager.Instance.QuitGame();
    }
}
