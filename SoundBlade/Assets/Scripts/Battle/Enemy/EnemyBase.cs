using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //////////////////////Important enemy variables//////////////////////

    
    private float currentHP;
    private float attackVariation;

    //Turn- decides if enemy can act or not
    public bool turn = false;


    //////////////////////GameObjects to call on//////////////////////

    private GameObject turnManager;
    private GameObject noteSpawner;



    // Start is called before the first frame update
    void Start()
    {
        noteSpawner = GameObject.FindGameObjectWithTag("NoteGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyTurn()
    {
        noteSpawner.GetComponent<NoteGeneration>().enabled = true;
    }
}
