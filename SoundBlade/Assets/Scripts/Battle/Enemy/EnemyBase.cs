using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    //////////////////////Enemy stats//////////////////////

    //HP- how much damage enemy can take. 0 HP= enemy dead, win the game
    public float maxHP = 100f;

    //Attack- affects how much damage enemy deals when player misses a note- higher attack = more damage to player
    public float attack = 20f;

    //Defense- affects how much damage enemy recieves when player attacks them- higher defense = recieve less damage
    public float defense = 5f;

    //Speed- Aka max enemy turn time- affects how long enemy has during attack phase to spawn notes- higher speed = more attack time
    public float speed = 10f;

    //Fortitude- affects how long player has to attack enemy- more fortitude = less attack time for player
    public float fortitude = 20f;

    //Luck- affects the variation of attacks- more luck, less negative variation and more positive variation for attack damage
    public float luck = 2f;

    //////////////////////Time variables for attacking//////////////////////
    public float attackTime = 10;

    private float atTime;
    public bool timer = false;

    //////////////////////Important enemy variables//////////////////////


    public float currentHP;
    private float attackVariation;
    public float variation = 4;
    private int dmgLbl = 0;

    //Turn- decides if enemy can act or not
    public bool turn = false;


    //////////////////////GameObjects to call on//////////////////////

    private GameObject turnManager;
    private GameObject noteSpawner;
    public GameObject battleMenu;
    private GameObject mainCamera;

    //////////////////////UI//////////////////////

    public Text EHPUI;

    // Start is called before the first frame update
    void Start()
    {
        noteSpawner = GameObject.FindGameObjectWithTag("NoteGenerator");
        battleMenu = GameObject.FindGameObjectWithTag("BattleMenu");
        turnManager = GameObject.FindGameObjectWithTag("TurnManager");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        atTime = attackTime;

        currentHP = maxHP;
        dmgLbl = (int)currentHP;
        EHPUI.text = dmgLbl.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            atTime -= Time.deltaTime;
        }

        if (atTime <= 0)
        {
            EndEnemyTurn();
            atTime = attackTime;
            timer = false;
        }

        if (currentHP <= 0)
        {
            SceneManager.LoadScene("Win Screen");
        }

        //Send new value to the UI to inform the player
        dmgLbl = (int)currentHP;
        EHPUI.text = dmgLbl.ToString();
    }

    //Called when player hits enemy during attack phase
    public void TakeDamage(GameObject player)
    {
        //Takes into consideration attacking party member's attack and luck
        float playerAttackStat = player.GetComponent<PlayerBase>().attack;
        float playerLuckStat = player.GetComponent<PlayerBase>().luck;

        //Get varation (randomised, luck value affecting)
        float totalVariation = Random.Range(variation, variation + playerLuckStat);

        //Damage formula
        float damage = (playerAttackStat * playerAttackStat / (playerAttackStat + defense)) + totalVariation;
        //Subtract damage from hp
        currentHP -= damage;
    }

    public void EnemyTurn()
    {
        mainCamera.GetComponent<CameraSystem>().ZoomOut();
        battleMenu.SetActive(false);
        noteSpawner.GetComponent<NoteGeneration>().enabled = true;
        timer = true;
    }

    private void EndEnemyTurn()
    {
        noteSpawner.GetComponent<NoteGeneration>().enabled = false;
        turnManager.GetComponent<TurnManager>().ChangeTurn();
    }
}
