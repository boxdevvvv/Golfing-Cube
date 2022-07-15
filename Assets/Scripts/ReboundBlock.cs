using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ReboundBlock : MonoBehaviour
{
    private float zScale;
    private float xScale;
    private float yScale;
    private void Start()
    {
        Invoke("LlamdoDeMedidas", 0.5f);
    }    
    public void LlamdoDeMedidas()
    {
        zScale = transform.localScale.z;
        yScale = transform.localScale.y; //+ 0.2f;
        xScale = transform.localScale.x;// + 0.2f;
    }
        
    
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // si chocan con el player reproducen una animacion
        {
            transform.DOScale(new Vector3(xScale +0.2f, yScale + 0.2f, zScale + 0.2f), 0.3f);//.SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear);
            SoundManager.PlaySound("BloqueEspecial");                   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // sale el player
        {
            DOTween.Kill(transform.gameObject);
            transform.DOScale(new Vector3(xScale, yScale, zScale), 0.3f);//.SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear);
            SoundManager.PlaySound("BloqueEspecial");
        }

    }
   // transform.DOScale(new Vector3(xScale, yScale, zScale), 0.3f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear);
}
