using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;                                //Library to change text of a TextMesh
using UnityEngine.SceneManagement;          //Library needed to reload scene

public class PlayerGameOver : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot
    TMP_Text gameOverText;                                          //Game Over text GUI component
    GameObject gameOverPanel;                                       //Game Over panel GUI component

    // Start is called before the first frame update
    void Start()
    {
        //Find the Game Over text GUI component
        gameOverText = GameObject.Find("GameOver").GetComponent<TMP_Text>();

        //Find the Game Over panel GUI component
        gameOverPanel = GameObject.Find("GameOverPanel");

        //Disable Game Over panel (hide it)
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Nothing to do. This is the way.
    }

    //Restart the game
    public void Restart()
    {
        //This reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Building")
        {
            //Disable guard so it stops shooting at the player
            GameObject guard = GameObject.Find("Guard");
            guard.SetActive(false);

            //Enable Game Over panel (show it)
            gameOverPanel.SetActive(true);

            //Display player wins text
            //\n means newline which adds a line break in between making this a two line message
            gameOverText.text = "Game Over\nPlayer Wins!!";
        }
    }
}
