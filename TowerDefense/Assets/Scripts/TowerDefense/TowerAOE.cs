using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TowerAOE : MonoBehaviour
{
    public GameObject aOEEffect;
    public GameObject prefabPaintball;
    public GameObject muzzle;
    public GameObject target;
    public int attackRange = 10;
    public float fireRate = 4f;
    public float range = 1;
    public float ballSpeed = 2000f;
    public float lastFire = 0f;
    public float damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        Transform barrel = transform.Find("Barrel");
        muzzle = barrel.Find("Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        lastFire += Time.deltaTime;
        target = FindClosestDistance("Enemy", attackRange);

        if (target != null)
        {
            //Look at it
            transform.LookAt(target.transform);

            ////Lead target
            //NavMeshAgent agent = target.GetComponent<NavMeshAgent>();
            //Debug.Log(agent.velocity);

            //Fire at it
            //Check to see if it's okay to fire a paintball, or if the guard needs to wait
            if (lastFire >= fireRate)
            {
                
                StartCoroutine(shoot());
                //It's okay to Fire paintball
                

                //Reset lastFire
                lastFire = 0;

            }
        }
    }

    IEnumerator shoot()
    {
        GameObject ball = Object.Instantiate(prefabPaintball, muzzle.transform.position, Quaternion.identity);

        //Launch ball
        Rigidbody rigidBody = ball.GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.forward * ballSpeed);

        yield return new WaitForSeconds(0.1f);


        GameObject aOE = Object.Instantiate(aOEEffect, 
            new Vector3(target.transform.position.x, target.transform.position.y - 1.5f, target.transform.position.z), 
            Quaternion.identity);
        Destroy(ball);
        aOE.transform.localScale = new Vector3(range, aOE.transform.localScale.y, range);
        aOE.GetComponent<AOEProjectile>().damage = (int)damage;
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

    public GameObject FindClosestDistance(string tag, float maxDistance)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);  //Fill array with all gameobjects with the tag
        GameObject closest = null;                          //result: starts at null just in case we don't find anything in range

        float sqrDistance = maxDistance * maxDistance;      //square the distance
        float distance = Mathf.Infinity;                    //make this super large                       
        Vector3 position = transform.position;              //our current position

        foreach (GameObject obj in gameObjects)
        {
            Vector3 difference = obj.transform.position - position; //calculate the difference to the object from our current position
            float curDistance = difference.sqrMagnitude;    //distance requires a square root, which is slow, just using the squared magnitude

            if (curDistance < sqrDistance)                     //comparing the squared distances
            {
                //Object is in range, now check to see if this object is closer to the exit
                EnemyMovementWaypoint enemy = obj.GetComponent<EnemyMovementWaypoint>();
                float distanceToExit = enemy.distanceToExit;
                //enemy.DistanceToEnd();    //(slow and can crash if object is killed)

                if (distanceToExit < distance)
                {
                    closest = obj;                              //new closest object set
                    distance = distanceToExit;        //distance to the exit saved for next comparison in loop
                }
            }
        }

        //Return the object we found, or null if we didn't find anything
        return closest;
    }

}
