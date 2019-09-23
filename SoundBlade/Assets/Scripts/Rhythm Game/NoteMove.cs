using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour {
    //public string dir = "DOWN";
    public SpriteRenderer thisSprite;
    public float speed = 1.0f;


    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
