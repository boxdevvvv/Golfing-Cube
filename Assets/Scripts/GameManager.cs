using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int coins;



    public CinemachineVirtualCamera _cameraFinal; // camara en la que te pones al pasar el nivel
    public GameObject activador; //este objeto al activarse logra la animacion de "oleada" de los bloques la pasar el nivel
    public static GameManager _gameManager; 
    public int scaleCount = 0;//setea la scala del activador a 0
    public Transform player;
    public RectTransform nextButton; //boton que sale al pasar el nivel
    public bool canMove = true; //variable usada en BallControll para moverte
    public bool inGoal; //se activa al llegar a la meta, es necesario para una funcion del UIManager que pone texto dependiendo de si llegas o no a la meta
    public Transform[] bush; //aqui se guardan los adornos, permite achicarlos al pasar el nivel

    public Transform playerBall;
    private void Awake()
    {
        _gameManager = this;
        player.localScale = new Vector3(0, 0, 0);


    }
    private void Start()
    {

        GameObject spawnBall = Instantiate(CharacterManager.Instance.characters[CharacterManager.Instance.CurrentCharacterIndex]);
        spawnBall.transform.SetParent(playerBall);
        spawnBall.transform.localPosition = new Vector3(0, 0, 0);
        spawnBall.transform.localScale = new Vector3(1, 1, 1);
        player.DOScale(new Vector3(1, 1, 1), 0.5f); // anim de player
    }
    public void WinLevel() // si llegas a la meta se activa el activador y la corutina que permite hacer que los bloques crezcan en forma de oleada, ademas esconde al player
    {
        StartCoroutine(ActivadorScale());
        player.DOScale(new Vector3(0, 0, 0), 0.5f);
        player.GetComponent<TrailRenderer>().enabled = false;
        activador.SetActive(true);

    }
    IEnumerator ActivadorScale()
    {
        _cameraFinal.m_Priority = 10; //pone la camara superior
        SoundManager.PlaySound("Emerge");
        while (scaleCount < 16) //hace crecer al activador para dar la sensacion de oleada
        {
            activador.transform.localScale = new Vector3(activador.transform.localScale.x + 0.5f, 1, activador.transform.localScale.z + 0.5f);
            yield return new WaitForSeconds(0.05f);
            scaleCount++;
            print("aumento escala");
        }
        GetComponent<UIManager>().FinishLevel(); //muestra el nombre del objeto armado
        nextButton.DOAnchorPosY(75, 0.5f).SetEase(Ease.OutBack); //aparece el boton que te lleva al otro nivel
        yield break;
    }
    IEnumerator ChangeSceneAnim() // al presionar el boton de pasar de nivel activas esto
    {
        GetComponent<UIManager>().textToName.text = ""; // quita el texto que deice que objeto era el que armas
        _cameraFinal.m_Priority = 0; //vuelve a su camara normal
        for (int i = 0; i < bush.Length; i++)
        {
            bush[i].DOScale(new Vector3(0, 0, 0), Random.Range(0.5f,1.5f)); //esconde los adornos del ambiente

        }      
        while (scaleCount > -1) // da la sensacion de que la figura que armaste ahora se desarma 
        {
            activador.transform.localScale = new Vector3(activador.transform.localScale.x - 0.5f, 0, activador.transform.localScale.z - 0.5f);
            yield return new WaitForSeconds(0.05f);
            scaleCount--;
            print("Disminuyo escala");
        }
        activador.transform.position = Vector3.up; //luego lo subes debido a que si quedas al medio los ultimos 4 bloques no se achican
        yield return new WaitForSeconds(1);

        StartNewScene();

        yield break;
    }
    public void NextLevel() //activas la corutina de animacion
    {
        StartCoroutine(ChangeSceneAnim());       
        
    }
    public void StartNewScene() //toma el numero actual de la escena y ve si puede ir a la siguiente segun el numero que le des, si puede seguir entonces vuelve a la primera
    {
        CoinManager.Instance.AddCoins(15);
        LevelManager._levelManager.WinLevel();
       
    }

    public int movements; //movimientos que tienes para mover la bola
    public bool canRestart; //permite saber al ballcontroll si puede reiniciar escena al quedarse sin movimientos
    //PUBLIC int numero tirables
    public int tirosDisponibles = 0;
    public void PlayerMovement()
    {
        GetComponent<UIManager>().BallsMove(); //quita una de las esferas del canvas para dar a entender que perdiste un movimiento
        movements++;//suma un movimiento
        if (movements == 3) //si es igual a 3 le avisa al BallControll que ya no se puede mover y al UIManager que fue su ultimo movimiento
        {
            canMove = false;
            GetComponent<UIManager>().LastMovement();
        }      
    }
    public void RestartLevel() //boton para reiniciar nivel en la esquina izquierda
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
