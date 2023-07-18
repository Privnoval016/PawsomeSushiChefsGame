using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TowerTurretLaser : MonoBehaviour
{
    public GameObject prefabPaintball;
    public GameObject muzzle;
    public GameObject target;
    public int attackRange = 5;
    public float fireRate = 2f;
    public float ballSpeed = 2000f;
    public float lastFire = 0f;
    public float pauseMovement = 0.2f;  //to allow the laser time to fade

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

        //Find nearest enemy
        //TODO: find target in range that is closest to end of map
        target = FindClosestTarget("Enemy", attackRange);

        if (target != null && lastFire >= pauseMovement)
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
                //It's okay to Fire paintball
                GameObject laser = Object.Instantiate(prefabPaintball, muzzle.transform.position, Quaternion.identity);
                LineRenderer line = laser.GetComponent<LineRenderer>();

                RaycastHit hit;
                if (Physics.Raycast(muzzle.transform.position, transform.forward, out hit))
                {
                    if (hit.collider && hit.collider.CompareTag("Enemy"))
                    {
                        line.SetPosition(0, muzzle.transform.position);
                        line.SetPosition(1, hit.point);
                        EnemyMovementWaypoint enemy = hit.collider.gameObject.GetComponent<EnemyMovementWaypoint>();
                        enemy.LaserHit();
                    }
                }

                //Reset lastFire
                lastFire = 0;
            }
        }
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
}
