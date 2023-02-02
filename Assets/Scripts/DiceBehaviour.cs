using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceBehaviour : MonoBehaviour
{
    public int diceUseCoolDownBase;
    public int specialDiceDifference;
    
    public Sprite[] diceFaces;

    [SerializeField] private Button diceButton;
    [SerializeField] private float rollingFrequency;
    private bool isRolling = false;
    private float rollingTimePrev;
    private GameManager GM;
    private void Start()
    {
        GM = GameManager.Instance;
        diceButton = GetComponent<Button>();
        //diceButton.onClick.AddListener(ButtonClickAction);
    }

    public void RollDice()
    {
        //Debug.Log(GM.currentPlayerTurn);
        GM.currentPlayerTurn.selfPreview.previewListForTurn.Clear();
        GM.currentPlayerTurn.selfPreview.previewListForTurn = new List<Transform>();
        StartCoroutine(RollingStart(1));
    }

    private void Update()
    {
        if (isRolling && Time.deltaTime > rollingTimePrev+rollingFrequency)
        {
            
            var randFaceIndex = 0;
        
            randFaceIndex = Random.Range(0, diceFaces.Length);
            GetComponent<Image>().sprite = diceFaces[randFaceIndex];  
        }
    }

    private IEnumerator RollingStart(float seconds)
    {
        GameManager.Instance.currentPlayerTurn.diceRolled = true;
        isRolling = true;
        //GM.currentPlayerTurn.isMoving = false;
        yield return new WaitForSeconds(seconds);
        
        isRolling = false;
        
        var randFaceIndex = 0;
        
        randFaceIndex = Random.Range(0, diceFaces.Length);
        GetComponent<Image>().sprite = diceFaces[randFaceIndex];

        GM.currentPlayerTurn.moveAmount = randFaceIndex + specialDiceDifference + 1;
    }
}
