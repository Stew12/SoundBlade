using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> battlers = new List<GameObject>();
    //public GameObject[] battlerCameras = new GameObject[5];
    private GameObject mainCamera;
    private bool started = false;
    public GameObject PlayerText, EnemyText;

    private int turnIndex = -1;

    public GameObject battleMenu;

    // Start is called before the first frame update
    void Start()
    {
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

        //Check if all party members dead
        if (battlers.Count < 2)
        {
            SceneManager.LoadScene("Game Over");
        }

    }

    public void ChangeTurn()
    {
        Debug.Log(turnIndex);

        //Loop back to start if all battlers have had their turn
        if (turnIndex >= 5)
        {
            turnIndex = 0;
        }
        else
        {
            //Change to next battler's turn
            turnIndex++;
        }

        //Check if player or enemy
        if (battlers[turnIndex].tag == "Player")
        {
            PlayerText.SetActive(true);
            EnemyText.SetActive(false);
            battleMenu.SetActive(true);
            battlers[turnIndex].GetComponent<PlayerBase>().PlayerTurn();

        }
        else if (battlers[turnIndex].tag == "Enemy")
        {
            PlayerText.SetActive(false);
            EnemyText.SetActive(true);
            battlers[turnIndex].GetComponent<EnemyBase>().EnemyTurn();
        }
       
    }

    private void StartGame()
    {
        ChangeTurn();

        started = true;
    }
}
