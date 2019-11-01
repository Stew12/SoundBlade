using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public AudioSource hit, miss, beat;

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
    private GameObject comboMeter;

    

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        comboMeter = GameObject.FindGameObjectWithTag("Combo");
        arrow = GetComponent<SpriteRenderer>();
        beatTime = 60 / BPM;
        time = beatTime;
        attackTime = maxAttackTime;
        outline = transform.GetChild(0).gameObject;
        slashEffect = transform.GetChild(1).gameObject;
        outline.SetActive(false);

        SetArrrow();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player);
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
            beat.Play();
            time = beatTime;
        }
        
        //End attack
        if (attackTime <= 0)
        {
            index = 0;
            attackTime = maxAttackTime;
            player.GetComponent<PlayerBase>().TurnEnd();
            StartCoroutine(example());
        }

        InputCheck();

      
    }

    public void SetArrrow()
    {
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
                hit.Play();
                HitEnemy();
            }
            else
            {
                miss.Play();
                attackTime = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if ((direction == "Left") && (outline.activeInHierarchy == true))
            {
                hit.Play();
                HitEnemy();
            }
            else
            {
                miss.Play();
                attackTime = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if ((direction == "Right") && (outline.activeInHierarchy == true))
            {
                hit.Play();
                HitEnemy();
            }
            else
            {
                miss.Play();
                attackTime = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if ((direction == "Down") && (outline.activeInHierarchy == true))
            {
                //Hit enemy
                hit.Play();
                HitEnemy();
            }
            else
            {
                //Miss attack
                miss.Play();
                attackTime = 0;
            }
        }
    }


    private void HitEnemy()
    {
        enemy.GetComponent<EnemyBase>().TakeDamage(player);
        comboMeter.GetComponent<ComboSystem>().currCombo++;

        slashEffect.SetActive(true);
        slashEffect.GetComponent<AttackGraphic>().ResetTime();

        index++;
        if (index > player.GetComponent<PlayerBase>().attackDirs.Length - 1)
        {
            index = 0;
        }

        direction = player.GetComponent<PlayerBase>().attackDirs[index];
    }

    IEnumerator example()
    {
        miss.Play();
        yield return new WaitWhile(() => miss.isPlaying);
        gameObject.SetActive(false);
    }
}
