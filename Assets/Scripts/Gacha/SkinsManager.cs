using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkinsManager : MonoBehaviour
{
    public Button purchaseButton;
    public RectTransform shopPanel;
    public RectTransform buttonOpen;
    
    public bool isOpen = false;
    public void UpShop()
    {
        if(isOpen)
        {
            purchaseButton.interactable = true;
            print("Me cerraste");
            isOpen = false ;
            shopPanel.DOAnchorPosY(-604, 0.5f);
            buttonOpen.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
            
            return;
        }
        if(!isOpen)
        {
            isOpen = true;
            shopPanel.DOAnchorPos3DY(1075, 0.5f);
            buttonOpen.DOLocalRotate(new Vector3(0, 0, 180), 0.5f);
            print("Me abroste");
            purchaseButton.interactable = false;
        }
    }
    private void Start()
    {
        for (int i = 0; CharacterManager.Instance.characters.Length >= i; i++)
        {
            print("veces que inicio");

            if (PlayerPrefs.GetInt(CharacterManager.Instance.GetComponent<Character>().characterName) == 1)
            {
                botones[i].interactable = true;
                lockers[i].SetActive(false);
                print(i);
            }
            botones[CharacterManager.Instance.CurrentCharacterIndex].interactable = false;
            print("veces que llego");
        }
    }
    public Button[] botones;
    public GameObject[] lockers;

    public void SeleccionoButton(int _numberSelection) //setiador personaje
    {
        CharacterManager.Instance.CurrentCharacterIndex = _numberSelection;
       for(int i = 0; botones.Length<i; i++)
       {
            botones[i].interactable = true;
       }
        botones[_numberSelection].interactable = false;
    }


}
