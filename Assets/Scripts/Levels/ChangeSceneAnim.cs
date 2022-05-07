using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeSceneAnim : MonoBehaviour
{
    private void OnTriggerExit(Collider other) //permite ejecutar la animacion de las oleadas, todo lo que no tenga de tag Ground lo achica
    {
        if(!other.CompareTag("Ground"))
        {
            other.transform.DOScale(new Vector3(0, 0, 0), 0.5f);

            print("Toco cosas a achicar");
        }      
    }
}
