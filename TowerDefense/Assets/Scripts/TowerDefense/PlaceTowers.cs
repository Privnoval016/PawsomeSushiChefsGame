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
    RectTransform canvasRect;
    RectTransform panelRect;
    TowerSpawn towerSpawn;
    public GameObject selectedTower;
    public GameObject[] towers = new GameObject[3];
    public Button[] buttons = new Button[3];
    public GameObject[] upgradeMenus = new GameObject[3];
    public GameObject upgradeMenu;
    public GameObject upgradeTower;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        buildTowerPanel = GameObject.Find("BuildTowerPanel");

        canvasRect = canvas.GetComponent<RectTransform>();
        panelRect = buildTowerPanel.GetComponent<RectTransform>();

        buildTowerPanel.SetActive(false);

        foreach (GameObject menu in upgradeMenus)
        {
            menu.SetActive(false);
        }

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
            foreach (GameObject menu in upgradeMenus)
            {
                menu.SetActive(false);
            }

            //If the player clicks, disable the panel (in case they clicked off the build menu)
            if (buildTowerPanel.activeSelf)
            {
                
                buildTowerPanel.SetActive(false);
                
            }



            RaycastHit hit;

            //Check to see if raycast from camera at the current mouse position hits the terrain
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {

                if (hit.collider.gameObject.CompareTag("TowerSpawn"))
                {
                    //Check to see if tower was alread created on spawn
                    towerSpawn = hit.collider.gameObject.GetComponent<TowerSpawn>();

                    if (towerSpawn.tower == null)
                    {
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

                        TowerInfo towerStats = selectedTower.GetComponent<TowerInfo>();

                        if (towerStats.level <= 0 && ResourceManager.gold >= towerStats.costs[0])
                        {
                            ResourceManager.gold -= towerStats.costs[0];
                            GameObject tower = GameObject.Instantiate(selectedTower,
                                new Vector3(towerSpawn.transform.position.x, towerSpawn.transform.position.y + 2,
                                towerSpawn.transform.position.z), towerSpawn.transform.rotation);

                            towerSpawn.tower = tower;
                            TowerInfo currentTower = tower.GetComponent<TowerInfo>();
                            currentTower.type = 0;
                            currentTower.level = 1;
                        }
                    }
                }
                else if (hit.collider.gameObject.CompareTag("Tower"))
                {
                    upgradeTower = hit.collider.gameObject;
                    TowerInfo upgradeScript = upgradeTower.GetComponent<TowerInfo>();
                    upgradeMenu = upgradeMenus[upgradeScript.type];
                    if (upgradeScript.level < 3)
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
            }
        }
    }

    public void changeSelectedTower(int index)
    {
        selectedTower = towers[index];

        
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].GetComponent<Image>().color = new Color(0.953f, 0.941f, 0.843f, 1);
        }

        buttons[index].GetComponent<Image>().color = new Color(1, 0.749f, 0.525f, 1);
        



    }

    public void upgradeLastTower()
    {
        Debug.Log("upgrade");
        TowerInfo towerScript = upgradeTower.GetComponent<TowerInfo>();
        if (towerScript.level < 3 && ResourceManager.gold >= towerScript.costs[towerScript.level])
        {
            ResourceManager.gold -= towerScript.costs[towerScript.level];
            towerScript.level++;
            Debug.Log(towerScript.level);
            towerScript.turretScript.fireRate = towerScript.fireRates[towerScript.level];
            towerScript.turretScript.damage = towerScript.damages[towerScript.level];

            if (towerScript.level < 3)
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

            foreach (GameObject menu in upgradeMenus)
            {
                menu.SetActive(false);
            }
        }
    }

    public void sellTower()
    {
        Debug.Log("sell");
        TowerInfo towerScript = upgradeTower.GetComponent<TowerInfo>();
        ResourceManager.gold += towerScript.costs[towerScript.level - 1];
        Destroy(upgradeTower);

        foreach (GameObject menu in upgradeMenus)
        {
            menu.SetActive(false);
        }
    }
}
