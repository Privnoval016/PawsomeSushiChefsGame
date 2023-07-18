using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementWaypoint : MonoBehaviour
{
    public GameObject[] waypoints;              //This will be assigned by the WaveManager
    public UnityEngine.AI.NavMeshAgent agent;
    public int waypointIndex = 0;
    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ResourceManager.IncreaseGold(3);
            Destroy(gameObject);
        }
    }

    public void StartMoving()
    {
        waypointIndex = 0;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = waypoints[waypointIndex].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == "Exit")
        {
            HealthManager.ReduceHealth(1);              //Reduce health due to hit - this unit only causes one damage
            Destroy(gameObject);                        //delete unit
        }
        else if (other.CompareTag("Waypoint"))
        {
            waypointIndex++;
            agent.destination = waypoints[waypointIndex].transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("TowerBall"))
        {
            health--;
            Destroy(collision.collider.gameObject);
                           
        }
    }

    public void LaserHit()
    {
        ResourceManager.IncreaseGold(1);            //increase gold
        Destroy(gameObject);                        //delete unit - this unit only has 1 HP (1 shot kill)
    }
}
