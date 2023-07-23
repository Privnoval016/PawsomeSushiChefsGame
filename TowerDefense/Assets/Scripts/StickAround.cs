using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("StickAround");

        //Only allow one active at a time (delete if already alive)
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        //Object sticks around as scenes switch.
        //This is how we can have data carry over to new levels.
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
