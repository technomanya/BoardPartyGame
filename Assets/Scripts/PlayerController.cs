using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : PlayerBaseController
{
    void Update()
    {
        
        if (!isTurn) return;

        if(!isMoving)
        {
            if (!diceRolled)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentDice.RollDice();
                }
            }
            else
            {
                var directionTemp = Vector3.zero;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    directionTemp = transform.TransformDirection(Vector3.forward);
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    directionTemp = transform.TransformDirection(Vector3.back);
                }

                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    directionTemp = transform.TransformDirection(Vector3.right);
                }

                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    directionTemp = transform.TransformDirection(Vector3.left);
                }
                
                selfPreview.PreviewMoving(directionTemp);
            }
        }
    }

}
