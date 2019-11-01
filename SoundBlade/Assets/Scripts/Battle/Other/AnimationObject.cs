using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    private float animTime = 1f;
    public GameObject currentPlayer;
    private GameObject tm;
    private AudioSource spellSound;
    public bool changeTurn = true;

    // Start is called before the first frame update

    void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TurnManager");
        spellSound = GetComponent<AudioSource>();
        spellSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        animTime -= Time.deltaTime;
        if (animTime <= 0)
        {
            if (changeTurn)
                tm.GetComponent<TurnManager>().ChangeTurn();
            Destroy(gameObject);
        }
    }
}
