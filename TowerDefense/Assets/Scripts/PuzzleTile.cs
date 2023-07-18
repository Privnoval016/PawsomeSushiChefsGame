using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    //Making these public so we can watch them change in the designer
    public bool active = false;                 //The tile is active when a block of the same color is touching it

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables (just in case they were changed in the designer)
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Nothing happens in Update(), it only responds to blocks touching it
    }

    //Called upon collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.name + ", " + other.name);

        //Check that the block and tile color match when an object touches the tile. 
        if (this.name == "RedTile" && other.name == "RedBlock")
        {
            //Red match, active is true
            active = true;
        }
        else if (this.name == "BlueTile" && other.name == "BlueBlock")
        {
            //Bluematch, active is true
            active = true;
        }
        else if (this.name == "GreenTile" && other.name == "GreenBlock")
        {
            //Green match, active is true
            active = true;
        }
        else if (this.name == "PurpleTile" && other.name == "PurpleBlock")
        {
            //Purple match, active is true
            active = true;
        }
    }

    //Called upon collision with another GameObject
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(this.name + ", " + other.name);

        //Check that the block and tile color match when an object stopes touching the tile. 
        if (this.name == "RedTile" && other.name == "RedBlock")
        {
            //Red match, active is false
            active = false;
        }
        else if (this.name == "BlueTile" && other.name == "BlueBlock")
        {
            //Blue match, active is false
            active = false;
        }
        else if (this.name == "GreenTile" && other.name == "GreenBlock")
        {
            //Green match, active is false
            active = false;
        }
        else if (this.name == "PurpleTile" && other.name == "PurpleBlock")
        {
            //Purple match, active is false
            active = false;
        }
    }
}
