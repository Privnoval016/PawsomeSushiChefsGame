using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    TMP_Text goldText;

    public static int gold = 0;         //Static variables belong to the class,
                                        //only one allowed for all instances of the class.
                                        //Can be accessed without creating a new object.
    public int startingGold = 10;

    // Start is called before the first frame update
    void Start()
    {
        goldText = GameObject.Find("Gold").GetComponent<TMP_Text>();
        gold = startingGold;
        SetGoldText();
    }

    // Update is called once per frame
    void Update()
    {
        SetGoldText();
    }

    void SetGoldText()
    {
        goldText.text = "Gold: " + gold;
    }

    //Static method belongs to the class and doesn't need any instance to call it.
    //Static methods cannot use regular member variables, only static member variables.
    //To call a static method: ClassName.MethodName();
    //For this one: ResourceManager.IncreaseGold(1);
    public static void IncreaseGold(int amount)
    {
        gold += amount;
    }
}
