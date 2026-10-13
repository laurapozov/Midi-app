
using UnityEngine;
using UnityEngine.UI;

public class Bibliotecasonidos : MonoBehaviour
{
    public AudioSource[] sonidosPads;
    public Text[] textosPads;

    void OnEnable()
    {
        ActualizarBiblioteca();
    }

    public void ActualizarBiblioteca()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i >= sonidosPads.Length || i >= textosPads.Length)
                break;

            if (textosPads[i] == null)
                continue;

            string nombre = "Sin archivo";

            if (sonidosPads[i] != null &&
                sonidosPads[i].clip != null)
            {
                nombre = sonidosPads[i].clip.name;
            }

            textosPads[i].text = "Pad " +
                (i + 1).ToString("00") + " - " + nombre;
        }
    }
}
