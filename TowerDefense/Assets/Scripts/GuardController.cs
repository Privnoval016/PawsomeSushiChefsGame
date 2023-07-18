using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot
    UnityEngine.AI.NavMeshAgent agent;                   //NavMeshAgent component for controlling movement

    //Game objects that we want to access (easier than querying for them)
    public GameObject[] waypoints = new GameObject[4];  //Array for the four waypoints (make sure to set in the Inspector)

    //Enumeration with all of the states being used by the Ant
    public enum GuardState
    {
        Waypoint, MoveToWaypoint, Chase, StopChase, MoveToBuilding
    }

    //The values can be edited in the designer (easy tweaking without recompling code)
    public int viewRange = 20;                          //Distance the guard can see the Player

    //Making these public so we can watch them change in the designer
    public GuardState state = GuardState.Waypoint;      //Current state
    public int currentWaypoint = 0;                     //Current waypoint guard is moving towards
    public GameObject building;                         //Building object - will fill automatically
    public GameObject target;                           //Target to chase (i.e. player)
    public bool buildingInRange;                        //Is the building in Range?
    public bool playerInRange;                          //Is the player in Range?

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables (just in case they were changed in the designer)
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //Automatically grab the building object for later (we don't have to set in Inspector)
        building = GameObject.Find("Building");

        //Set initial state
        state = GuardState.Waypoint;

        //Start waypoint at -1 so the first time the guard moves is towards Waypoint1
        currentWaypoint = -1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();
    }

    void CheckGameState()
    {
        //Going to check this multiple times, let's only call it once per frame
        buildingInRange = BuildingInRange();
        playerInRange = PlayerInRange();

        //Check for global state changes
        if (!buildingInRange && state != GuardState.MoveToBuilding)
        {
            //Buiding is not in range and we are not heading to a waypoint
            state = GuardState.StopChase;
        }
        else if (buildingInRange && playerInRange)
        {
            //Player and building are in range - let the chase begin!
            state = GuardState.Chase;
        }

        //Prcess currents state every update frame (~30 times per second)
        switch (state)
        {
            case GuardState.StopChase:
                StopChase();
                break;
            case GuardState.Waypoint:
                Waypoint();
                break;
            case GuardState.MoveToBuilding:
                MoveToBuilding();
                break;
            case GuardState.MoveToWaypoint:
                MoveToWaypoint();
                break;
            case GuardState.Chase:
                ChasePlayer();
                break;
            default:
                Debug.Log("Error: invalid state");
                break;
        }
    }

    public void Waypoint()
    {
        //Increment the waypoint index
        currentWaypoint += 1;

        //If we increment the waypoint beyond the lenght of the array, start back at the beginning
        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }

        //Set Guard's destination
        agent.SetDestination(waypoints[currentWaypoint].transform.position);

        //Set to the moving state (to waypoint)
        state = GuardState.MoveToWaypoint;
    }

    public void MoveToWaypoint()
    {
        //nothing to do, NavMesh is handling movement
    }

    public void ChasePlayer()
    {
        //Check to see if there is a target
        if (target != null)
        {
            //target fouhd, move towards it
            agent.SetDestination(target.transform.position);
        }
        else
        {
            //No player in range to chase, back to waypoints
            state = GuardState.Waypoint;

            //we could make this better by going to the closest waypoint,
            //instead of going to the next one - which could be on the other side
            //of the building
        }
    }

    public void StopChase()
    {
        //Too far!! Head directly back to the building!!
        agent.SetDestination(building.transform.position);

        //Set to the moving state (to building)
        state = GuardState.MoveToBuilding;
    }
    
    public void MoveToBuilding()
    {
        //If building is in range, we can stop running towards it
        if (buildingInRange)
        {
            //Close enough, back to the waypoints
            state = GuardState.Waypoint;
        }
    }

    //Check to see if the building is in range
    public bool BuildingInRange()
    {
        //Calculate the difference to the building from our current position
        Vector3 difference = building.transform.position - transform.position; 
        
        //Comparing the squared distances (faster than computing square root)
        return difference.sqrMagnitude < (viewRange * viewRange);
    }

    //Check to see if a player is in range
    public bool PlayerInRange()
    {
        //Find the closest player, works fine with one player, works better when multiple playes
        target = FindClosestTarget("Player", viewRange);

        //If target is not null, then player was found in range. null means no player found in range
        return target != null;
    }

    //Find the closest target that has the tag and is closer than maxDistance
    public GameObject FindClosestTarget(string tag, float maxDistance)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);  //Fill array with all gameobjects with the tag
        GameObject closest = null;                          //result: starts at null just in case we don't find anything in range

        float distance = maxDistance * maxDistance;         //square the distance
        Vector3 position = transform.position;              //our current position
        
        foreach (GameObject obj in gameObjects)
        {
            Vector3 difference = obj.transform.position - position; //calculate the difference to the object from our current position
            float curDistance = difference.sqrMagnitude;    //distance requires a square root, which is slow, just using the squared magnitued

            if (curDistance < distance)                     //comparing the squared distances
            {
                closest = obj;                              //new closest object set
                distance = curDistance;                     //distance to the object saved for next comparison in loop
            }
        }

        //Return the obejct we found, or null if we didn't find anything
        return closest;
    }

    //Called upon collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.tag == "Waypoint")
        {
            //We hit the waypoint, if we were moving to waypoint, then let's move to the next one
            if (state == GuardState.MoveToWaypoint)
            {
                //Set waypoint state to move to next waypoint
                state = GuardState.Waypoint;
            }
        }
    }
}
