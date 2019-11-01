using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteReciever : MonoBehaviour {
    private BoxCollider noteCollide;
    public string inputDir = "Up";

    public float maxTime = 0.25f, cMaxtime = 0.5f;
    private float timeLeft, ctimeLeft;
    private SpriteRenderer image;

    // Use this for initialization
    void Start () {
        timeLeft = 0;
        ctimeLeft = 0;
        image = GetComponent<SpriteRenderer>();
        noteCollide = GetComponent<BoxCollider>();
        noteCollide.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        keyCheck();

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            Color tempcolour = image.color;
            tempcolour.a = 0.7f;
            image.color = tempcolour;
            noteCollide.enabled = false;
        }

        if (ctimeLeft > 0)
        {
            ctimeLeft -= Time.deltaTime;
        }
    }



    private void recieverActivate()
    {
        noteCollide.enabled = true;
        Color tempcolour = image.color;
        tempcolour.a = 1f;
        image.color = tempcolour;
        timeLeft = maxTime;
        ctimeLeft = cMaxtime;
    }



    private void keyCheck()
    {
        if (ctimeLeft <= 0)
        {
            if (inputDir == "Up")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    recieverActivate();
                }

            }

            else if (inputDir == "Down")
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    recieverActivate();
                }

            }

            else if (inputDir == "Left")
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    recieverActivate();
                }

            }

            else if (inputDir == "Right")
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    recieverActivate();
                }

            }
        }

    }

     
}
