using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditionals : MonoBehaviour
{
    public enum food { pizza, mac, apple, banana, rice, coffee }
    public food currentFood = food.pizza;

    Dictionary<food, string> foodDictionary = new Dictionary<food, string>
    {
        {food.pizza, "yummy pizza" },
        {food.apple, "Red apple" },
        {food.coffee, "warm coffee" }
    };

    void Start()
    {
        print(foodDictionary[food.coffee]);
        int number = 1;
        bool boolean = number > 0;

        int numberTwo = 10;

        if (boolean && numberTwo == 10)
        {
            print("greater");
        }

        // switch

        switch(currentFood)
        {
            case food.pizza:
                print("cheesy");
                break;
            case food.mac:
                print("mac and cheesy");
                break;
            case food.apple:
                // do if 2
                break;
            default:
                // do if no match found
                break;
        }

        bool change = true;
        int newNumber = change ? 5 : 6;

        if(change)
        {
            newNumber = 5;
        }
        else
        {
            newNumber = 6;
        }
    }

    void Update()
    {
        
    }
}