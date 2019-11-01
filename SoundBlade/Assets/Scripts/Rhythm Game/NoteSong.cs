using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteSong : MonoBehaviour
{
    public string direction;
    public float speed = 10;
    public float testTime = 1;
    public bool test = false;
    public bool hiding = false;

    private GameObject noteGen;
    private GameObject comboMeter;
    private SpriteRenderer arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponent<SpriteRenderer>();
        noteGen = GameObject.FindGameObjectWithTag("NoteGenerator");
        comboMeter = GameObject.FindGameObjectWithTag("Combo");
        SetColour();
    }

    // Update is called once per frame
    void Update()
    {
        testTime -= Time.deltaTime;

        if (!test)
        {
            transform.position += -Vector3.forward * speed * Time.deltaTime;
        }

        if ((testTime > 0) && (test))
        {
            transform.position += -Vector3.forward * speed * Time.deltaTime;
        }

        if (!noteGen.GetComponent<NoteGeneration>().enabled)
        {
            Hide();
        }
    }

    public void Hide()
    {
        hiding = true;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    //Note hits one of the Note Hitters, make it disappear
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);   

        if ((other.tag == "HitBox") && (!hiding))
        {
            other.GetComponent<HitBox>().DamagePlayer();

            //Reset the combo if hit
            comboMeter.GetComponent<ComboSystem>().currCombo = 0;
            comboMeter.GetComponent<ComboSystem>().LateText();
        }
        else if ((other.tag == "NoteHit") && (!hiding))
        {
            other.GetComponent<NoteReciever>().hit.Play();
            //Add to the combo
            comboMeter.GetComponent<ComboSystem>().currCombo++;
            comboMeter.GetComponent<ComboSystem>().late = false;
        }
    }

    private void SetColour()
    {
        switch (direction)
        {
            case "Right":
                arrow.color = Color.green;
                break;
            case "Up":
                arrow.color = Color.red;
                break;
            case "Down":
                arrow.color = Color.cyan;
                break;
            case "Left":
                arrow.color = Color.yellow;
                break;
        }
    }

   
}
