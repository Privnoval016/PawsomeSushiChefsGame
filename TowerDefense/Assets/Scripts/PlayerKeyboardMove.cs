using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMove : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot
    Rigidbody rigidBody;                            //Rigidbody component used for jumping (it's physics!!)

    //Making these public so we can watch them change in the designer
    float moveSpeed = 5;                            //How fast the player moves 
    float rotationSpeed = 50;                       //How fast the player turns left/right
    float jumpForce = 400;                          //How much force to use when the player jumps
    bool isJumping = false;                         //True/false flag indicating if the player is already jumping

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for each of the movement keys to see if the player is currently pressing them.
        if (Input.GetKey(KeyCode.W))
        {
            //W - move foward along the player's forward direction.
            //Always multiply by deltaTime when moving as Update() runs 30+ frames per second.
            transform.position += moveSpeed * transform.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //A - turn left by rotating the player along the Y axis in the negavite direction
            transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            //S - move backward along the player's negative forward direction.
            transform.position += -moveSpeed * transform.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //D - turn right by rotating the player along the Y axis in the positive direction
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //It can be helpful when testing to print out debug statements to the Console window.
            //Debug.Log("space key was pressed");   //not currently using it

            //No double jumps, don't allow jumping if already jumping
            if (!isJumping)
            {
                //Set the jumping flag to prevent double jumps
                isJumping = true;

                //Apply a force to this Rigidbody in direction of this GameObjects up axis
                rigidBody.AddForce(transform.up * jumpForce);
            }
        }
    }

    //Called upon collision with another GameObject
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(other.name);

        //Check to see if the player has touched the GameOver portal
        if (collision.gameObject.name == "GameOver")
        {
            //Player touched the portal, display win message (in the future we will print message to the GUI)
            Debug.Log("Player wins!!");
        }

        ////Extras
        //if (collision.gameObject.tag == "Block")
        //{
        //    Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        //    rb.AddForce(transform.forward * 400);
        //}

        //Collision with any object lets the player jump again
        isJumping = false;
    }
}
