using UnityEngine;
using UnityEngine.UI;

public class VolumenIndependiente : MonoBehaviour
{
    public AudioSource sonidoButton;
    public Slider sliderVolumen;

    void Start()
    {
        sliderVolumen.value = sonidoButton.volume;
    }

    public void CambioVolumen()
    {
        sonidoButton.volume = sliderVolumen.value;
    }
}
