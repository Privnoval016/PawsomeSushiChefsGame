using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;                                //Library to change text of a TextMesh

public class PlayerHealthGUI : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot
    PlayerClickMove playerClickMove;                                //Player script (for respawn)
    TMP_Text healthText;                                            //Health text GUI component

    //Making these public so we can watch them change in the designer
    public int maxHealth = 5;                                       //Initial health of the player
    public int health = 0;                                          //Current health of the player (when it goes to 0, respawn)

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the PlayerClickMove component from the GameObject 
        playerClickMove = GetComponent<PlayerClickMove>();

        //Find the Health text GUI component
        healthText = GameObject.Find("Health").GetComponent<TMP_Text>();

        //Set health to maxHealth
        health = maxHealth;

        //Update health on GUI
        UpdateGUI();
    }

    // Update is called once per frame
    void Update()
    {
        //Do nothing, it's flying with physics!
    }

    //Update the text of the HealthText objects on the UI
    public void UpdateGUI()
    {
        //This will concatenate the string "Health: " to the value of the player’s health
        healthText.text = "Health: " + health;
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

            //Update health on GUI
            UpdateGUI();
        }
    }
}
