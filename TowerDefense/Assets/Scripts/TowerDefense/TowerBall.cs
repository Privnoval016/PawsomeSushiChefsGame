using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBall : MonoBehaviour
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
        //Debug.Log(collision.gameObject.name);

        //Check to see if the enemy has been hit by a tower ball (enemy will delete any balls that hit it)
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            //Enemy was not hit, remove the tower ball from the scene whenever it touches any other game object
            Destroy(gameObject);
        }
    }
}
