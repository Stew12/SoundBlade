using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour
{
    public Text[] HP, MP = new Text[2];

    private Color defaultColourHP;

    public GameObject PlayerBase;
    private GameObject enemy;

    private float maxTime = 10f;
    private float time = 0;
    private bool timer = false;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        defaultColourHP = HP[0].color;
    }

    private void Update()
    {
        if (timer)
            time -= Time.time;

        Debug.Log(time);
        if (time <= 0)
        {
            HP[0].color = Color.red;
            timer = false;
            time = maxTime;
        }
        else
        {
           
            HP[0].color = defaultColourHP;
        }
            
    }

    public void HPChange(int newValue)
    {
        foreach (Text textLayers in HP)
        {
            if (newValue > 0)
                textLayers.text = newValue.ToString();
            else
                textLayers.text = "0";
        }
        
        timer = true;
        
    }

    public void MPChange(int newValue)
    {
        foreach (Text textLayers in MP)
        {
            if (newValue > 0)
                textLayers.text = newValue.ToString();
            else
                textLayers.text = "0";
        }
    }

    public void DamageFlag()
    {
        //Takes into consideration enemy attack and luck
        float enemyAttackStat = enemy.GetComponent<EnemyBase>().attack;
        float enemyLuckStat = enemy.GetComponent<EnemyBase>().luck;

        //Send to the base player object
        PlayerBase.GetComponent<PlayerBase>().TakeDamage(enemyAttackStat, enemyLuckStat);
    }
}
