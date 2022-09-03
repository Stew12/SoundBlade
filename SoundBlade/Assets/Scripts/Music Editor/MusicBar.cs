using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MusicBar : MonoBehaviour
{
    

    public AudioSource music;
    public GameObject startBar;

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
        startingPos = startBar.transform.position;
        transform.position = startingPos;
        path = "C:\\Users\\Lachlan\\Documents\\SoundBlade\\Assets\\Scripts\\Song1.txt";
        songLength = movementSpeed;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x >= startBar.transform.position.x)
            {
                transform.position = new Vector3(Input.mousePosition.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(startBar.transform.position.x, transform.position.y, transform.position.z);
            }

            distance = transform.position.x - startBar.transform.position.x;
            music.time = distance/songLength;
        }

        if (Input.GetKeyDown("space")) 
        {
            Debug.Log("p");
            if (!paused)
            {
                music.Pause();
                paused = true;

                distance = transform.position.x - startBar.transform.position.x;
                music.time = distance/songLength;
            }
            else
            {
                music.Play();
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

        if (!paused)
        {
            transform.position += Vector3.right * (movementSpeed * Time.deltaTime);
        }
        
        
        
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
