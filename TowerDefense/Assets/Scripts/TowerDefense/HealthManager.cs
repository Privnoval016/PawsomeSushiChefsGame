using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    TMP_Text healthText;

    static int health = 0;              //Static variables belong to the class,
                                        //only one allowed for all instances of the class.
                                        //Can be accessed without creating a new object.
    public int startingHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("Health").GetComponent<TMP_Text>();
        health = startingHealth;
        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthText();

        if (health <= 0)
        {
            Debug.Log("Game over!! Player lost!!");
        }
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }

    //Static method belongs to the class and doesn't need any instance to call it.
    //Static methods cannot use regular member variables, only static member variables.
    //To call a static method: ClassName.MethodName();
    //For this one: HealthManager.ReduceHealth(1);
    public static void ReduceHealth(int amount)
    {
        health -= amount;
    }
}
