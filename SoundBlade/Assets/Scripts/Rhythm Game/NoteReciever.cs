using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteReciever : MonoBehaviour {
    private Collider2D noteCollide;
    public string inputDir = "Up";

    public float maxTime = 0.25f, cMaxtime = 0.5f;
    private float timeLeft, ctimeLeft;
    private Image image;

    // Use this for initialization
    void Start () {
        timeLeft = 0;
        ctimeLeft = 0;
        image = GetComponent<Image>();
        noteCollide = GetComponent<CircleCollider2D>();
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
            image.color = new Color(1f, 1f, 1f, 1f);
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
        image.color = new Color(1f, 1f, 1f, .7f);
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
