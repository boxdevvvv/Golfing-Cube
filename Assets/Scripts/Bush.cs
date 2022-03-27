using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bush : MonoBehaviour
{
    //son adornos, solo crecen, el gameManager se encarga de achicarlos una vez pasas el nivel
    public Vector3 size; //toma su tamaño actual para luego hacer que crezca a este, permitiendo asi tener varios tamaños
    private void Awake()
    {
        size = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    private float timer;
    private void Start()
    {
        timer = Random.Range(0.5f, 2f); //se evita que todo crezca de manera uniforme dandole mas vida al escenario
        transform.DOScale(size, timer).SetEase(Ease.OutBack);
    }
   
}
