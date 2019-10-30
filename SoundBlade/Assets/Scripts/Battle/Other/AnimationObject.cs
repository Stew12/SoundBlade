using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    public float animTime = 5f;

    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        animTime -= Time.time;
        if (animTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
