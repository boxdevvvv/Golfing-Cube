using UnityEngine;
using UnityEngine.EventSystems;

public class BallControl : MonoBehaviour
{
    [SerializeField] private float MaxForce; //maxima fuerza aplicable a la bola
    [SerializeField] private float forceModifier = 0.5f; //fuerza que se agrega a la operacion matematica para que tome mas fuerza
    [SerializeField] private LayerMask rayLayer; //mascara usada para detectar el lugar de pulsacion con raycast, podria simplemente ponerse que al presionar tome la ubicacion pero no funcionaba de manera correcta cuando lo intente, Camera.main.ScreenToWorldPoint(Input.mousePosition) y similares no me daban el valor que necesitaba la bola, es probable que fuera culpa de la camara que esta a 60 grados

    public static float scale; // distancia que se alargara la flecha, es estatica ya que su valor es usado por el script "LineDirection"
    private float force;                                    //fuerza que se aplicara a la bola
    public Rigidbody rgBody;                               //rb de la bola
    /// <summary>
    /// </summary>
    private Vector3 startPos, endPos; //posicion toque inicio y ultima posicion del dedo antes de despegarse
    private Vector3 direction;                              //direccion en la que saldra la bola

    public GameObject arrowContainer; //Contenedor que contiene el linerenderer
    public Transform arrow; // esta es la flecha de la punta de la linea, en este script se le da la rotacion para que siga la direccion de la linea
    public bool activeTouch = false; // variable usada para hacer que no se pueda interactuar en ciertas condiciones, tambien para evitar multiples pulsaciones que bugueen el juego
    private void Start()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }
  

    private void Update()
    {

        if (GameManager._gameManager.canMove)  //esto se usa para jugar en PC
        {
            if (Input.GetMouseButtonDown(0) && !activeTouch)
            {
                PressScreen(); //detecta la ubicacion de la primera pulsacion y frena la bola
                activeTouch = true;
            }
            if (Input.GetMouseButton(0) && activeTouch)
            {
                DraggingInScreen(); //calcula la distancia de la primera pulsacion con la ubicacion de la pulsacion actual para tomar la direccion y fuerza
            }
            if (Input.GetMouseButtonUp(0) && activeTouch)
            {
                BallMovement(); //con la fuerza va a la ubicacion señalada
                print("Activo UI");
            }


            if (Input.touchCount > 0) //esto se usa para jugar en celular, ocupa las mismas funciones de lanzamiento
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && !activeTouch)
                {
                    int id = touch.fingerId;
                    if (EventSystem.current.IsPointerOverGameObject(id)) //esto se usa para identificar cuando tocas la UI para no activar las funciones 
                    {
                        print("toco ui");
                    }
                    else
                    {
                        PressScreen();
                        activeTouch = true;
                    }
                }
                if (touch.phase == TouchPhase.Moved && activeTouch)
                {
                    DraggingInScreen();
                }
                if (touch.phase == TouchPhase.Ended && activeTouch)
                {
                    BallMovement();
                }
            }
        }
        if (GameManager._gameManager.canRestart) // si la booleana del gameManager esta activa permite reiniciar la escena, esto ocurre cuando te quedas sin tiros
        {
            if (Input.touchCount > 0)
            {
                GameManager._gameManager.RestartLevel();
            }
            if (Input.GetMouseButtonDown(0))
            {
                GameManager._gameManager.RestartLevel();
            }
        }

    }
    public bool firstTry;
  

    public void PressScreen()
    {
        rgBody.velocity = Vector3.zero; //lo setea en 0
        rgBody.angularVelocity = Vector3.zero; //lo setea en 0
        startPos = ClickedPoint(); //toma la ubicacion de la pulsacion
        startPos.y = 0; //esto es para evitar que la bola pueda ir en direccion Y causando que salga volando      
        arrowContainer.gameObject.SetActive(true); // esto activa el LineRenderer para saber la direccion a la que apuntas
        if (!firstTry)
        {
            firstTry = true;
            UIManager._UIManager.esconderUI();
        }

    }
   
    public void DraggingInScreen()
    {
        endPos = ClickedPoint();   //ejecuta la funcion para saber la ubicacion actual de la pulsacion                                          
        endPos.y = 0;// esto es para evitar que la bola pueda ir en direccion Y causando que salga volando
        arrow.transform.rotation = Quaternion.LookRotation(direction);  // le da la rotacion a la flecha para que siga a la linea
        arrowContainer.transform.rotation = Quaternion.LookRotation(direction);  // le da la direccion al Linerenderer, es muy importante
        force = Mathf.Clamp(Vector3.Distance(endPos, startPos) * forceModifier, 5, MaxForce);   //calcula la fuerza
       // print("esta es la fuerza de lanzamiento "+force); //esto es para pruebas, actualmente me encuentro experimentando cuales serian los mejores numeros posibles para la fuerza
        scale = Mathf.Clamp(Vector3.Distance(endPos, startPos), -1.5f, 1.5f); // esto le da la distancia de largo a la flecha
              
        direction = (startPos - endPos).normalized; // operacion matematica que te permite ir a la ubicacion deseada    
    }

    void BallMovement()
    {
        
            activeTouch = false; //permite volver a acceder a la funcion de lanzamiento
            arrowContainer.gameObject.SetActive(false);  //apaga el linerenderer

            rgBody.AddForce(direction * force, ForceMode.Impulse);  //agrega la fuerza correspondiente
            force = 0;                                              //setea en 0 la fuerza
            startPos = endPos = Vector3.zero;                                 //las variables vuelven a 0 para evitar posibles errores

            GameManager._gameManager.PlayerMovement(); // se utiliza para descontar tiros disponibles en el canvas
        
    }
    Vector3 ClickedPoint()
    {
        Vector3 position = Vector3.zero;                                //setea la posicion en 0
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //lanza un rayo que permita saber la posicion utilizando un objeto invisible en el editor como muralla
        RaycastHit hit = new RaycastHit();                              
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer))    //checkea si hay un choque
        {
            position = hit.point;                                       //guarda la posicion del choque
        }
        return position;                                                //devuelve el valor
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            SoundManager.PlaySound("Pared");
        }
    }
}
