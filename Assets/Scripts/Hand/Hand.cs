using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveZ(-2, 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
    private void Update()
    {

        if (Input.touchCount > 0) //esto se usa para jugar en celular, ocupa las mismas funciones de lanzamiento
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
