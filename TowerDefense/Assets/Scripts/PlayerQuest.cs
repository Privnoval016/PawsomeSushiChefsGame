using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    //Making these public so we can watch them change in the designer
    public GameObject wall;                                 //Wall that we want to lower when the quest is completed
    public Vector3 hiddenPosition;                          //Calculated hidden position of the wall (not visible)
    public bool redBlock = false;                           //Flag indicating if the red block has been picked up
    public bool greenBlock = false;                         //Flag indicating if the green block has been picked up
    public bool blueBlock = false;                          //Flag indicating if the blue block has been picked up
    public bool purpleBlock = false;                        //Flag indicating if the purple block has been picked up
    public ExitQuestState state = ExitQuestState.None;      //Current state

    //Enumeration with all of the states being used for the exit quest
    public enum ExitQuestState
    {
        None, RedBlock, GreenBlock, BlueBlock, PurpleBlock, Completed
    }

    //The values can be edited in the designer (easy tweaking without recompling code)
    public float wallSpeed = 1.0f;                  //How fast the wall lowers

    // Start is called before the first frame update
    void Start()
    {
        //Set current state to None (not assigned)
        state = ExitQuestState.None;

        //Get the wall game object in the scene
        wall = GameObject.Find("Wall");

        //Calculate the wall's hidden position (current position, but subtract 10.5 from the Y axis, which is below ground)
        hiddenPosition = wall.transform.position - new Vector3(0, 10.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Only lower the wall if the quest has been completed
        if (state == ExitQuestState.Completed)
        {
            //Lower the wall
            float step = wallSpeed * Time.deltaTime; // calculate distance to move (wall speed times deltaTime as usual)
            wall.transform.position = Vector3.MoveTowards(wall.transform.position, hiddenPosition, step);
        }
    }

    //Check to see what state the exit quest is in and call the appropriate method
    public void CheckExitQuest()
    {
        switch (state)
        {
            case ExitQuestState.None:
                NoneExitQuest();
                break;
            case ExitQuestState.RedBlock:
                RedBlockExitQuest();
                break;
            case ExitQuestState.GreenBlock:
                GreenBlockExitQuest();
                break;
            case ExitQuestState.BlueBlock:
                BlueBlockExitQuest();
                break;
            case ExitQuestState.PurpleBlock:
                PurpleBlockExitQuest();
                break;
            case ExitQuestState.Completed:
                CompletedExitQuest();
                break;
        }
    }

    //Currently in the None state, start the quest
    public void NoneExitQuest()
    {
        Debug.Log("Find a red block and bring it back to me.");
        state = ExitQuestState.RedBlock;
    }

    //Quest is done, nothing more to do
    public void CompletedExitQuest()
    {
        Debug.Log("You have already completed my quest.");
    }

    //Looking for the red block state
    public void RedBlockExitQuest()
    {
        //Does the player have the red block?
        if (redBlock)
        {
            //Yes, move to next part of quest
            Debug.Log("Thanks for returning the red block.");
            Debug.Log("Find a green block and bring it back to me.");
            state = ExitQuestState.GreenBlock;
        }
        else
        {
            //Nope, remind player what's going on
            Debug.Log("You haven't found my red block yet.");
        }
    }

    //Looking for the green block state
    public void GreenBlockExitQuest()
    {
        //Does the player have the green block?
        if (greenBlock)
        {
            //Yes, move to next part of quest
            Debug.Log("Thanks for returning the green block.");
            Debug.Log("Find a blue block and bring it back to me.");
            state = ExitQuestState.BlueBlock;
        }
        else
        {
            //Nope, remind player what's going on
            Debug.Log("You haven't found my green block yet.");
        }
    }

    //Looking for the blue block state
    public void BlueBlockExitQuest()
    {
        //Does the player have the blue block?
        if (blueBlock)
        {
            //Yes, move to next part of quest
            Debug.Log("Thanks for returning the blue block.");
            Debug.Log("Find a purple block and bring it back to me.");
            state = ExitQuestState.PurpleBlock;
        }
        else
        {
            //Nope, remind player what's going on
            Debug.Log("You haven't found my blue block yet.");
        }
    }

    //Looking for the purple block state
    public void PurpleBlockExitQuest()
    {
        //Does the player have the purple block?
        if (purpleBlock)
        {
            //Yes, move to next part of quest
            Debug.Log("Thanks for returning the purple block.");
            Debug.Log("I have opened the way for you. Goodbye!");
            state = ExitQuestState.Completed;
        }
        else
        {
            //Nope, remind player what's going on
            Debug.Log("You haven't found my purple block yet.");
        }
    }

    //Player needs to pick up the red block
    public void PickUpRedBlock(GameObject other)
    {
        //In the red block state?
        if (other.name == "RedBlock")
        {
            //Yes, pick up block
            Debug.Log("Picking up red block");

            //Pick up red block
            redBlock = true;

            //Remove block
            Destroy(other);
        }
        else
        {
            //Nope, remind player (we could do nothing here also)
            Debug.Log("Looking for red block");
        }
    }

    //Player needs to pick up the green block
    public void PickUpGreenBlock(GameObject other)
    {
        //In the green block state?
        if (other.name == "GreenBlock")
        {
            //Yes, pick up block
            Debug.Log("Picking up green block");

            //Pick up green block
            greenBlock = true;

            //Remove block
            Destroy(other);
        }
        else
        {
            //Nope, remind player (we could do nothing here also)
            Debug.Log("Looking for green block");
        }
    }

    //Player needs to pick up the blue block
    public void PickUpBlueBlock(GameObject other)
    {
        //In the blue block state?
        if (other.name == "BlueBlock")
        {
            //Yes, pick up block
            Debug.Log("Picking up blue block");

            //Pick up blue block
            blueBlock = true;

            //Remove block
            Destroy(other);
        }
        else
        {
            //Nope, remind player (we could do nothing here also)
            Debug.Log("Looking for blue block");
        }
    }

    //Player needs to pick up the purple block
    public void PickUpPurpleBlock(GameObject other)
    {
        //In the purple block state?
        if (other.name == "PurpleBlock")
        {
            //Yes, pick up block
            Debug.Log("Picking up purple block");

            //Pick up purple block
            purpleBlock = true;

            //Remove block
            Destroy(other);
        }
        else
        {
            //Nope, remind player (we could do nothing here also)
            Debug.Log("Looking for purple block");
        }
    }

    //Player is trying to pick up a block
    public void PickUpBlock(GameObject other)
    {
        //If the player isn't in any of these states, then do nothing. The player will
        //probably push the block around. We could turn off physics on the blocks and turn
        //them into triggers if we don’t want objects to move.
        switch (state)
        {
            case ExitQuestState.RedBlock:
                PickUpRedBlock(other);
                break;
            case ExitQuestState.GreenBlock:
                PickUpGreenBlock(other);
                break;
            case ExitQuestState.BlueBlock:
                PickUpBlueBlock(other);
                break;
            case ExitQuestState.PurpleBlock:
                PickUpPurpleBlock(other);
                break;
        }
    }

    //Called upon collision with another GameObject (with trigger)
    private void OnTriggerEnter(Collider other)
    {
        //Check to see if player entered the exit quest trigger
        if (other.name == "ExitQuestTrigger")
        {
            CheckExitQuest();
        }
    }

    //Called upon collision with another GameObject (no trigger)
    private void OnCollisionEnter(Collision collision)
    {
        //Check to see if player ran into one of the blocks
        if (collision.gameObject.tag == "Block")
        {
            PickUpBlock(collision.gameObject);
        }
    }
}
