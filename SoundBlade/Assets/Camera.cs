using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    public Transform target; //This will be your citizen
    public float distance;


    void Update()
    {
        int DistanceAway = 10;
        Vector3 PlayerPOS = GameObject.Find("Player").transform.transform.position;
        GameObject.Find("MainCamera").transform.position = new Vector3(PlayerPOS.x, PlayerPOS.y, PlayerPOS.z - DistanceAway); transform.position = new Vector3(target.position.x, target.position.y + 25, target.position.z - distance);
    }
}