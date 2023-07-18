using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);

        //Check to see if the player has been hit by a paint ball (player will delete any balls that hit it)
        if (collision.gameObject.tag != "Player")
        {
            //Player was not hit, remove the paintball from the scene whenever it touches any other game object
            Destroy(gameObject);
        }
    }
}
