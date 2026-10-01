using UnityEngine;

public class SonidoButton : MonoBehaviour
{
    public AudioSource FuenteAudio; //de donde sale el sonido

    public void reproducirSonido()
    {
        if (FuenteAudio != null)
        {
            FuenteAudio.Play();
        }
    }


}
