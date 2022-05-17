using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager _levelManager;
    public bool restartPlayerPREFS;

    private void Awake()
    {
       if( restartPlayerPREFS)
        {
            PlayerPrefs.DeleteAll();
        }
        _levelManager = this;
    }
    public void WinLevel()
    {
     PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        GameScenes();
    }
 
   public void GameScenes()
    {
        if(PlayerPrefs.GetInt("Level") <= 1 || PlayerPrefs.GetInt("Level") >= 9)
        {
            PlayerPrefs.SetInt("Level",3);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        print(PlayerPrefs.GetInt("Level"));
    }


    public void ballScene()
    {
        SceneManager.LoadScene("Seleccion");

    }

    public void gachaponScene()
    {
        SceneManager.LoadScene("Gacha");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");

    }

}
