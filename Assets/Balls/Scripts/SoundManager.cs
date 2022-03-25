using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("no se destruyo en load el musica");

        }
        else
        {
            print("se destruyo musicObject");
            Destroy(gameObject);
        }

    }

    public static AudioClip Jump, bloqueNormal, BloqueBase, salto, musica, bloqueEspecial;
    public static AudioSource effectsScript;
    public static AudioSource musicScript;
    
    void Start()
    {
        musicScript = GetComponent<AudioSource>(); 

        effectsScript = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();

        bloqueNormal = Resources.Load<AudioClip>("PopSound");
        bloqueEspecial = Resources.Load<AudioClip>("PopSound2");

        salto = Resources.Load<AudioClip>("Colision");
        BloqueBase = Resources.Load<AudioClip>("BloqueBase");
        musica = Resources.Load<AudioClip>("airtone");


        print("iNICIO START DE MUSICA");
    }
    public static void PlaySound(string clip)
    {
           switch (clip)
           {
               case "BloqueNormal":
                   effectsScript.PlayOneShot(bloqueNormal);
                   break;
            case "BloqueEspecial":
                effectsScript.PlayOneShot(bloqueEspecial);
                break;

            case "Colision":
                   effectsScript.PlayOneShot(salto);
                   break;
               case "BloqueBase":
                   effectsScript.PlayOneShot(BloqueBase);
                   break;
           }
    }
}
