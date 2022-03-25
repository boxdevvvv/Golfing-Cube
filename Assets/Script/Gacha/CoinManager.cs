using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int Coins { get; private set; }

    [SerializeField]
    int INITIAL_COINS = 100;
    const string COINS = "COINS";
    // key name to store high score in PlayerPrefs

    void Awake()
    {
       // PlayerPrefs.DeleteAll();
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        // Initialize coins
        Coins = PlayerPrefs.GetInt(COINS, INITIAL_COINS);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;


        // Store new coin value
        PlayerPrefs.SetInt(COINS, Coins);

    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;

        // Store new coin value
        PlayerPrefs.SetInt(COINS, Coins);

    }
}
