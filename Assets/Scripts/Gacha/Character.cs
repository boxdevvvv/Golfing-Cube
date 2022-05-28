using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int characterSequenceNumber;
    public string characterName;
    public int price;
    public bool isFree = false;

    public bool IsUnlocked
    {
        get
        {
            return (isFree || PlayerPrefs.GetInt(characterName, 0) == 1);
        }
    }

    void Awake()
    {
        print("primer print" + characterName);
       // characterName = characterName;//.ToUpper();
        print("second print" + characterName);

    }
    public void SalioEnGachapon()
    {
        PlayerPrefs.SetInt(characterName, 1);
        print("le cambio el numero a: " + PlayerPrefs.GetInt(characterName)+ characterName);

        PlayerPrefs.Save();
        print("ya lo guardo");
    }

    public bool Unlock()
    {
        if (IsUnlocked)
            return true;

        if (CoinManager.Instance.Coins >= price)
        {
            PlayerPrefs.SetInt(characterName, 1);
            PlayerPrefs.Save();
        //    SgLib.CoinManager.Instance.RemoveCoins(price);

            return true;
        }

        return false;
    }
}