using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBounce : MonoBehaviour
{
    public float BPM = 150f;
    private float beatTime, speedyBeatTime, orginalBeatTime, time;

    private GameObject sourcePlayer;

    public float yMovement = 0.05f;

    private bool bounceUp = true;

    // Start is called before the first frame update
    void Start()
    {
        beatTime = 60 / BPM / 2;
        orginalBeatTime = beatTime;
        speedyBeatTime = beatTime / 1.5f;
        time = beatTime;
        sourcePlayer = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        //If it is the player's turn, have them bounce faster
        if ((sourcePlayer != null ) && (sourcePlayer.GetComponent<PlayerBase>().turn))
        {
            beatTime = speedyBeatTime;
        }
        else
        {
            beatTime = orginalBeatTime;
        }

        time -= Time.deltaTime;

        //Bounce sprite on beat
        if (time <= 0)
        {
            if (bounceUp)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - yMovement, gameObject.transform.position.z);
            else
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + yMovement, gameObject.transform.position.z);

            bounceUp = !bounceUp;
            time = beatTime;
        }
    }

    
}
