﻿using System.Collections;
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

    //Fortitude- aggregate fortitude of all party members are used in calculating how long the enemy has during its attack phase- more fortitude = less attack time
    public float fortitude = 20f;

    //Luck- affects the variation of attacks- more luck, less negative variation and more positive variation for attack damage
    public float luck = 3f;

    //////////////////////Important party member variables//////////////////////

    private float currentHP;
    private float currentMP;
    private float attackVariation;
    private float atkTime;

    public float variation = 4;
    

    //////////////////////GameObjects to call on//////////////////////
    
    //This object is used to change the values of the party member's UI, such as hp and mp
    public GameObject PartyMemberUI;





    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // Update is called once per frame
    void Update()
    {
        CheckFainted();
    }

    //Is called when the party member misses a note
    public void TakeDamage(float enemyAttack, float enemyluck)
    {
        //Get varation (randomised, luck value affecting)
        float totalVariation = Random.Range(-variation, variation + enemyluck);

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
            //Faint
        }
    }
}