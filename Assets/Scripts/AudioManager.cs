using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Musica;
    private AudioSource source;

    private int CancionActual;

    public Text TituloCancion;
    private int FullLenght;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    //Con esto conseguimos que reproduzca la primera cancion de la array
    public void ReproducirMusica()
    {
        if (source.isPlaying)
        {
            return;
        }

        CancionActual--;
        if (CancionActual < 0)
        {
            CancionActual = Musica.Length - 1;
        }
        StartCoroutine("WaitForMusicEnd");
    }

    //Con esto conseguimos que espere hasta que acabe de reproducirse la cancion, y que salte a la siguiente
    IEnumerator WaitForMusicEnd()
    {
        while (source.isPlaying)
        {
            yield return null;
        }
        SiguienteCancion();
    }

    //Aqui conseguimos irnos a la siguiente cancion y que se reproduzca
    public void SiguienteCancion()
    {
        source.Stop();
        CancionActual++;
        if (CancionActual > Musica.Length - 1)
        {
            CancionActual = 0;
        }
        source.clip = Musica[CancionActual];
        source.Play();

        MostarTitulo();

        StartCoroutine("WaitForMusicEnd");
    }

    //Aqui conseguimos irnos a la anterior cancion y que se reproduzca
    public void AnteriorCancion()
    {
        source.Stop();
        CancionActual--;
        if (CancionActual < 0)
        {
            CancionActual = Musica.Length - 1;
        }
        source.clip = Musica[CancionActual];
        source.Play();

        MostarTitulo();

        StartCoroutine("WaitForMusicEnd");
    }

    //Con esto paramos la cancion
    public void PausarMusica()
    {
        StopCoroutine("WaitForMusicEnd");
        source.Pause();
    }

    //Con esta funcion podemos mostrar el titulo de la cancion que se esta reproduciendo
    public void MostarTitulo()
    {
        TituloCancion.text = source.clip.name;
        FullLenght = (int)source.clip.length;
    }

    //Aqui conseguimos que se escoja una cancion aleatoria del array y que lo reproduzca
    public void CancionAleatoria()
    {
        source.clip = Musica[Random.Range(0, Musica.Length)];
        source.Play();
        MostarTitulo();
        StartCoroutine("WaitForMusicEnd");
    }
}
