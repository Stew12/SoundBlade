using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenData : MonoBehaviour
{
    private float time;
    private GameObject combos;
    private float highestCombo = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        combos = GameObject.FindGameObjectWithTag("Combo");
        if (combos != null)
        {
            if (combos.GetComponent<ComboSystem>().currCombo > highestCombo)
            {
                highestCombo = combos.GetComponent<ComboSystem>().currCombo;
            }
        } 

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Win Screen")
        {
            GameObject timeLabel = GameObject.Find("time");
            GameObject comboLabel = GameObject.Find("combo");
            GameObject perfectLabel = GameObject.Find("perfect");
            int t = (int)time;
            int c = (int)highestCombo;
            timeLabel.GetComponent<Text>().text = "Clear Time: " + t.ToString();
            comboLabel.GetComponent<Text>().text = "Best Combo: " + c.ToString();

            float minutes = Mathf.Floor(time / 60);
            float seconds = Mathf.RoundToInt(time % 60);
            string minutess = "";
            string secondss = "";

            if (minutes < 10)
            {
               minutess = "0" + minutes.ToString();
            }
            if (seconds < 10)
            {
                secondss = "0" + Mathf.RoundToInt(seconds).ToString();
            }

            //Avoid weird blank error
            if (secondss == "")
            {
                secondss = "00";
            }

            timeLabel.GetComponent<Text>().text = "Clear Time: " + minutess + ":" + secondss;

            if (highestCombo < 100)
            {
                perfectLabel.GetComponent<Text>().text = "Go for a Perfect Combo!";
            }
            else
            {
                perfectLabel.GetComponent<Text>().text = "Perfect! You are a master!";
            }
        }
        else
        {
            time += Time.deltaTime;
        }

    }
}
