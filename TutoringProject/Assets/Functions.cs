using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(1 + 1);
        print(mathFunction());
    }

    void dumbfunction()
    {
        print(1 + 1);
    }

    public int mathFunction()
    {
        return 1 + 1;
    }

    private static int mathFunction(int num1, int num2)
    {
        return num1 + num2;
    }
}
