using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Slider musicaSlider;
    public Slider efectoSlider;
    public AudioSource audioMusica;
    public AudioSource audioEfectos;

    void Start()
    {

        audioMusica = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioEfectos = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("firstPlay") == 0)
        {
            MusicVolume(1);
            EffectVolume(1);
            PlayerPrefs.SetInt("firstPlay", 1);
        }
        else
        {
            audioMusica.volume = PlayerPrefs.GetFloat("volumeMusic");
            musicaSlider.value = PlayerPrefs.GetFloat("volumeMusic");
            audioEfectos.volume = PlayerPrefs.GetFloat("volumeEffects");
            efectoSlider.value = PlayerPrefs.GetFloat("volumeEffects");
        }


    }
    public void MusicVolume(float volumen)
    {
        audioMusica.volume = volumen;
        PlayerPrefs.SetFloat("volumeMusic", volumen);

    }
    public void EffectVolume(float volumen)
    {
        audioEfectos.volume = volumen;
        PlayerPrefs.SetFloat("volumeEffects", volumen);
    }
}
