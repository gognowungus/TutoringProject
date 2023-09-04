using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score;
    public int coins;
    public int scorePerCoin;

    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ChangeScore(int amount)
    {
        score = Mathf.Clamp(score + amount, 0, int.MaxValue);
    }

    public void GetCoins(int amount)
    {
        coins = Mathf.Clamp(coins + amount, 0, int.MaxValue);
        ChangeScore(amount * scorePerCoin);
    }
}
