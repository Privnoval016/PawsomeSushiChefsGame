using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchLevel(int level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void StartScreen()
    {
        SwitchLevel(0);
    }

    public void ReplayScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Replay: " + scene.name);
        SceneManager.LoadScene(scene.name);
    }
}
