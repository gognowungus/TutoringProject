using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loops : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] numberArray = new int[] { 6, 8, 7, 5, 1 };

        for (int i = 0; i < numberArray.Length; i++)
        {
            print(numberArray[i]);
        }

        foreach(int num in numberArray)
        {
            print(num);
        }

        List<int> numberList = new List<int> { 6, 8, 7, 5, 1 };

        foreach (int num in numberList)
        {
            numberList.RemoveAt(0);
            print(num);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
