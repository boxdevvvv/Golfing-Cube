using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacles : MonoBehaviour
{
    public bool scalaUnica;

    public Vector3 escalaOriginal;
    private void Awake()
    {
        if (scalaUnica)
        {
            escalaOriginal = transform.localScale;
        }

        transform.localScale = new Vector3(0, 0, 0); //siempre empiezan en escala 0 para dar la sensacion de que salen del suelo
    }


    void Start()
    {
        if (!scalaUnica)
        {
           transform.DOScale(new Vector3(1, 1, 1), 0.3f).SetEase(Ease.Linear);
        }
        else
        {
           transform.DOScale(escalaOriginal, 0.3f).SetEase(Ease.Linear);
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
