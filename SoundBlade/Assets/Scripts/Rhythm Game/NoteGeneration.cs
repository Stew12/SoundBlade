using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NoteGeneration : MonoBehaviour {
    public GameObject spawnUp, spawnDown, spawnLeft, spawnRight, note;
    public float currTime = 0f, BPM = 385.0f;
    private int counter = 0, counter2 = 0;
    private float pitch;
    private bool finished = false;
    private float MIDITicks;

    //List of note time stamps and their corresponding pitches, copied from .csv files of the MIDI
    public string noteT = "0.45,0.65,1,1.346,1.544,2", noteP = "C,D#,F,B,E,G";

    private string[] noteTimes;
    private string[] notePitches;
    private float[] noteTimes2;
    private float[] notePitches2;

    // Use this for initialization
    void Start() {
        noteTimes = noteT.Split(',');
        notePitches = noteP.Split(',');

        //BPM equation
        MIDITicks = BPM * 24 / 60;
       

        noteTimes2 = new float[noteTimes.Length];
        for (int i = 0; i < noteTimes.Length; i++)
        {
            noteTimes2[i] = float.Parse(noteTimes[i]);
            noteTimes2[i] /= MIDITicks;
        }

        notePitches2 = new float[notePitches.Length];
        for (int i = 0; i < notePitches2.Length; i++)
        {
            notePitches2[i] = float.Parse(notePitches[i]);
        }

    }

    // Update is called once per frame
    void Update() {
        currTime += Time.deltaTime;

        if (counter > noteTimes2.Length - 1)
        {
            finished = true;
        }

        if (currTime >= noteTimes2[counter] && finished == false)
        {
            findPitch();
            counter++;
        }

    }

    private void findPitch()
    {
        Debug.Log(notePitches2[counter]);
        pitch = notePitches2[counter];

        if ((pitch > 47 && pitch < 55) || (pitch >= 74 && pitch < 79))
        {
            Instantiate(note, spawnLeft.transform.position, Quaternion.identity);
        }
        else if ((pitch >= 55 && pitch < 61) || (pitch >= 79 && pitch < 86))
        {
            Instantiate(note, spawnDown.transform.position, Quaternion.identity);
            
        }
        else if ((pitch >= 61 && pitch < 69) || (pitch >= 86 && pitch < 93))
        {
            Instantiate(note, spawnUp.transform.position, Quaternion.identity);
        }
        else if ((pitch >= 69 && pitch < 73) || (pitch >= 93 && pitch < 97))
        {
            Instantiate(note, spawnRight.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Error: " + pitch);
        }

        //right arrow
        //Down = cd
        //right = EF
        //left = G
        //up = AB
    }
}
