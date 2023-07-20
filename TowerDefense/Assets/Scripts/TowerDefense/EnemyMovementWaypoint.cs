using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovementWaypoint : MonoBehaviour
{
    public GameObject[] waypoints;              //This will be assigned by the WaveManager
    public float[] distanceToWaypoints;
    public UnityEngine.AI.NavMeshAgent agent;
    public int waypointIndex = 0;
    public int health = 3;
    public float distanceToExit = 0;
    public int index = 0;
    public float speed = 3.5f;
    public int healthDecrease = 1;
    public int goldIncrease = 1;

    public GameObject fishModel;
    public GameObject sushiModel;
    public bool isEnemy;
    public bool hasSold = false;

    // Start is called before the first frame update
    void Start()
    {
        PrecalculateDistanceToWaypoints();
        isEnemy = true;
        fishModel.SetActive(true);
        sushiModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health <= 0)
        {
            if (!hasSold)
            {
                ResourceManager.IncreaseGold(goldIncrease);
                hasSold = true;
            }
            fishModel.SetActive(false);
            sushiModel.SetActive(true);
            isEnemy = false;
            gameObject.tag = "Sushi";
        }
        distanceToExit = DistanceToEnd();
    }

    public void StartMoving()
    {
        waypointIndex = 0;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = waypoints[waypointIndex].transform.position;
        agent.speed = speed;

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == "Exit")
        {
            if (isEnemy)
            {
                HealthManager.ReduceHealth(healthDecrease);
            }
            Destroy(gameObject);                        
        }
        else if (other.CompareTag("Waypoint"))
        {
            waypointIndex++;
            agent.destination = waypoints[waypointIndex].transform.position;
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("TowerBall"))
        {
            TowerBall ballScript = collision.gameObject.GetComponent<TowerBall>();
            health -= ballScript.damage;
            Destroy(collision.collider.gameObject);
                           
        }
    }
    */

    public void LaserHit()
    {
        ResourceManager.IncreaseGold(1);            //increase gold
        Destroy(gameObject);                        //delete unit - this unit only has 1 HP (1 shot kill)
    }

    //Precalculate the distanceToWaypoints array
    private void PrecalculateDistanceToWaypoints()
    {
        distanceToWaypoints = new float[waypoints.Length];

        //Ignore the first waypoint
        for (int i = 1; i < waypoints.Length; i++)
        {
            distanceToWaypoints[i] = Vector3.Distance(waypoints[i - 1].transform.position,
                                                      waypoints[i].transform.position);
        }
    }

    //Calculate the distance to the last waypoint (end of the path)
    public float DistanceToEnd()
    {
        //Distance left for current waypoint
        float distance = agent.remainingDistance;

        //Calculate distance for remaining waypoints (start after the current waypoint)
        for (int i = waypointIndex + 1; i < waypoints.Length; i++)
        {
            distance += distanceToWaypoints[i];
        }

        return distance;
    }

}
