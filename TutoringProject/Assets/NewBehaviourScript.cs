using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Variable
    // Type name value

    // primitives
    // low level
    // small amounts of information
    // numbers, letters, boolean

    // integers
    // whole numbers
    // negative or positive
    // ints, bytes, shorts, longs

    // use the smallest data type
    // give yourself allowance

    // basic integer types
    // bytes 8 bit 0 to 255
    // shorts 16 bit -32,768 to 32,767
    // ints 32 bit -2,147,483,648 to 2,147,483,647
    // longs 64 bit -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807

    // int variants
    // sbytes -127 to 127

    // ushort 0 to 65,535
    // uint 0 to 4,294,967,295
    // ulong 0 to 18,446,744,073,709,551,615

    // integer overflow
    // wraps to the lowest or highest value

    // floating data types
    // numbers with decimals
    // imprecise

    // float 7 digits of precision 1.2345323
    // double 15 digits of precision

    // unstable at values
    // store player location

    // char
    // character
    // ''
    // 'a' 'b' 'c' '-' '☺'

    // bool
    // boolean
    // true
    // false

    // int, float, char, bool, double, byte, short, long

    // computers work on 1s and 0s
    // bit
    // short = 0000000000000000
    // short = 2^16

    // Objects

    // word
    // "hello"
    // array of chars
    // 'h' + 'e' + 'l' + 'l' + 'o'
    // string

    // way to store multiple pieces of the same type of data
    // array
    // work off of index
    // start at 0
    // the location of in a array
    // represented by an int

    // list
    // like an array
    // but cooler
    // variable size
    // more functionality

    // cat class

    int[] numberArray = new int[] { 6,8,7,5,1};

    List<int> numberList = new List<int> { 6,8,7,5,1};

    public static float number = 0;

    public readonly int publicNumber = 1;
    public const int constNumber = 1;

    void Start()
    {
        numberList[0] += 10;

        cat kitty = new cat();
        print(kitty.catName);

        kitty.catName = "cuddles";
        print(kitty.catName);

        cat kittyTwo = new cat();

        numberList[5] = 2;

        float floatNumber = 30;
        floatNumber = Mathf.Clamp(floatNumber, 0, float.PositiveInfinity);

        int index = Mathf.Clamp(10, 0, numberList.Count - 1);

        floatNumber = Mathf.Abs(floatNumber);
        floatNumber = Mathf.RoundToInt(floatNumber);
        floatNumber = Mathf.Max(floatNumber, 10);
        floatNumber = Mathf.Min(floatNumber, 10);
    }

    void Update()
    {
        
    }
}

class cat
{
    public Conditionals.food favoriteFood = Conditionals.food.pizza;
    public string catName = "skittles";
    public int age = 13;
    public string gender = "female";
}