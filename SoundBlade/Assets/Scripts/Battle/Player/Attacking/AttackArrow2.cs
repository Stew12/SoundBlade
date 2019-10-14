using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArrow2 : MonoBehaviour
{

    public string direction = "Up";
    private SpriteRenderer arrow;

    public float maxTime = 0.2f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponent<SpriteRenderer>();
        SetColour();
        time = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        SetColour();

        time -= Time.deltaTime;

        if (time <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    public void ResetTime()
    {
        time = maxTime;
    }

    private void SetColour()
    {
        switch (direction)
        {
            case "Right":
                arrow.color = new Color(0f, 1f, 0f, 1);
                break;
            case "Up":
                arrow.color = new Color(1f, 0f, 0f, 1);
                break;
            case "Down":
                arrow.color = new Color(0f, 1f, 1f, 1);
                break;
            case "Left":
                arrow.color = new Color(1f, 0.92f, 0.016f, 1);
                break;
        }
    }
}
