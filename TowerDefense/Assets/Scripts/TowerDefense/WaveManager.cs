using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject waveUI;
    public GameObject menuButtons;
    public GameObject nextButton;
    public GameObject endText;
    public GameObject winImage;
    public bool isEnd = false;
    public float agentAccel = 8;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
        waveUI = GameObject.Find("WaveUI");

        waveLength = 0;
        spawnTimer = 0;
        waveTimer = waveRate;
        currentCount = 0;
        waveNumber = 0;

        menuButtons.SetActive(false);
        nextButton.SetActive(false);
        endText.SetActive(false);
        winImage.SetActive(false);
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

                enemyScript.isEnd = isEnd;
                enemyScript.waveManager = gameObject.GetComponent<WaveManager>();
                
                //Initialize the enemy object (i.e. give it the waypoints, start it moving)
                EnemyMovementWaypoint enemyMovementWaypoint = enemy.GetComponent<EnemyMovementWaypoint>();
                enemyMovementWaypoint.waypoints = waypoints;
                enemyMovementWaypoint.speed = agentSpeed;
                enemyMovementWaypoint.accel = agentAccel;
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
        waveUI.GetComponent<RawImage>().color = new Color(0.961f, 0.314f, 0, 1);
        currentEnemy = enemyModels[0];
        health = 3;
        gold = 2;
        damage = 1;
        waveLength = 8;
        spawnRate = 2.0f;
        isEnd = false;
        agentSpeed = 3f;
        agentAccel = 8;
    }

    void StartWave2()
    {
        Debug.Log("Starting wave 2");
        waveText.text = "2";
        waveUI.GetComponent<RawImage>().color = new Color(0.961f, 0.314f, 0, 1);
        currentEnemy = enemyModels[0];
        health = 8;
        gold = 3;
        damage = 1;
        waveLength = 15;
        spawnRate = 1.5f;
        isEnd = false;
        agentSpeed = 3.5f;
        agentAccel = 8;
    }

    void StartWave3()
    {
        Debug.Log("Starting wave 3");
        waveText.text = "3";
        waveUI.GetComponent<RawImage>().color = new Color(0.165f, 0.404f, 0.737f, 1);
        currentEnemy = enemyModels[1];
        health = 14;
        gold = 5;
        damage = 1;
        waveLength = 20;
        spawnRate = 2f;
        isEnd = false;
        agentSpeed = 3.5f;
        agentAccel = 8;
    }

    void StartWave4()
    {
        Debug.Log("Starting wave 4");
        waveText.text = "4";
        waveUI.GetComponent<RawImage>().color = new Color(0.165f, 0.404f, 0.737f, 1);
        currentEnemy = enemyModels[1];
        health = 24;
        gold = 5;
        damage = 3;
        waveLength = 15;
        spawnRate = 1.5f;
        isEnd = false;
        agentSpeed = 4.5f;
        agentAccel = 8;
    }

    void StartWave5()
    {
        Debug.Log("Starting wave 5");
        waveText.text = "5";
        waveUI.GetComponent<RawImage>().color = new Color(0.804f, 0.580f, 0.192f, 1);
        currentEnemy = enemyModels[2];
        health = 300;
        gold = 100;
        damage = 10;
        waveLength = 1;
        spawnRate = 1f;
        isEnd = true;
        agentSpeed = 3.0f;
        agentAccel = 3;

    }

    void NoWaves()
    {
        Debug.Log("No more waves!");
        waveLength = 0;
        spawnRate = 10000000f;
    }

    public void endGame()
    {
        Debug.Log("yahoo");
        menuButtons.SetActive(true);
        nextButton.SetActive(true);
        endText.SetActive(true);
        endText.GetComponent<TMP_Text>().text = "You Win!";
        winImage.SetActive(true) ;


    }
}
