using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ReboundBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // si chocan con el player reproducen una animacion
        {
            transform.DOScale(new Vector3(1.2f, 1.2f, transform.localScale.z+0.2f), 0.1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear);
            SoundManager.PlaySound("BloqueEspecial");
                     
        }
    }

}
