using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource music;


    //Delay should be the time it takes for a note to reach the note hitters when spawned
    public float delay = 1f;

    private bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(music.time);
        delay -= Time.deltaTime;
        if ((delay <= 0) && !playing)
        {
            music.Play();
            playing = true;
        }

        if ((playing) && (!music.isPlaying))
        {
            music.Play();
        }
    }
}
