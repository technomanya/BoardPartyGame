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
    public Transform endTile;
    
    private CommandBase activeCommand;
    private Queue<CommandBase> commandQueue = new Queue<CommandBase>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetGame();   
        activeCommand = new MoveCommand(currentPlayerTurn,transform, gameSpeed);
    }

    private void Update()
    {
        
        if (!activeCommand.isExecuting && commandQueue.Count > 0)
        {
            activeCommand = commandQueue.Dequeue();
            activeCommand.Execute();
        }

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
            
            playerNum++;
            _name = playerTemp.playerName;
            playerTemp.playerName = _name != "" ? _name : "Player "+playerNum;
            
            playerTemp.specialDiceCoolDownSet = specailDiceCoolDownMax;
            playerTemp.specialDiceCoolDown = specailDiceCoolDownMax;

            playerTemp.PlayerReset();
            playersInGame.Add(playerTemp);
        }

        foreach (var aiPlayer in aiPlayers)
        {
            var aiPlayerTemp = aiPlayer.GetComponent<PlayerBaseController>();
            
            playerNum++;
            _name = aiPlayerTemp.playerName;
            aiPlayerTemp.playerName = _name != "" ? _name : "Player "+playerNum;
            
            aiPlayerTemp.specialDiceCoolDownSet = specailDiceCoolDownMax;
            aiPlayerTemp.specialDiceCoolDown = specailDiceCoolDownMax;
            
            aiPlayerTemp.PlayerReset();
            playersInGame.Add(aiPlayerTemp);
        }
        OnGameStart();
    }

    public void RestartGame()
    {
        ResetGame();
        StartGame();
    }

    public void ResetGame()
    {
        currentPlayerTurnIndex = 0;
        if(playersInGame.Count<=0) return;
        
        foreach (var player in playersInGame)
        {
            player.moveAmount = 0;
            player.selfPreview.previewListForTurn.Clear();
            player.selfPreview.previewListForTurn = new List<Transform>();
            player.PlayerReset();
        }
        
    }

    public void MoveConfirm()
    {
        currentPlayerTurn.isMoving = true;
        foreach (var previewTransform in currentPlayerTurn.selfPreview.previewListForTurn)
        {
            currentPlayerTurn.targetTile = previewTransform;
            commandQueue.Enqueue(new MoveCommand(currentPlayerTurn,currentPlayerTurn.targetTile,gameSpeed));
        }
    }
    public void ChangeTurn()
    {
        
        if (currentPlayerTurn.transform.parent!=null && currentPlayerTurn.transform.parent == endTile)
        {
            winnerInTheEnd = currentPlayerTurn.playerName;
            Debug.Log(winnerInTheEnd);
            OnGameEnd();
            return;
        }
        currentPlayerTurnIndex = currentPlayerTurnIndex == playersInGame.Count - 1 ? 0 : ++currentPlayerTurnIndex;
        currentPlayerTurn = playersInGame[currentPlayerTurnIndex].GetComponent<PlayerBaseController>();
        currentPlayerTurn.OnTurnStart();
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
    
    private void OnGameStart()
    {
        currentPlayerTurn = playersInGame[currentPlayerTurnIndex];
        currentPlayerTurn.isTurn = true;
        
        currentPlayerTurn.OnTurnStart();
    }
    
    private void OnGameEnd()
    {
        currentPlayerTurn.isTurn = false;
        currentPlayerTurn = null;
        currentPlayerTurnIndex = -1;
        UIManager.Instance.OnEndGame(winnerInTheEnd);
    }
}
