using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
public class UIManager : MonoBehaviour
{
    public string pixelArtName; //nombre que se ve al final de cada escena
    public TextMeshProUGUI textToName; // se le da el nombre
    public Image[] movements; //imagenes en el canvas que representan los movimientos
    public TextMeshProUGUI loseText; //texto cuando te quedas sin movimientos
   // public Material[] skins; //skins que se le da al player
    public static UIManager _UIManager;
    private void Awake()
    {
        _UIManager = this;
    }

    void Start()
    {
        DOTween.SetTweensCapacity(500, 10); //cambia la capacidad maxima de tweens, se supone que dotween lo hace automaticamente si se llega a sobrepasar el limite pero, la documentacion recomienda ponerlo igualmente
    }       
    public void FinishLevel() // muestra el nombre del objeto armado, lo activa el GameManager
    {
        textToName.DOFade(1, 0.5f);
        textToName.text = pixelArtName;
    }

    public void BallsMove()
    {
        movements[GameManager._gameManager.movements].rectTransform.DOScale(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.InBack); //quita un movimiento de la UI, lo active el GameManager
    }
  //  public RectTransform gachaButton;
   // public RectTransform ballsButton;

    public void esconderUI()
    {
        Invoke("HideUI",0.15f);
    }
    public void HideUI()
    {
      //  gachaButton.GetComponent<Button>().enabled = false;
       // ballsButton.GetComponent<Button>().enabled = false;

        //gachaButton.DOAnchorPosX(155,0.5f).SetEase(Ease.InBack);
        //ballsButton.DOAnchorPosX(-155, 0.5f).SetEase(Ease.InBack);

    }

    public void LastMovement() //el GameManager lo activa, pasa cuando usas tus 3 movimientos y no llegas a la meta, se espera 3 segundos por si en esos 3 segundos llegas
    {
        Invoke("LoseText", 3f);
    }
    public void LoseText() //si no llegas a la meta aparece el texto de "lose" y te permite reiniciar la escena
    {
        if(GameManager._gameManager.inGoal == false)
        {
            loseText.rectTransform.DOAnchorPosY(430, 0.5f).SetEase(Ease.OutBack);
            GameManager._gameManager.canRestart = true;
        }
    }
    
    public void ActivateMovement()
    {
        GameManager._gameManager.canMove = true;
    }

}
