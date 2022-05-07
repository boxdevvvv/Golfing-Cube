using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animal : MonoBehaviour
{
    public int direction = 0;
    void Start()
    {

        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Directions"))
        {
            //other.GetComponent<Directions>();
        }
    }
}
