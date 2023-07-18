using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    //Game objects that we want to access (easier than querying for them)
    public GameObject home;
    public GameObject food;
    public GameObject party;

    //The values can be edited in the designer (easy tweaking without recompling code)
    public int maxEnergy = 10000;                   //Maximum amount of energy ant can eat
    public int lowEnergy = 2500;                    //Amount of energy that causes ant to go find food
    public int energyIncrement = 100;               //How much food the ant eats each frame
    public int energyDontation = 2500;              //Ant donate's 25% of food to the queen
    public int energyConsumption = 5;               //How much energy the ant loses each frame
    public float speed = 10;                        //How fast the ant moves

    //Enumeration with all of the states being used by the Ant
    public enum AntState
    {
        Home, Food, Party
    }

    //Making these public so we can watch them change in the designer
    public AntState state = AntState.Party;         //Current state
    public int energy = 0;                          //Current energy level (amount of food)
    public bool arrived = false;                    //Reached destination yet?

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables (just in case they were changed in the designer)
        state = AntState.Party;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for errors
        if ((home == null) || (food == null) || (party == null))
        {
            Debug.Log("Error: assign home, food, and party objects in designer");
            return;
        }

        //Losing energy every frame
        energy -= energyConsumption;

        CheckGameState();
    }

    void CheckGameState()
    {
        //Check for global state changes
        if (energy < lowEnergy && state != AntState.Food)     //Ant energy running low (and already not heading towards food)? 
        {
            //Ant is hungry, find some food ASAP
            state = AntState.Food;

            //Reset arrived flag
            arrived = false;
        }

        //Prcess currents state every update frame (~30 times per second)
        switch (state)
        {
            case AntState.Home:
                Home();
                break;
            case AntState.Food:
                Food();
                break;
            case AntState.Party:
                Party();
                break;
            default:
                Debug.Log("Error: invalid state");
                break;
        }
    }

    // Used in states to move the ant forward.
    void Move()
    {
        //Update the position along the ant's forward vector, at max speed
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void Home()
    {
        if (arrived)
        {
            //Give donation to the queen
            energy -= energyDontation;

            //Duty complete, head to the party!
            state = AntState.Party;

            //Reset arrived flag
            arrived = false;            
        }
        else
        {
            //Not home, face and move towards home
            transform.LookAt(home.transform);
            Move();
        }
    }

    //Debug.DrawLine(transform.position, transform.position + transform.forward * speed, Color.red);

    void Food()
    {
        //Has the ant eaten enough?
        if (energy >= maxEnergy)
        {
            //Truncate energy (don't allow it to be above maxEnergy)
            energy = maxEnergy;

            //Ant is full, head home
            state = AntState.Home;

            //Reset arrived flag
            arrived = false;

            //Exit the method since we left the state
            return;
        }

        if (arrived)
        {
            //Eat some food
            energy += energyIncrement;
        }
        else
        {
            //Face and move towards the food
            transform.LookAt(food.transform);
            Move();
        }
    }

    void Party()
    {
        if (!arrived)
        {
            //Face and move towards the party
            transform.LookAt(party.transform);
            Move();
        }
    }

    //Called upon collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == "Home")
        {
            if (state == AntState.Home)
            {
                //Arrived at home, donate to the queen, then head to party
                arrived = true;
            }
        }
        else if (other.name == "Food")
        {
            if (state == AntState.Food)
            {
                //Arrived at food, time to eat
                arrived = true;
            }
        }
        else if (other.name == "Party")
        {
            if (state == AntState.Party)
            {
                //Arrived at party, party until hungry
                arrived = true;
            }
        }
    }
}
