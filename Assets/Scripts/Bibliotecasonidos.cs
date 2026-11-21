using UnityEngine;
using UnityEngine.UI;

public class Bibliotecasonidos : MonoBehaviour
{
    public AudioSource[] sonidosPads;
    public Text[] textosPads;

    [Header("Selección y carga")]
    public IdentificadoSonidos identificadorSonidos;
    public int padSeleccionado = 0;

    void OnEnable()
    {
        ActualizarBiblioteca();
    }

    public void SeleccionarPad(int indice)
    {
        if (sonidosPads == null || sonidosPads.Length == 0)
            return;

        padSeleccionado = Mathf.Clamp(indice, 0, sonidosPads.Length - 1);
        ActualizarBiblioteca();
    }

    public void AsignarSonidoSeleccionadoAPad()
    {
        if (identificadorSonidos == null || identificadorSonidos.audioSeleccionado == null)
            return;

        AsignarClipAPad(padSeleccionado, identificadorSonidos.audioSeleccionado);
    }

    public void AsignarClipAPad(int indicePad, AudioClip clip)
    {
        if (clip == null || sonidosPads == null)
            return;

        if (indicePad < 0 || indicePad >= sonidosPads.Length)
            return;

        if (sonidosPads[indicePad] == null)
            return;

        sonidosPads[indicePad].clip = clip;
        ActualizarBiblioteca();
    }

    public void ActualizarBiblioteca()
    {
        if (sonidosPads == null || textosPads == null)
            return;

        int cantidad = Mathf.Min(6, Mathf.Min(sonidosPads.Length, textosPads.Length));

        for (int i = 0; i < cantidad; i++)
        {
            if (textosPads[i] == null)
                continue;

            string nombre = "Sin archivo";

            if (sonidosPads[i] != null && sonidosPads[i].clip != null)
            {
                nombre = sonidosPads[i].clip.name;
            }

            string marca = (i == padSeleccionado) ? "[Elegido] " : "";
            textosPads[i].text = marca + "Pad " + (i + 1).ToString("00") + " - " + nombre;
        }
    }
}

