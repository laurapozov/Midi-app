using UnityEngine;
using UnityEngine.UI;

public class BloqueoYdesbloqueo : MonoBehaviour
{
    public Slider sliderbloqueo;
    public GameObject pads;
    public Image imagenCandado;
    public Sprite candadoNegro;
    public Sprite candadoBlanco;

    private CanvasGroup canvasPads;

    void Start()
    {
        if (pads != null)
        {
            canvasPads = pads.GetComponent<CanvasGroup>();
            if (canvasPads == null)
            {
                Debug.LogWarning("BloqueoYdesbloqueo: 'pads' no tiene un componente CanvasGroup.", this);
            }
        }
        else
        {
            Debug.LogWarning("BloqueoYdesbloqueo: falta asignar 'pads'.", this);
        }

        if (sliderbloqueo != null)
        {
            sliderbloqueo.value = 0;
            sliderbloqueo.onValueChanged.AddListener(CambiarEstado);
        }
        else
        {
            Debug.LogWarning("BloqueoYdesbloqueo: falta asignar 'sliderbloqueo'.", this);
        }

        Desbloquear();
    }

    void CambiarEstado(float value)
    {
        if (value >= 1f)
        {
            Bloquear();
        }
        else
        {
            Desbloquear();
        }
    }

    void Bloquear()
    {
        if (canvasPads != null)
        {
            canvasPads.interactable = false;
            canvasPads.blocksRaycasts = false;
        }

        if (imagenCandado != null && candadoNegro != null)
        {
            imagenCandado.sprite = candadoNegro;
        }
    }

    void Desbloquear()
    {
        if (canvasPads != null)
        {
            canvasPads.interactable = true;
            canvasPads.blocksRaycasts = true;
        }

        if (imagenCandado != null && candadoBlanco != null)
        {
            imagenCandado.sprite = candadoBlanco;
        }
    }
}