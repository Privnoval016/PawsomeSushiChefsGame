using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    //Making these public so we can watch them change in the designer
    public PuzzleTile[] tiles = new PuzzleTile[4];  //Array for the four PuzzleTiles (make sure to set in the Inspector)
    public Vector3 startingPosition;                //Starting position of the wall
    public Vector3 hiddenPosition;                  //Calculated hidden position of the wall (not visible)

    //The values can be edited in the designer (easy tweaking without recompling code)
    public float wallSpeed = 1.0f;                  //How fast the wall lowers (or raises)

    // Start is called before the first frame update
    void Start()
    {
        //Save wall's starting position (current position)
        startingPosition = transform.position;

        //Calculate the wall's hidden position (current position, but subtract 10.5 from the Y axis, which is below ground)
        hiddenPosition = transform.position - new Vector3(0, 10.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //The udpate method for the wall is going to check all the tiles to see if they are active. 
        //If they are all active, then it will lower the wall, otherwise it will raise the wall. 
        //If the wall is already in the correct position, then it won't move.

        bool allActive = true;              //allActive starts out at true (because if it started at false, it will never go true)
        Vector3 targetPosition;             //The targetPosition local variable will be set to either raise or lower the wall

        //Check each tile in the array. This is called a foreach loop, for each element in tiles, assign it to the variable tile.
        foreach (PuzzleTile tile in tiles)
        {
            //&= is using AND which means if both are true, then true is assigned to allActive,
            //but if either is false, then false is assigned to allActive.
            //In other words, allActive will only be true if all the tiles are active,
            //otherwise it will be false if any tile is not active.
            allActive &= tile.active;
        }

        //Check to see if all tiles are active
        if (allActive)
        {
            //All tiles are active, therefore lower the wall (slowly)
            //by setting the targetPosition to the hidden positon.
            targetPosition = hiddenPosition;
        }
        else
        {
            //At least one of the tiles is not active, therefore raise the wall (slowly)
            //by setting the targetPosition to the starting positon.
            targetPosition = startingPosition;
        }

        float step = wallSpeed * Time.deltaTime; // calculate distance to move (wall speed times deltaTime as usual)

        //Update the position of the wall. MoveTowards() will move the wall towards the targetPosition
        //a little bit at a time, based on the step. If the wall is already at the targetPosition, nothing will happen.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
