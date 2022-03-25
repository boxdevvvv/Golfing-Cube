using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Goal : MonoBehaviour
{
    public GameObject textFail; //texto que se debe apagar en caso de que llegues a la meta y este haya aparecido
    public Transform[] obstacles;//los obstaculos en el escenario
    public bool haveObstacles; // para esconderlos solo en caso de que estos existan en el escenario

    private void Awake()
    {
        transform.localScale = new Vector3(0, 0, 0); //empieza chico
    }
    void Start()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack); // se hace grande
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))  
        {
            if(haveObstacles)
            {
                haveObstacles = false; //es para evitar que se vuelva activar en caso de que el player vuelva a entrar
                for (int i = 0; i < obstacles.Length; i++)
                {
                    print(i);
                    DOTween.KillAll(obstacles[i].transform);  //detiene la animacion para evitar bugs de los bloques estirables
                    obstacles[i].DOScale(new Vector3(0, 0, 0), 0.5f); //.OnComplete(()=> obstacles[i].gameObject.SetActive(false)); //esconde las trampas del mapa, es importante apagarlos o algunos que tienen animaciones seguiran animandose
                }
            }            
            GameManager._gameManager.WinLevel(); //empieza la corutina para hacer la animacion de "olada" de los bloques
            transform.DOScale(new Vector3(0, 0, 0), 0.5f); //se achica
            GameManager._gameManager.inGoal = true;//avisa que llego a la meta para evitar la funcion que pone el texto de reiniciar nivel
            GameManager._gameManager.canRestart = false; //si se activo la booleana en el gameManager la desactiva para no reiniciar el nivel con la puslacion
            textFail.gameObject.SetActive(false); //apaga el texto que sale si no tienes mas tiros
            GameManager._gameManager.canMove = false; //evita el bug de seguir perdiendo movimientos en el canvas a pesar de haber ganado el nivel


        }
    }
}
