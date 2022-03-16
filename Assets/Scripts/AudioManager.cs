using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Musica;
    private AudioSource fuente;

    private int currentTrack;

    public Text TituloCancion;
    private int FullLenght;
    
    void Start()
    {
        fuente = GetComponent<AudioSource>();
    }

    //Con esto conseguimos que reproduzca la primera cancion de la array
    public void ReproducirMusica()
    {
        if (fuente.isPlaying)
        {
            return;
        }

        currentTrack--;
        if (currentTrack < 0)
        {
            currentTrack = Musica.Length - 1;
        }
        StartCoroutine("WaitForMusicEnd");
    }

    //Con esto conseguimos que espere hasta que acabe de reproducirse la cancion, y que salte a la siguiente
    IEnumerator WaitForMusicEnd()
    {
        while (fuente.isPlaying)
        {
            yield return null;
        }
        SiguienteCancion();
    }

    //Aqui conseguimos irnos a la siguiente cancion y que se reproduzca
    public void SiguienteCancion()
    {
        fuente.Stop();
        currentTrack++;
        if (currentTrack > Musica.Length - 1)
        {
            currentTrack = 0;
        }
        fuente.clip = Musica[currentTrack];
        fuente.Play();

        MostarTitulo();

        StartCoroutine("WaitForMusicEnd");
    }

    //Aqui conseguimos irnos a la anterior cancion y que se reproduzca
    public void AnteriorCancion()
    {
        fuente.Stop();
        currentTrack--;
        if (currentTrack < 0)
        {
            currentTrack = Musica.Length - 1;
        }
        fuente.clip = Musica[currentTrack];
        fuente.Play();

        MostarTitulo();

        StartCoroutine("WaitForMusicEnd");
    }

    //Con esto paramos la cancion
    public void PausarMusica()
    {
        StopCoroutine("WaitForMusicEnd");
        fuente.Pause();
    }

    //Con esta funcion podemos mostrar el titulo de la cancion que se esta reproduciendo
    public void MostarTitulo()
    {
        TituloCancion.text = fuente.clip.name;
        FullLenght = (int)fuente.clip.length;
    }

    //Aqui conseguimos que se escoja una cancion aleatoria del array y que lo reproduzca
    public void CancionAleatoria()
    {
        fuente.clip = Musica[Random.Range(0, Musica.Length)];
        fuente.Play();
        MostarTitulo();
        StartCoroutine("WaitForMusicEnd");
    }
}
