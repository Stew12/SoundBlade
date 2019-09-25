using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] battlers = new GameObject[5];
    private int turnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        battlers[turnIndex].GetComponent<PlayerBase>().PlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTurn()
    {
        //Change to next battler's turn
        turnIndex++;

        //Loop back to start if all battlers have had their turn
        if (turnIndex > battlers.Length)
        {
            turnIndex = 0;
        }

        //Check if player or enemy
        if (battlers[turnIndex].tag == "Player")
        {
            battlers[turnIndex].GetComponent<PlayerBase>().PlayerTurn();
        }
        else if (battlers[turnIndex].tag == "Enemy")
        {
            battlers[turnIndex].GetComponent<EnemyBase>().EnemyTurn();
        }
       
    }
}
