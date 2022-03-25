using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blockings : MonoBehaviour
{
    public bool isObstacle;    //si son obstaculo crecen al iniciar la escena
    public Transform blocking; 
    private void Awake()
    {
        blocking.localScale = new Vector3(0, 0, 0); //siempre empiezan en escala 0 para dar la sensacion de que salen del suelo
    }
    void Start()
    {
        if (isObstacle) //si son obstaculo crecen al iniciar la escena y se quitan el trigger para poder colisionar con ellos
        {
            GetComponent<BoxCollider>().isTrigger = false;
            blocking.DOScale(new Vector3(1, 1, 1), 0.3f).SetEase(Ease.OutBack);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Activador")) // en el editor hay un objeto activador el cual se activa al pasar el nivel, este crece cada X segundos y da la sensacion de que los blockes crezcan en diferentes tiempos
        {
            blocking.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack);
        }
        if(other.CompareTag("Player") && isObstacle) // si chocan con el player reproducen una animacion
        {
            blocking.DOScale(new Vector3(1.2f,1.2f,1.2f),0.1f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.Linear);
            SoundManager.PlaySound("BloqueNormal");
        }
    }

}
