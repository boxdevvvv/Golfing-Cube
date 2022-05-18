using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ScenesMenu : MonoBehaviour
{
    public bool isShop = false;
    public void LoadLevels()
    {
        if (!isShop)
        {
            if(PlayerPrefs.GetInt("Level") <=2)
            {
                PlayerPrefs.SetInt("Level", 3);
            }
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            return;
        }
        if (isShop)
        {
            SceneManager.LoadScene("Gacha");
        }
    }
    public bool colisiono = false;
    public Transform[] environment;
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!colisiono)
            {
                colisiono = true; //es para evitar que se vuelva activar en caso de que el player vuelva a entrar
                player.DOScale(new Vector3(0, 0, 0), 0.5f);
                player.GetComponent<TrailRenderer>().enabled = false;
                for (int i = 0; i < environment.Length; i++)
                {
                    print(environment.Length);
               //    DOTween.KillAll(environment[i].transform);  //detiene la animacion para evitar bugs de los bloques estirables
                    environment[i].DOScale(new Vector3(0, 0, 0), 0.5f); //.OnComplete(()=> obstacles[i].gameObject.SetActive(false)); //esconde las trampas del mapa, es importante apagarlos o algunos que tienen animaciones seguiran animandose
                }
            }           
            GameManager._gameManager.inGoal = true;//avisa que llego a la meta para evitar la funcion que pone el texto de reiniciar nivel
            GameManager._gameManager.canRestart = false; //si se activo la booleana en el gameManager la desactiva para no reiniciar el nivel con la pulsacion
                                                         //textFail.gameObject.SetActive(false); //apaga el texto que sale si no tienes mas tiros
            GameManager._gameManager.canMove = false; //evita el bug de seguir perdiendo movimientos en el canvas a pesar de haber ganado el nivel
            StartCoroutine(ActivadorScale());
        }
    }
    IEnumerator ActivadorScale()
    {
        yield return new WaitForSeconds(0.5f);
        LoadLevels();
        yield break;
    }


    public void GoToScene()
    {
       
    }
}
