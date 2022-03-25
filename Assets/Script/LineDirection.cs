using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDirection : MonoBehaviour
{
    
    public Vector3 inicioPosDirection = new Vector3(0f, 0, 0); //inicio de la linea
    public Vector3 finalPosDirection = new Vector3(0f, 0f, 1);//direccion final de la linea
    public LineRenderer DirectionLine;

    private void Awake() //en algunos celulares la primera vez que se activa el update la linea no se pone en su lugar lo suficientemente rapido y te permite ver como se "teletransporta", esto corrige ese error
    {
        Vector3 newBeginPos = transform.localToWorldMatrix * new Vector4(inicioPosDirection.x, inicioPosDirection.y, inicioPosDirection.z, 1);
        Vector3 newEndPos = transform.localToWorldMatrix * new Vector4(finalPosDirection.x, finalPosDirection.y, finalPosDirection.z + BallControl.scale, 1); 
        DirectionLine.SetPosition(0, newBeginPos); 
        DirectionLine.SetPosition(1, newEndPos);
        arrow.transform.position = new Vector3(newEndPos.x, newEndPos.y, newEndPos.z);
    }
    public Transform arrow; // punta de la flecha
    void Update()
    {
        //Calcula la posicion actual 
        Vector3 newBeginPos = transform.localToWorldMatrix * new Vector4(inicioPosDirection.x, inicioPosDirection.y, inicioPosDirection.z, 1); //permite saber la posicion actual
        Vector3 newEndPos = transform.localToWorldMatrix * new Vector4(finalPosDirection.x, finalPosDirection.y, finalPosDirection.z + BallControl.scale, 1); //con el scale se puede controlar que tanto se alarga el LineRenderer
        //aplica la nueva posicion
        DirectionLine.SetPosition(0, newBeginPos); //posicion actual
        DirectionLine.SetPosition(1, newEndPos); //el linerenderer apunta constantemente hacia el frente siguiendo la rotacion del container el cual es controlado por el BallControl, es necesario esto ya que como el linerender no puede rotar por cuenta propia necesita un guia
        arrow.transform.position = new Vector3(newEndPos.x, newEndPos.y, newEndPos.z); //permite a la flecha siempre estar en la punta de la linea, el BallControl le da la direccion para que gire segun dodne se apunte
    }
}

