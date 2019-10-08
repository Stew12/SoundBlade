﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private float defaultPos_X, defaultPos_Y, defaultPos_Z;
    public float cameraSpeed = 200;
    private float newPos_X, newPos_Y, newPos_Z;
    private bool zoomedIn = false;
    private bool positiveX = false;

    public GameObject testObject;
    // Start is called before the first frame update
    void Start()
    {
        //Get default positions of camera to zoom out to later
        defaultPos_X = transform.position.x;
        defaultPos_Y = transform.position.y;
        defaultPos_Z = transform.position.z;

        //newPos_Y takes the default y axis of the camera into consideration
        newPos_Y = transform.position.y;

        //ZoomIn(testObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomedIn)
        {
            CameraZoomIn();
        }
        else
        {
            CameraZoomOut();
        }
            

    }

    /////////////////////////Call these methods in other scripts to initiate zoom in/zoom out effect/////////////////////////

    //CALL THIS METHOD from another script to have the camera zoom in on the specified player
    //Note: Depending on camera angle, the camera's view may not zoom in on the player properly. To fix this, tweak the ZoomInLevel_Y and ZoomInLevel_Z public variables
    public void ZoomIn(GameObject playerObject)
    {
        zoomedIn = true;

        newPos_Z = transform.position.z;
        newPos_Y = transform.position.y;
        newPos_X = playerObject.transform.position.x;
    }

    //Call ZoomOut from another script to have the camera return to its original position
    public void ZoomOut()
    {
        zoomedIn = false;
    }


    /////////////////////////Don't call these methods in other scripts, used to make camera move in Update()/////////////////////////
    private void CameraZoomIn()
    {

            //X Axis
            if (transform.position.x < newPos_X)
            {
                transform.position = new Vector3(transform.position.x + cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        


            //X Axis
            if (transform.position.x > newPos_X)
            {
                transform.position = new Vector3(transform.position.x - cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        
    }




    private void CameraZoomOut()
    {
        //X Axis
        if (transform.position.x < defaultPos_X)
        {
            transform.position = new Vector3(transform.position.x + cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }



        //X Axis
        if (transform.position.x > defaultPos_X)
        {
            transform.position = new Vector3(transform.position.x - cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

}

