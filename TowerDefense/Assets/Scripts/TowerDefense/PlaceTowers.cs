using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTowers : MonoBehaviour
{
    public GameObject canvas;
    public GameObject buildTowerPanel;
    public GameObject towerEffect;
    RectTransform canvasRect;
    RectTransform panelRect;
    TowerSpawn towerSpawn;
    public GameObject selectedTower;
    public GameObject[] towers = new GameObject[3];
    public Button[] buttons = new Button[3];
    public GameObject[] upgradeMenus = new GameObject[3];
    public GameObject upgradeMenu;
    public GameObject upgradeTower;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        buildTowerPanel = GameObject.Find("BuildTowerPanel");

        canvasRect = canvas.GetComponent<RectTransform>();
        panelRect = buildTowerPanel.GetComponent<RectTransform>();

        buildTowerPanel.SetActive(false);

        hideMenus();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = new Color(0.953f, 0.941f, 0.843f, 1);
        }


        buttons[0].GetComponent<Image>().color = new Color(1, 0.749f, 0.525f, 1);
    }

    // Update is called once per frame
    void Update()
    {
       


        //Check to see if the player is holding down the left mouse button (button 0)
        if (Input.GetMouseButtonDown(0))
        {
           

            //If the player clicks, disable the panel (in case they clicked off the build menu)
          


            RaycastHit hit;

            //Check to see if raycast from camera at the current mouse position hits the terrain
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {

                if (hit.collider.gameObject.CompareTag("TowerSpawn"))
                {
                    //Check to see if tower was alread created on spawn
                    towerSpawn = hit.collider.gameObject.GetComponent<TowerSpawn>();
                    Debug.Log("hit");
                    if (towerSpawn.tower == null)
                    {
                        hideMenus();

                        /*
                        buildTowerPanel.SetActive(true);

                        //Calculate the position of the UI element. 0,0 for the canvas is at the center of the screen,
                        //whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need
                        //to subtract the height / width of the canvas * 0.5 to get the correct position.
                        Vector2 panelPosition = Camera.main.WorldToViewportPoint(towerSpawn.transform.position);

                        Vector2 worldObjectScreenPosition = new Vector2(
                            ((panelPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                            ((panelPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

                        panelRect.anchoredPosition = worldObjectScreenPosition;
                        */

                       

                        if (index == 0)
                        {
                            TurretInfo towerStats = selectedTower.GetComponent<TurretInfo>();
                            if (towerStats.level <= 0 && ResourceManager.gold >= towerStats.costs[0])
                            {
                                ResourceManager.gold -= towerStats.costs[0];
                                GameObject tower = GameObject.Instantiate(selectedTower,
                                    new Vector3(towerSpawn.transform.position.x, towerSpawn.transform.position.y + 2,
                                    towerSpawn.transform.position.z), towerSpawn.transform.rotation);

                                towerSpawn.tower = tower;

  
                                TurretInfo currentTower = tower.GetComponent<TurretInfo>();
                                currentTower.type = 0;
                                currentTower.level = 1;

                                GameObject effect = GameObject.Instantiate(towerEffect,
                                new Vector3(towerSpawn.transform.position.x, towerSpawn.transform.position.y + 1.2f,
                                        towerSpawn.transform.position.z), Quaternion.Euler(90, 0, 0));
                                Destroy(effect, 0.1f);

                            }
                          
                        }

                        else if (index == 1)
                        {
                            AOEInfo towerStats = selectedTower.GetComponent<AOEInfo>();
                            if (towerStats.level <= 0 && ResourceManager.gold >= towerStats.costs[0])
                            {
                                ResourceManager.gold -= towerStats.costs[0];
                                GameObject tower = GameObject.Instantiate(selectedTower,
                                    new Vector3(towerSpawn.transform.position.x, towerSpawn.transform.position.y + 2,
                                    towerSpawn.transform.position.z), towerSpawn.transform.rotation);

                                towerSpawn.tower = tower;


                                AOEInfo currentTower = tower.GetComponent<AOEInfo>();
                                currentTower.type = 0;
                                currentTower.level = 1;

                                GameObject effect = GameObject.Instantiate(towerEffect,
                                new Vector3(towerSpawn.transform.position.x, towerSpawn.transform.position.y + 1.2f,
                                        towerSpawn.transform.position.z), Quaternion.Euler(90, 0, 0));
                                Destroy(effect, 0.1f);

                            }

                        }

                        
                    }
                }
                else if (hit.collider.gameObject.CompareTag("TurretTower"))
                {
                    upgradeTower = hit.collider.gameObject;
                    
                    TurretInfo upgradeScript = upgradeTower.GetComponent<TurretInfo>();
                    upgradeMenu = upgradeMenus[0];
                    if (upgradeScript.level < 4)
                    {
                        upgradeMenu.GetComponentInChildren<TMP_Text>().text =
                            "Knife Cat\r\nLevel: " + upgradeScript.level + "\r\nFire Rate ↑\r\nDamage ↑\r\n$" +
                            upgradeScript.costs[upgradeScript.level];
                    }
                    else
                    {
                        upgradeMenu.GetComponentInChildren<TMP_Text>().text =
                            "Knife Cat\r\nLevel: " + upgradeScript.level + "\r\n\r\nMax Level\r\nReached";
                    }

                    upgradeMenu.SetActive(true);

                    Vector2 panelPosition = Camera.main.WorldToViewportPoint(upgradeTower.GetComponent<Transform>().position);

                    Vector2 worldObjectScreenPosition = new Vector2(
                        ((panelPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                        ((panelPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

                    upgradeMenu.GetComponent<RectTransform>().anchoredPosition = worldObjectScreenPosition;
                }
                else if (hit.collider.gameObject.CompareTag("AOETower"))
                {
                    upgradeTower = hit.collider.gameObject;

                    AOEInfo upgradeScript = upgradeTower.GetComponent<AOEInfo>();
                    upgradeMenu = upgradeMenus[1];
                    if (upgradeScript.level < 4)
                    {
                        upgradeMenu.GetComponentInChildren<TMP_Text>().text =
                            "Wasabi\r\nBomb\r\nLevel: " + upgradeScript.level + "\r\nRadius ↑\r\nDamage ↑\r\n$" +
                            upgradeScript.costs[upgradeScript.level];
                    }
                    else
                    {
                        upgradeMenu.GetComponentInChildren<TMP_Text>().text =
                            "Wasabi\r\nBomb\r\nLevel: " + upgradeScript.level + "\r\n\r\nMax Level\r\nReached";
                    }

                    upgradeMenu.SetActive(true);

                    Vector2 panelPosition = Camera.main.WorldToViewportPoint(upgradeTower.GetComponent<Transform>().position);

                    Vector2 worldObjectScreenPosition = new Vector2(
                        ((panelPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                        ((panelPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

                    upgradeMenu.GetComponent<RectTransform>().anchoredPosition = worldObjectScreenPosition;
                }
            }
        }
    }

    public void changeSelectedTower(int a)
    {
        index = a;
        selectedTower = towers[a];
        upgradeMenu = upgradeMenus[a];

        
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].GetComponent<Image>().color = new Color(0.953f, 0.941f, 0.843f, 1);
        }

        buttons[a].GetComponent<Image>().color = new Color(1, 0.749f, 0.525f, 1);

        hideMenus();




    }

    public void upgradeTurretTower()
    {
        Debug.Log("upgrade");
        TurretInfo towerScript = upgradeTower.GetComponent<TurretInfo>();
        if (towerScript.level < 4 && ResourceManager.gold >= towerScript.costs[towerScript.level])
        {
            ResourceManager.gold -= towerScript.costs[towerScript.level];
            
            towerScript.turretScript.fireRate = towerScript.fireRates[towerScript.level];
            towerScript.turretScript.damage = towerScript.damages[towerScript.level];

            towerScript.level++;
            Debug.Log(towerScript.level);

            if (towerScript.level < 4)
            {
                upgradeMenu.GetComponentInChildren<TMP_Text>().text =
                    "Knife Cat\r\nLevel: " + towerScript.level + "\r\nFire Rate ↑\r\nDamage ↑\r\n$" +
                    towerScript.costs[towerScript.level];
            }
            else
            {
                upgradeMenu.GetComponentInChildren<TMP_Text>().text =
                    "Knife Cat\r\nLevel: " + towerScript.level + "\r\n\r\nMax Level\r\nReached";
            }

            GameObject effect = GameObject.Instantiate(towerEffect,
                            new Vector3(upgradeTower.transform.position.x, upgradeTower.transform.position.y - 0.8f,
                                        upgradeTower.transform.position.z), Quaternion.Euler(90, 0, 0));
            Destroy(effect, 0.1f);

            hideMenus();
        }
    }

    public void sellTurretTower()
    {
        Debug.Log("sell");
        TurretInfo towerScript = upgradeTower.GetComponent<TurretInfo>();
        ResourceManager.gold += towerScript.costs[towerScript.level - 1];
        Destroy(upgradeTower);

        hideMenus();
    }

    public void upgradeAOETower()
    {
        Debug.Log("upgrade");
        AOEInfo towerScript = upgradeTower.GetComponent<AOEInfo>();
        upgradeMenu = upgradeMenus[1];
        if (towerScript.level < 4 && ResourceManager.gold >= towerScript.costs[towerScript.level])
        {
            ResourceManager.gold -= towerScript.costs[towerScript.level];

            towerScript.launcherScript.range = towerScript.ranges[towerScript.level];
            towerScript.launcherScript.damage = towerScript.damages[towerScript.level];

            towerScript.level++;
            Debug.Log(towerScript.level);

            if (towerScript.level < 4)
            {
                upgradeMenu.GetComponentInChildren<TMP_Text>().text =
                    "Wasabi\r\nBomb\r\nLevel: " + towerScript.level + "\r\nRadius ↑\r\nDamage ↑\r\n$" +
                    towerScript.costs[towerScript.level];
            }
            else
            {
                upgradeMenu.GetComponentInChildren<TMP_Text>().text =
                    "Wasabi\r\nBomb\r\nLevel: " + towerScript.level + "\r\n\r\nMax Level\r\nReached";
            }

            GameObject effect = GameObject.Instantiate(towerEffect,
                            new Vector3(upgradeTower.transform.position.x, upgradeTower.transform.position.y - 0.8f,
                                        upgradeTower.transform.position.z), Quaternion.Euler(90, 0, 0));
            Destroy(effect, 0.1f);

            hideMenus();
        }
    }

    public void sellAOETower()
    {
        Debug.Log("sell");
        AOEInfo towerScript = upgradeTower.GetComponent<AOEInfo>();
        ResourceManager.gold += towerScript.costs[towerScript.level - 1];
        Destroy(upgradeTower);

        hideMenus();
    }

    public void hideMenus()
    {
        foreach (GameObject menu in upgradeMenus)
        {
            menu.SetActive(false);
        }
    }

}
