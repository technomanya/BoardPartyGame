using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveCommand : CommandBase
{
    private PlayerBaseController currentPlayer;
    private Transform targetTileTransform;
    private float gameSpeed;

    public MoveCommand(PlayerBaseController _currPlayer, Transform _targetTransform, float _gameSpeed)
    {
        currentPlayer = _currPlayer;
        targetTileTransform = _targetTransform;
        gameSpeed = _gameSpeed;
    }
    
    protected override async Task AsyncExecution()
    {
        currentPlayer.transform.parent = targetTileTransform;
        while (currentPlayer.transform.localPosition != Vector3.zero)
        {
            currentPlayer.transform.localPosition = Vector3.MoveTowards(currentPlayer.transform.localPosition,Vector3.zero, gameSpeed);
            await Task.Delay(10);
        }
    }
}
