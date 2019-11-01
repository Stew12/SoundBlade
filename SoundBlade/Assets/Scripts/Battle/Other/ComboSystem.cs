using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public Text[] comboNumbers = new Text[2];
    public Text[] comboWords = new Text[2];

    public float currCombo = 0;
    public bool late = false;

    private Color textColour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i < comboNumbers.Length; i++)
        {
            comboNumbers[i].text = currCombo.ToString();
            if (!late)
            {
                comboWords[i].text = WordCheck(currCombo);
                comboWords[1].color = textColour;
            }
        }
        
    }

    public void LateText()
    {
        late = true;
        for (int i = 0; i < comboWords.Length; i++)
        {
            comboWords[i].text = "Too Late!";
        }
        comboWords[1].color = Color.black;
    }

    //'Combo words' e.g. "Nice! Good! Great!"
    private string WordCheck(float comboNo)
    {
        textColour = Color.white;
        string word = "";

        if (comboNo <= 5)
        {
            word = "";
        }
        if ((comboNo > 5) && (comboNo < 15))
        {
            word = "OK...";
            textColour = Color.white;
        }
        else if ((comboNo >= 15) && (comboNo < 35))
        {
            word = "Nice!";
            //Orange
            textColour = new Color(1.0f, 0.64f, 0.0f); 
        }
        else if ((comboNo >= 35) && (comboNo < 45))
        {
            word = "Cool!";
            textColour = Color.blue;
        }
        else if ((comboNo >= 45) && (comboNo < 65))
        {
            word = "Great!";
            textColour = Color.green;
        }
        else if ((comboNo >= 75) && (comboNo < 85))
        {
            word = "Groovin'!";
            textColour = Color.cyan;
        }
        else if ((comboNo >= 85) && (comboNo < 100))
        {
            word = "Maestro!";
            textColour = Color.red;
        }
        else if (comboNo >= 100)
        {
            word = "Perfect!";
            textColour = Color.yellow;
        }

        return word;
    }
}
