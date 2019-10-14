using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteRecorded : MonoBehaviour
{
    public GameObject musicBar;
    public string direction;

    private Image arrow;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponent<Image>();
        SetColour();
        musicBar = GameObject.FindGameObjectWithTag("MusicBar");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Playback bar hits note
    private void OnTriggerEnter2D(Collider2D other)
    {
        arrow.color = Color.white;

        if (other.GetComponent<MusicBar>().recording)
        other.GetComponent<MusicBar>().WriteTo(direction);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SetColour();
    }

    private void SetColour()
    {
        Color mainColor;
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

    public void PlaceNote()
    {
        GameObject note = Instantiate(gameObject, musicBar.transform.position, gameObject.transform.rotation);
        note.transform.SetParent(GameObject.Find("Canvas").transform);
    }
}
