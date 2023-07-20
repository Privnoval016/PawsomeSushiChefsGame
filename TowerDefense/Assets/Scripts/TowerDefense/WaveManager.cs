using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] waypoints = new GameObject[7];
    public GameObject[] enemyModels = new GameObject[3];
    public GameObject spawnPoint;

    public float spawnTimer = 0;
    public float spawnRate = 1.0f;
    public float waveLength = 0;
    public float currentCount = 0;
    public float waveTimer = 0;
    public float waveRate = 5.0f;
    public int waveNumber = 0;
    public float agentSpeed = 0;
    public float health = 2;
    public float damage = 1;
    public float gold = 1;
    public GameObject currentEnemy;
    public TMP_Text waveText;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");

        waveLength = 0;
        spawnTimer = 0;
        waveTimer = waveRate;
        currentCount = 0;
        waveNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCount >= waveLength)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer >= waveRate)
            {
                waveNumber++;
                switch (waveNumber)
                {
                    case 1:
                        StartWave1();
                        break;
                    case 2:
                        StartWave2();
                        break;
                    case 3:
                        StartWave3();
                        break;
                    case 4:
                        StartWave4();
                        break;
                    case 5:
                        StartWave5();
                        break;
                    default:
                        NoWaves();
                        break;
                }

                currentCount = 0;
                waveTimer = 0;
            }

            return;
        }

        if (waveNumber > 0)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                GameObject enemy = Object.Instantiate(currentEnemy, spawnPoint.transform.position, Quaternion.identity);
                EnemyMovementWaypoint enemyScript = enemy.GetComponent<EnemyMovementWaypoint>();
                enemyScript.health = (int)health;
                
                //Initialize the enemy object (i.e. give it the waypoints, start it moving)
                EnemyMovementWaypoint enemyMovementWaypoint = enemy.GetComponent<EnemyMovementWaypoint>();
                enemyMovementWaypoint.waypoints = waypoints;
                enemyMovementWaypoint.speed = agentSpeed;
                enemyMovementWaypoint.healthDecrease = (int)damage;
                enemyMovementWaypoint.goldIncrease = (int)gold;
                enemyMovementWaypoint.StartMoving();


                currentCount++;
                spawnTimer = 0;
            }
        }
    }

    void StartWave1()
    {
        Debug.Log("Starting wave 1");
        waveText.text = "1";
        currentEnemy = enemyModels[0];
        health = 2;
        gold = 1;
        damage = 1;
        waveLength = 7;
        spawnRate = 2.0f;
        agentSpeed = 2.5f;
    }

    void StartWave2()
    {
        Debug.Log("Starting wave 2");
        waveText.text = "2";
        currentEnemy = enemyModels[0];
        health = 4;
        gold = 1;
        damage = 1;
        waveLength = 15;
        spawnRate = 1.5f;
        agentSpeed = 3.5f;
    }

    void StartWave3()
    {
        Debug.Log("Starting wave 3");
        waveText.text = "3";
        currentEnemy = enemyModels[1];
        health = 8;
        gold = 2;
        damage = 1;
        waveLength = 20;
        spawnRate = 1.5f;
        agentSpeed = 3.5f;
    }

    void StartWave4()
    {
        Debug.Log("Starting wave 4");
        waveText.text = "4";
        currentEnemy = enemyModels[1];
        health = 12;
        gold = 3;
        damage = 3;
        waveLength = 15;
        spawnRate = 1.5f;
        agentSpeed = 4.5f;
    }

    void StartWave5()
    {
        Debug.Log("Starting wave 5");
        waveText.text = "5";
        currentEnemy = enemyModels[2];
        health = 300;
        gold = 3;
        damage = 10;
        waveLength = 1;
        spawnRate = 1f;
        agentSpeed = 1.0f;
    }

    void NoWaves()
    {
        Debug.Log("No more waves!");
        waveLength = 0;
        spawnRate = 10000000f;
    }
}
