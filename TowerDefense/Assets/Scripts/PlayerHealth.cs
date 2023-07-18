using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot
    PlayerClickMove playerClickMove;                                //Player script (for respawn)

    //Making these public so we can watch them change in the designer
    public int maxHealth = 5;                                       //Initial health of the player
    public int health = 0;                                          //Current health of the player (when it goes to 0, respawn)

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the PlayerClickMove component from the GameObject 
        playerClickMove = GetComponent<PlayerClickMove>();

        //Set health to maxHealth
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Do nothing, it's flying with physics!
    }

    //Called upon collision with another GameObject
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        //Check to see if the player has been hit by a paint ball
        if (collision.gameObject.tag == "PaintBall")
        {
            //Remove the bullet that just hit the player
            Destroy(collision.gameObject);

            //Subtract health (paintballs only do 1 damage)
            health -= 1;

            //Check to see if health dropps to 0 (or lower)
            if (health <= 0)
            {
                //Health dropped to 0, respawn the character
                playerClickMove.Respawn();

                //Set player health back to maximum
                health = maxHealth;
            }
        }
    }
}
