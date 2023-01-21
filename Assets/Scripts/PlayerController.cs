using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isTurn;
    public bool isMoving;
    
    [SerializeField]private int moveAmount;
    [SerializeField]private Transform targetTile;
    [SerializeField] private Transform[] possibleTargetTiles;
    [SerializeField] private float turnAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurn && !isMoving)
        {
            if (possibleTargetTiles.Length > 1)
            {
                
            }
            else
            {
                Move(moveAmount);
            }
        }
    }

    void Move(int _remainingMove)
    {
        if(_remainingMove == 0)
        {
            isMoving = false;
            isTurn = false;
            return;
        }
        if (possibleTargetTiles.Length > 1)
        {
            isMoving = false;
            return;
        }
        if(possibleTargetTiles.Length == 1)
        {
            transform.LookAt(possibleTargetTiles[0]);
            transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
        }
        isMoving = true;

        transform.Rotate(transform.up,turnAmount);
        transform.Translate(transform.forward);
        Move(--_remainingMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GridTileData>())
        {
            var gridTileData = other.GetComponent<GridTileData>();
            turnAmount = gridTileData.turnAngle;
            possibleTargetTiles = gridTileData.possibleDirections;
        }
    }
}
