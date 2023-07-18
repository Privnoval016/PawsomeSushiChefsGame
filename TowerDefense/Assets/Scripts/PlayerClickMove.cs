using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickMove : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot
    UnityEngine.AI.NavMeshAgent agent;                          //NavMeshAgent component for controlling movement

    //Making these public so we can watch them change in the designer
    public Vector3 spawnPoint;                                  //Player's spawn point

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();    //Get NavMeshAgent component and save for later
        spawnPoint = transform.position;                        //Save player's spawn point (current position)
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if the player is holding down the left mouse button (button 0)
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            //Check to see if raycast from camera at the current mouse position hits the terrain
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //hit.point is where the raycast hit the terrain, move to that location
                agent.SetDestination(hit.point);
            }
        }
    }

    public void Respawn()
    {
        //Send player to spawn point
        transform.position = spawnPoint;

        //End movement
        agent.ResetPath();
    }

    //Called upon collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == "Building")
        {
            //Send message to console (future labs, we will display messages to UI)
            Debug.Log("Player touched building");
        }
        else if (other.tag == "Guard")
        {
            //Send message to console (future labs, we will display messages to UI)
            Debug.Log("Guard hit player!!");

            Respawn();
        }
    }
}
