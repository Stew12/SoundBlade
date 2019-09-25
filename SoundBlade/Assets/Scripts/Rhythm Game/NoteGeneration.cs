using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class NoteGeneration : MonoBehaviour {
    public GameObject spawnUp, spawnDown, spawnLeft, spawnRight;
    public GameObject note;

    private GameObject canvas;

    public List<float> noteTimes = new List<float>();
    public List<string> noteDirs = new List<string>();

    public float currTime = 0f;
    public string[] noteList;
    public bool enabled = false;
    
    private int index = 0;
    private bool finishedSong = false;

    // Use this for initialization
    void Start() {
        readFile();
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update() {

        currTime += Time.deltaTime;

        if (!finishedSong)
        {
            noteSpawn();
        }

    }

    void readFile()
    {
        noteList = System.IO.File.ReadAllLines(@"C:\Users\Lachlan\Desktop\GitHub\SoundBlade\SoundBlade\Assets\Scripts\Song1.txt");
        string[] storedValues = new string[2];
        foreach (string line in noteList)
        {
            storedValues = line.Split(',');
            noteTimes.Add(float.Parse(storedValues[0], CultureInfo.InvariantCulture.NumberFormat));
            noteDirs.Add(storedValues[1]);
        }

    }

    void noteSpawn()
    {
        if (currTime >= noteTimes[index])
        {
            GameObject spawnLocation = spawnUp;
            float rotation = 0;

            switch (noteDirs[index])
            {
                case "Left":
                    spawnLocation = spawnLeft;
                    rotation = 90;
                    break;
                case "Up":
                    spawnLocation = spawnUp;
                    rotation = 0;
                    break;
                case "Down":
                    spawnLocation = spawnDown;
                    rotation = 180;
                    break;
                case "Right":
                    spawnLocation = spawnRight;
                    rotation = 270;
                    break;
                case "":
                    spawnLocation = spawnUp;
                    rotation = 0;
                    break;
            }

            if (enabled)
            {
                GameObject spawnedNote = Instantiate(note, spawnLocation.transform.position, Quaternion.Euler(0f, 0f, rotation));
                spawnedNote.GetComponent<NoteSong>().direction = noteDirs[index];
                spawnedNote.transform.SetParent(canvas.transform);
            }

            //Check if all notes have been not been played
            if (index < noteTimes.Count - 1)
            {
                index++;
            }
            else
            {
                //All notes played
                finishedSong = true;
            }

        }
    }
}
