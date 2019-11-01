using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    //////////////////////Party member stats//////////////////////

    //HP- how much damage party member can take. 0 HP= fainting
    public float maxHP = 100f;

    //MP- depletes when using skills- not enough MP = skill can't be used
    public float maxMP = 50f;

    //Attack- affects how much damage party member deals to enemy- higher attack = more damage to enemy
    public float attack = 20f;

    //Defense- affects how much damage party member recieves when missing a note during enemy attacks- higher defense = recieve less damage
    public float defense = 5f;

    //Speed- Aka max attack time- affects how long player has during attack phase to attack the enemy- higher speed = more attack time
    public float speed = 10f;

    //Luck- affects the variation of attacks- more luck, less negative variation and more positive variation for attack damage
    public float luck = 3f;

    //////////////////////Important party member variables//////////////////////

    public float currentHP;
    public float currentMP;
    private float attackVariation;
    private float atkTime;
    private float defenseRegular;

    public float variation = 4;
    private float cameraSpeed = 5;
    private float xPos;

    //Turn- decides if player can act or not
    public bool turn = false;
    public bool turnSent = false;

    public bool defenseBuff = false;

    //////////////////////Skill and Item Arrays//////////////////////
    public string[] Skills = new string[3];
    public string[] Items = new string[3];

    //////////////////////GameObjects to call on//////////////////////

    //This object is used to change the values of the party member's UI, such as hp and mp
    public GameObject PartyMemberUI;
    public GameObject cameraObject;

    private GameObject turnManager;
    private GameObject battleMenu;
    private GameObject mainCamera;
    private GameObject enemy;
    private SpriteRenderer sprite;
    public AudioSource hurt, die;

    //Array for the pattern of attack directions
    public string[] attackDirs = new string[4];


    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        defenseRegular = defense;
        xPos = transform.position.x;
        turnManager = GameObject.FindGameObjectWithTag("TurnManager");
        battleMenu = GameObject.FindGameObjectWithTag("BattleMenu");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFainted();

        if (defenseBuff)
        {
            defense *= 1.1f;
        }
        else
        {
            defense = defenseRegular;
        }

        if ((turn) && (!turnSent))
            MenuInterface();

        //Zoom in on party member if it is their turn
        if (turn)
        {
            mainCamera.GetComponent<CameraSystem>().ZoomIn(gameObject);    
        }

        //Reduce opacity during enemy's turn so player can see oncoming notes
        if (enemy.GetComponent<EnemyBase>().timer)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.7f);
        }
        else
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void PlayerTurn()
    {
        turn = true;
       defenseBuff = false;
    }

    //Is called when the party member misses a note
    public void TakeDamage(float enemyAttack, float enemyluck)
    {
        hurt.Play();

        //Get varation (randomised, luck value affecting)
        float totalVariation = Random.Range(variation, variation + enemyluck);

        //Damage formula
        float damage = (enemyAttack * enemyAttack / (enemyAttack + defense)) + totalVariation;
        //Subtract damage from hp
        currentHP -= damage;
        //Send new value to the UI to inform the player
        PartyMemberUI.GetComponent<PlayerValues>().HPChange((int)currentHP);
    }

    //Checks to see if hp is depleted
    private void CheckFainted()
    {
        if (currentHP <= 0)
        {
            currentHP = 0.1f;

            //Remove from turn manager
            turnManager.GetComponent<TurnManager>().battlers.Remove(gameObject);

            //Increase enemy attack for every party member defeated
            enemy.GetComponent<EnemyBase>().attack *= 1.5f;

            StartCoroutine(example());
        }
    }

    private void MenuInterface()
    {
        //Show the battle menu and have it handle inputs

        battleMenu.GetComponent<MenuControl>().currentPlayer = gameObject;
        battleMenu.GetComponent<MenuControl>().GetPlayer(gameObject);
        turnSent = true;
    }


    public void TurnEnd()
    {
        turn = false;
        turnSent = false;
        turnManager.GetComponent<TurnManager>().ChangeTurn();
    }

    public void SetHPLabel()
    {
        PartyMemberUI.GetComponent<PlayerValues>().HPChange((int)currentHP);
    }

    //Is called when a party member casts a damaging spell
    public void DealSpellDamage(float damageLevel, float mpCost, bool buffing, bool debuffing)
    {

        //Get varation (randomised, luck value affecting)
        float totalVariation = Random.Range(variation, variation + luck);

        //Damage formula
        float damage = ((attack * attack) / (attack + enemy.GetComponent<EnemyBase>().defense)) + totalVariation;

        if (debuffing)
        {
            enemy.GetComponent<EnemyBase>().attack /= 1.5f;
        }
        else if (buffing)
        {
            attack *= 1.5f;
        }

        //Reduce MP
        currentMP -= mpCost;

        PartyMemberUI.GetComponent<PlayerValues>().MPChange((int)currentMP);

        //If run out of mp for skill, remove it from the player's list
        if (currentMP < mpCost)
        {
            Skills[0] = "";
        }

        

        //Subtract damage from hp
        enemy.GetComponent<EnemyBase>().currentHP -= damageLevel * damage;
    }

    IEnumerator example()
    {
        die.Play();
        yield return new WaitWhile(() => die.isPlaying);
        Destroy(gameObject);
    }
}
