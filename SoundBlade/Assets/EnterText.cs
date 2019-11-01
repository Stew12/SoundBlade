using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterText : MonoBehaviour
{
    public float maxTime = 0.5f;
    private float time;
    private string txt;
    public bool toMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
        txt = GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            if (GetComponent<Text>().text == "")
            {
                GetComponent<Text>().text = txt;
            }
            else
            {
                GetComponent<Text>().text = "";
            }
            time = maxTime;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!toMenu)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene("Title Screen");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Battle");
        }

    }
}
