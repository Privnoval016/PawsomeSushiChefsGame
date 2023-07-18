using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementLooper : MonoBehaviour
{
    public GameObject[] waypoints = new GameObject[5];
    public GameObject spawnPoint;
    public UnityEngine.AI.NavMeshAgent agent;

    public int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        waypointIndex = 0;
        transform.position = spawnPoint.transform.position;
        agent.destination = waypoints[waypointIndex].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == "Exit")
        {
            //Constantly loops back to beginning - doens't die
            HealthManager.ReduceHealth(1);
            Spawn();
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
            //Constantly loops back to beginning - doens't die
            Destroy(collision.collider.gameObject);
            ResourceManager.IncreaseGold(1);
            Spawn();
        }
    }
}
