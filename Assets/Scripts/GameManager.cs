using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerBaseController currentPlayerTurn;
    public int currentPlayerTurnIndex = 0;
    public List<PlayerBaseController> playersInGame;
    
    [HideInInspector]
    public string winnerInTheEnd;

    public GameObject basicDiceObj, specialDiceObj;
    public int specailDiceCoolDownMax;
    
    public float gameSpeed;
    public Transform startTile;
    
    private CommandBase activeCommand;
    private Queue<CommandBase> commandQueue = new Queue<CommandBase>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetGame();   
        activeCommand = new MoveCommand(currentPlayerTurn,transform, gameSpeed);
    }

    private void Update()
    {
        if (activeCommand.isExecuting || commandQueue.Count <= 0) return;
        else
        {
            currentPlayerTurn.isMoving = false;
            if (currentPlayerTurn.CompareTag("AiPlayer"))
            {
                ChangeTurn();
            }
        }
        activeCommand = commandQueue.Dequeue();
        activeCommand.Execute();
    }

    public void StartGame()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        var aiPlayers = GameObject.FindGameObjectsWithTag("AiPlayer");

        var playerNum = 0;
        var _name = "";

        foreach (var player in players)
        {
            var playerTemp = player.GetComponent<PlayerBaseController>();
            playerTemp.specialDiceCoolDownSet = specailDiceCoolDownMax;
            playerTemp.specialDiceCoolDown = specailDiceCoolDownMax;
            playerNum++;
            _name = playerTemp.playerName;
            playerTemp.playerName = _name != "" ? _name : "Player "+playerNum;
            playersInGame.Add(playerTemp);
        }

        foreach (var aiPlayer in aiPlayers)
        {
            var aiPlayerTemp = aiPlayer.GetComponent<PlayerBaseController>();
            aiPlayerTemp.specialDiceCoolDownSet = specailDiceCoolDownMax;
            aiPlayerTemp.specialDiceCoolDown = specailDiceCoolDownMax;
            playerNum++;
            _name = aiPlayerTemp.playerName;
            aiPlayerTemp.playerName = _name != "" ? _name : "Player "+playerNum;
            playersInGame.Add(aiPlayerTemp);
        }
        OnGameStart();
        currentPlayerTurn.OnTurnStart();
    }

    public void RestartGame()
    {
        ResetGame();
        StartGame();
    }

    public void ResetGame()
    {
        currentPlayerTurnIndex = 0;
        foreach (var player in playersInGame)
        {
            player.PlayerReset();
        }
    }

    public void OnGameStart()
    {
        currentPlayerTurn = playersInGame[currentPlayerTurnIndex];
        currentPlayerTurn.isTurn = true;
    }

    public void ChangeTurn()
    {
        
        if (currentPlayerTurn.transform.parent!=null && currentPlayerTurn.transform.parent.GetComponent<GridTileData>().endTile)
        {
            winnerInTheEnd = currentPlayerTurn.playerName;
            Debug.Log(winnerInTheEnd);
            EndGame();
            return;
        }
        currentPlayerTurn.OnTurnEnd();
        currentPlayerTurnIndex = currentPlayerTurnIndex == playersInGame.Count - 1 ? 0 : ++currentPlayerTurnIndex;
        currentPlayerTurn = playersInGame[currentPlayerTurnIndex].GetComponent<PlayerBaseController>();
        currentPlayerTurn.OnTurnStart();
    }
    
    public void EndGame()
    {
        currentPlayerTurn = null;
        currentPlayerTurnIndex = -1;
        UIManager.Instance.OnEndGame(winnerInTheEnd);
    }

    public void MoveConfirm()
    {
        currentPlayerTurn.isMoving = true;
        for (int i = 0; i < currentPlayerTurn.selfPreview.previewListForTurn.Count; i++)
        {
            currentPlayerTurn.targetTile = currentPlayerTurn.selfPreview.previewListForTurn[i];
            commandQueue.Enqueue(new MoveCommand(currentPlayerTurn,currentPlayerTurn.targetTile,gameSpeed));
        }
    }
    public void PauseGame()
    {
        currentPlayerTurn.isTurn = false;
    }

    public void ContinueGame()
    {
        currentPlayerTurn.isTurn = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
