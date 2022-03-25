using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StretchBlock : MonoBehaviour
{

    void Start()
    {
        //transform.DOScale(new Vector3(3.5f, 0.8f, 1), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBack); //no es posible agregar delay 

        Sequence stretch = DOTween.Sequence();
        stretch.SetDelay(0.5f);
        stretch.Append(transform.DOScale(new Vector3(3.7f, 0.8f, 1), 1f).SetEase(Ease.OutBack));
        stretch.SetLoops(-1, LoopType.Yoyo);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.PlaySound("BloqueEspecial");
        }
    }
}
