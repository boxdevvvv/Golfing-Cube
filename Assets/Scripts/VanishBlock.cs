using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VanishBlock : MonoBehaviour
{
    public bool isTouched;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTouched) // si chocan con el player reproducen una animacion
        {
            isTouched = true;
            
            transform.DOScale(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.InBack);
            SoundManager.PlaySound("BloqueEspecial");

            GetComponent<BoxCollider>().isTrigger=true;
        }
    }

}
