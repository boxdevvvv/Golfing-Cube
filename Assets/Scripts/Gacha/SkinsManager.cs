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
        PlayerPrefs.SetInt(CharacterManager.Instance.characters[0].GetComponent<Character>().characterName, 1);
        CargadoDeDatos();
    }

    public void CargadoDeDatos()
    {
        for (int i = 0; CharacterManager.Instance.characters.Length - 1 >= i; i++)
        {
            print("veces que inicio");

            if (PlayerPrefs.GetInt(CharacterManager.Instance.characters[i].GetComponent<Character>().characterName) == 1)
            {
                print("number boton " + i);
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
        botones[CharacterManager.Instance.CurrentCharacterIndex].interactable = true;
        CharacterManager.Instance.CurrentCharacterIndex = _numberSelection;
        botones[_numberSelection].interactable = false;
    }


}
