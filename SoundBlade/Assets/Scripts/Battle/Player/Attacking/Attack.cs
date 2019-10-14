using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private string direction = "Up";
    private SpriteRenderer arrow;

    private float maxAttackTime = 5.0f;
    private float attackTime;
    public float BPM = 100f;
    private float beatTime, time;
    private int index = 0;
    private float rotation = 0;

    public GameObject player;
    private GameObject outline;
    private GameObject slashEffect;
    private GameObject enemy;

    

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        arrow = GetComponent<SpriteRenderer>();
        beatTime = 60 / BPM;
        time = beatTime;
        attackTime = maxAttackTime;
        outline = transform.GetChild(0).gameObject;
        slashEffect = transform.GetChild(1).gameObject;
        outline.SetActive(false);

        switch (player.GetComponent<PlayerBase>().attackDirs[0])
        {
            case "Right":
                direction = "Right";
                break;
            case "Up":
                direction = "Up";
                break;
            case "Down":
                direction = "Down";
                break;
            case "Left":
                direction = "Left";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetColour();

        transform.rotation =  Quaternion.Euler(0, 0, rotation);

        outline.GetComponent<AttackArrow2>().direction = direction;
        slashEffect.GetComponent<AttackGraphic>().direction = direction;

        attackTime -= Time.deltaTime;
        time -= Time.deltaTime;

        //Show arrow on beat
        if (time <= 0)
        {
            outline.SetActive(true);
            outline.GetComponent<AttackArrow2>().ResetTime();
            time = beatTime;
        }
        
        //End attack
        if (attackTime <= 0)
        {
            index = 0;
            attackTime = maxAttackTime;
            player.GetComponent<PlayerBase>().TurnEnd();
            gameObject.SetActive(false);
        }

        InputCheck();

      
    }

    private void SetColour()
    {
        switch (direction)
        {
            case "Right":
                arrow.color = new Color(0f, 1f, 0f, 0.7f);
                rotation = 270;
                break;
            case "Up":
                arrow.color = new Color(1f, 0f, 0f, 0.7f);
                rotation = 0;
                break;
            case "Down":
                arrow.color = new Color(0f, 1f, 1f, 0.7f);
                rotation = 180;
                break;
            case "Left":
                arrow.color = new Color(1f, 0.92f, 0.016f, 0.7f);
                rotation = 90;
                break;
        }
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if ((direction == "Up") && (outline.activeInHierarchy == true))
            {
                enemy.GetComponent<EnemyBase>().TakeDamage(player);
                NextNote();
            }
            else
            {
                attackTime = 0;
            }
        }
        //Skills
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if ((direction == "Left") && (outline.activeInHierarchy == true))
            {
                enemy.GetComponent<EnemyBase>().TakeDamage(player);
                NextNote();
            }
            else
            {
                attackTime = 0;
            }
        }
        //Item
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if ((direction == "Right") && (outline.activeInHierarchy == true))
            {
                enemy.GetComponent<EnemyBase>().TakeDamage(player);
                NextNote();
            }
            else
            {
                attackTime = 0;
            }
        }
        //Defend
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if ((direction == "Down") && (outline.activeInHierarchy == true))
            {
                enemy.GetComponent<EnemyBase>().TakeDamage(player);
                NextNote();
            }
            else
            {
                attackTime = 0;
            }
        }
    }

    private void NextNote()
    {
        slashEffect.SetActive(true);
        slashEffect.GetComponent<AttackGraphic>().ResetTime();

        index++;
        if (index > player.GetComponent<PlayerBase>().attackDirs.Length - 1)
        {
            index = 0;
        }

        direction = player.GetComponent<PlayerBase>().attackDirs[index];
    }
}
