using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePreview : MonoBehaviour
{
    public List<Transform> previewListForTurn;
    public int moveAmount;
    
    private PlayerController player;
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 directionTemp =Vector3.zero;
        bool isHit = false;
        if(player.isTurn && !player.isMoving && Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                directionTemp = transform.TransformDirection(Vector3.forward);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                directionTemp = transform.TransformDirection(Vector3.back);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                directionTemp = transform.TransformDirection(Vector3.right);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                directionTemp = transform.TransformDirection(Vector3.left);
            }

            isHit = Physics.Raycast(transform.position, directionTemp, out hit, 1);
            Debug.Log(isHit);
            if (isHit && hit.collider.CompareTag("Tile"))
            {
                Debug.Log(hit.collider.name);
                if (previewListForTurn.Count == 0)
                {
                    moveAmount--;
                    previewListForTurn.Add(hit.transform);
                    transform.parent = hit.transform;
                    transform.localPosition = Vector3.zero;
                }
                else
                {
                    if (moveAmount>0 && !previewListForTurn.Contains(hit.transform))
                    {
                        moveAmount--;
                        previewListForTurn.Add(hit.transform);
                        transform.parent = hit.transform;
                        transform.localPosition = Vector3.zero;
                    }
                    else if(previewListForTurn.Contains(hit.transform))
                    {
                        moveAmount++;
                        previewListForTurn.Remove(previewListForTurn[^1]);
                        transform.parent = hit.transform;
                        transform.localPosition = Vector3.zero;
                    }
                }
                

                
            }
        }
    }
}
