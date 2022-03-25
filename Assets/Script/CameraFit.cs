using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraFit : MonoBehaviour
{
    //permite hacer que la camara se ajuste a los bordes del celular, permitiendo asi siempre mostrar lo que te interese y no hacer que el celular abarque mas de lo que debe, muy util
    public CinemachineVirtualCamera vcam;
    public CinemachineVirtualCamera finalCam;
    public CinemachineBrain brainCam;
    public SpriteRenderer medidor;
    public TextMeshProUGUI anchorNumber; //permite ver el porte de la camara en cada celular, dependiendo del celular tiene pequenas variaciones pero en general no baja de 60 y no sube de 63
    public float size;
    private void Awake()
    {
        size = medidor.bounds.size.x * Screen.height / Screen.width * 0.5f;
        vcam.m_Lens.FieldOfView = size;
        finalCam.m_Lens.FieldOfView = size;
        //anchorNumber.text = vcam.m_Lens.FieldOfView.ToString();
    }
}
