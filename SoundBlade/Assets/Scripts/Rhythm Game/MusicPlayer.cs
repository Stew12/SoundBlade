using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource music;


    //Delay should be the time it takes for a note to reach the note hitters when spawned
    public float delay = 1f;

    private bool playing = false;
    public bool menuMusic;
    // Start is called before the first frame update
    void Start()
    {
        if (menuMusic)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((SceneManager.GetActiveScene().name == "Main Battle") && (menuMusic))
        {
            Destroy(gameObject);
        }

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
