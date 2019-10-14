using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject playerUI;
    // Start is called before the first frame update
    public void DamagePlayer()
    {
        playerUI.GetComponent<PlayerValues>().DamageFlag();
    }
}
