using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MusicBar : MonoBehaviour
{
    

    public AudioSource music;
    public GameObject startBar;
    public GameObject Treble;

    private StreamWriter noteWriter;

    public float movementSpeed;
    public float musicVolume = 0.561f;
    public bool recording = false;
    public string songName = "Song1";

    private float distance;
    private float songLength;
    private string path;
    private bool paused = false;
    private Vector3 startingPos;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = Treble.transform.position;
        transform.position = startingPos;
        //path = "C:\\Users\\Lachlan\\Documents\\SoundBlade\\Assets\\Scripts\\Song1.txt";
        path = "Assets\\Scripts\\SongFiles\\Song1.txt";
        songLength = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = new Vector3(Input.mousePosition.x, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown("space")) 
        {
            if (!paused)
            {
                music.volume = 0;
                paused = true;
            }
            else
            {
                music.volume = musicVolume;
                paused = false;
            }
        }

        if (Input.GetKeyDown("r"))
        {
            transform.position = startingPos;
            movementSpeed = songLength;
            recording = true;
        }

        if (Input.GetKeyDown("e"))
        {
            transform.position = startingPos;
        }

        if (Input.GetKeyDown("1"))
        {
            movementSpeed /= 2;
        }

        if (Input.GetKeyDown("2"))
        {
            movementSpeed *= 2;
        }

        distance = transform.position.x - startBar.transform.position.x;
        if (!paused)
        transform.position += Vector3.right * (movementSpeed * Time.deltaTime);

        music.time = distance/songLength;

        music.Play();
        
        
        //File.Delete(path);
        //File.Create(path);

    }

    [MenuItem("Tools/Write file")]
    public void WriteTo(string direction)
    {
        
        Debug.Log(music.time.ToString() + "," + direction);
        File.AppendAllText(path, music.time.ToString() + "," + direction+"\n");


    }

   


    //[MenuItem("Tools/Read file")]
    //static void ReadString()
    //{
    //    string path = "Assets/Resources/test.txt";

    //    //Read the text from directly from the test.txt file
    //    StreamReader reader = new StreamReader(path);
    //    Debug.Log(reader.ReadToEnd());
    //    reader.Close();
    //}

}
