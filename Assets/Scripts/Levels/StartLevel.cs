using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    private void Start()
    {
        LevelManager._levelManager.GameScenes();
    }

}
