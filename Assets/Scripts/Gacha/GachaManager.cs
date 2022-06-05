using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GachaManager : MonoBehaviour
{
    public GameObject[] personajeDesbloqueable;
    public int numeroAleatorio;
    public Transform gachaExterno;
    public TextMeshProUGUI gold;
    public GameObject botonDeCompra;
    public GameObject ballsButton;
    public GameObject playButton;
    public GameObject volverButton;
    public GameObject homeButton;

    public GameObject reemplazoGacha;
   // public SkinsManager _skinsManager;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        gold.text = CoinManager.Instance.Coins.ToString();

    }
    public RectTransform botonSkins;
    private GameObject personajeQueSpawneo;
    public void Purchase()
    {
        if (CoinManager.Instance.Coins >= 250)
        {
            volverButton.SetActive(false);
            botonDeCompra.SetActive(false);
            botonSkins.DOAnchorPosY(-960, 1f).SetEase(Ease.OutBack);
            numeroAleatorio = Random.Range(0, personajeDesbloqueable.Length);

            GameObject _Character = Instantiate(personajeDesbloqueable[numeroAleatorio], gachaExterno.position, Quaternion.identity);
            _Character.transform.parent = gachaExterno.transform;
            _Character.SetActive(false);
            personajeQueSpawneo = _Character;
            if (PlayerPrefs.GetInt(_Character.GetComponent<Character>().characterName) == 0)
            {
                print("le salio " + PlayerPrefs.GetInt(_Character.GetComponent<Character>().characterName));
                _Character.GetComponent<Character>().SalioEnGachapon(); //activa la funcion al object spawneado
            }
            else
            {
                _Character.SetActive(false);
                Debug.Log("numero asignado ya fue usado");
                 PlayerPrefs.Save();

                personajeQueSpawneo = reemplazoGacha;
                sumarDinero = true;
            }

            CoinManager.Instance.RemoveCoins(250);
            gold.text = CoinManager.Instance.Coins.ToString();
            palanca.DOLocalRotate(new Vector3(-90, 0, 0), 1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InBack);

            StartCoroutine(animacionGacha());
            SkinsManager._skinsManager.CargadoDeDatos();
        }
        
    }

    public RawImage panelBlanco;
    public Transform gacha;
    public TextMeshProUGUI nameCharacter;
    public Transform palanca;
    public GameObject luzParaGacha;
    public GameObject confetti;
    public bool sumarDinero = false;
    IEnumerator animacionGacha()
    {
        yield return new WaitForSeconds(1.5f);
        gachaExterno.DOLocalMove(new Vector3(1.8f, -1.9f, -0.7f), 0.5f);//.SetEase(Ease.OutElastic);

        print("sadas");

        Debug.Log("animacion de gacha");
        yield return new WaitForSeconds(1);
        // gachaExterno.DOLocalMove(new Vector3(-0.62f,1,-3.4f), 1);
        gachaExterno.DOLocalMove(new Vector3(-1.2f, 1.2f, -4.2f), 1);

        gachaExterno.DORotate(new Vector3(10, 45, 0), 1, RotateMode.Fast);
        gachaExterno.DOScale(new Vector3(4, 4, 4), 1);
        yield return new WaitForSeconds(1); ////////////////////////////// espera a que llegue
        gachaExterno.DOScale(new Vector3(3.5f, 4.7f, 4), 1).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(0.5f);
        panelBlanco.DOFade(1, 0.1f);
        yield return new WaitForSeconds(0.2f);

        personajeQueSpawneo.transform.parent = null;
        gacha.transform.localScale = new Vector3(1, 1, 1);
        gachaExterno.gameObject.SetActive(false);

        nameCharacter.text = personajeQueSpawneo.GetComponent<Character>().characterName;
        nameCharacter.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        personajeQueSpawneo.transform.localScale = new Vector3(0, 0, 0);
        personajeQueSpawneo.SetActive(true);
        personajeQueSpawneo.transform.DOScale(new Vector3(10, 10, 10), 2);
        luzParaGacha.SetActive(true);
        confetti.SetActive(true);
        panelBlanco.DOFade(0, 0.1f);
        personajeQueSpawneo.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), 3, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        yield return new WaitForSeconds(2);

        homeButton.SetActive(true);
        playButton.SetActive(true);
        ballsButton.SetActive(true);

        gachaExterno.localScale = new Vector3(1, 1, 1);
        gachaExterno.transform.localPosition = new Vector3(1.8f, -1.9f, 1.81f);
        gachaExterno.gameObject.SetActive(true);
        gachaExterno.localRotation = Quaternion.Euler(0, 0, 0);

        if(sumarDinero)
        {
            sumarDinero = false;
            CoinManager.Instance.AddCoins(100);
            gold.text = CoinManager.Instance.Coins.ToString();
        }

        yield break;
    }

    public void reiniciar()
    {
        StartCoroutine(RestartGacha());
        
    }



    IEnumerator RestartGacha()
    {
        confetti.SetActive(false);

        personajeQueSpawneo.transform.DOScale(new Vector3(0, 0, 0), 1).SetEase(Ease.InBack);
        nameCharacter.gameObject.SetActive(false);
        homeButton.SetActive(false);
        ballsButton.SetActive(false);
        playButton.SetActive(false);
        yield return new WaitForSeconds(1);
        luzParaGacha.SetActive(false);

        personajeQueSpawneo.SetActive(false);
        //Destroy(personajeQueSpawneo);
        palanca.localRotation = Quaternion.Euler(0, 0, 0);
        gacha.DOScale(new Vector3(242,242,242), 1).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1);
        volverButton.SetActive(true);
        botonDeCompra.SetActive(true);
        botonSkins.DOAnchorPosY(-604, 1f).SetEase(Ease.OutBack);


        yield break;
    }




}