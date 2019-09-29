using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] battlers = new GameObject[5];
    public GameObject[] battlerCameras = new GameObject[5];
    private GameObject mainCamera;
    private bool started = false;

    private int turnIndex = 0;

    public GameObject battleMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        battleMenu = GameObject.FindGameObjectWithTag("BattleMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.anyKey)
            {
                StartGame();
            }
        }
    }

    public void ChangeTurn()
    {
        battlerCameras[turnIndex].SetActive(false);

        Debug.Log(turnIndex);

        //Loop back to start if all battlers have had their turn
        if (turnIndex >= 4)
        {
            Debug.Log("you stoopid");
            turnIndex = 0;
        }
        else
        {
            Debug.Log("21");
            //Change to next battler's turn
            turnIndex++;
        }

        battlerCameras[turnIndex].SetActive(true);

        //Check if player or enemy
        if (battlers[turnIndex].tag == "Player")
        {
            battleMenu.SetActive(true);
            battlers[turnIndex].GetComponent<PlayerBase>().PlayerTurn();

        }
        else if (battlers[turnIndex].tag == "Enemy")
        {
            battlers[turnIndex].GetComponent<EnemyBase>().EnemyTurn();
        }
       
    }

    private void StartGame()
    {
        battlers[turnIndex].GetComponent<PlayerBase>().PlayerTurn();
        battlerCameras[turnIndex].SetActive(true);
        mainCamera.SetActive(false);

        started = true;
    }
}
