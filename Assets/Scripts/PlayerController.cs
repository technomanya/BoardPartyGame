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
       
    }
    
}
