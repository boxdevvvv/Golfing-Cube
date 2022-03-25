using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacles : MonoBehaviour
{
    public bool scalaUnica;
    private void Awake()
    {
        transform.localScale = new Vector3(0, 0, 0); //siempre empiezan en escala 0 para dar la sensacion de que salen del suelo
    }
    void Start()
    {       
       transform.DOScale(new Vector3(1, 1, 1), 0.3f).SetEase(Ease.Linear);
       if(scalaUnica)
       {
        transform.DOScale(new Vector3(1, 1, 0.842f), 0.3f).SetEase(Ease.Linear);
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SoundManager.PlaySound("BloqueEspecial");
        }
    }
}
