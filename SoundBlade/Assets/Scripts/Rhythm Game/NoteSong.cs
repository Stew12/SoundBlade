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

    private Image arrow;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponent<Image>();
        SetColour();
    }

    // Update is called once per frame
    void Update()
    {
        testTime -= Time.deltaTime;

        if (!test)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if ((testTime > 0) && (test))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    //Note hits one of the Note Hitters, make it disappear
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);   

        if (other.tag == "Party")
        {
            other.GetComponent<PlayerValues>().DamageFlag();
        }
    }

    private void SetColour()
    {
        switch (direction)
        {
            case "Right":
                arrow.color = Color.yellow;
                break;
            case "Up":
                arrow.color = Color.red;
                break;
            case "Down":
                arrow.color = Color.green;
                break;
            case "Left":
                arrow.color = Color.magenta;
                break;
        }
    }

   
}
