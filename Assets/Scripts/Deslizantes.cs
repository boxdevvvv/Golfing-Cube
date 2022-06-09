using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deslizantes : MonoBehaviour
{
    public float _dynamicFriction;
    public float _staticFriction;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           other.GetComponent<SphereCollider>().material.dynamicFriction = _dynamicFriction;
            other.GetComponent<SphereCollider>().material.staticFriction = _staticFriction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<SphereCollider>().material.dynamicFriction = 0.4f;
        other.GetComponent<SphereCollider>().material.staticFriction = 0.4f;
    }
}
