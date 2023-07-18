using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject exit;
    public GameObject spawnPoint;
    public UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
        exit = GameObject.Find("Exit");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        //Doesn't use waypoints
        transform.position = spawnPoint.transform.position;
        agent.destination = exit.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == "Exit")
        {
            Spawn();
        }
    }
}
