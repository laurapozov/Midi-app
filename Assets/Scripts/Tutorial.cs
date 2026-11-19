using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Elementos del tutorial")]
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private Image imagenPaso;

    [Header("Botones de flechas")]
    [SerializeField] private Button botonSiguiente;
    [SerializeField] private Button botonAnterior;

    [Header("Assets de los pasos")]
    [SerializeField] private Sprite[] pasosTutorial;

    private int pasoActual = 0;
    private bool tutorialActivo = false;

    void Start()
    {
        if (panelTutorial != null)
        {
            panelTutorial.SetActive(false);
        }

        if (botonSiguiente != null)
        {
            botonSiguiente.onClick.AddListener(SiguientePaso);
        }

        if (botonAnterior != null)
        {
            botonAnterior.onClick.AddListener(PasoAnterior);
        }

        ActualizarBotones();
    }

    public void IniciarTutorial()
    {
        if (pasosTutorial == null || pasosTutorial.Length == 0)
        {
            Debug.LogWarning("No hay pasos asignados al tutorial.");
            return;
        }

        pasoActual = 0;
        tutorialActivo = true;

        if (panelTutorial != null)
        {
            panelTutorial.SetActive(true);
        }

        MostrarPaso();
    }

    public void SiguientePaso()
    {
        if (!tutorialActivo || pasosTutorial == null || pasosTutorial.Length == 0)
            return;

        pasoActual++;

        if (pasoActual < pasosTutorial.Length)
        {
            MostrarPaso();
        }
        else
        {
            FinalizarTutorial();
        }
    }

    public void PasoAnterior()
    {
        if (!tutorialActivo || pasosTutorial == null || pasosTutorial.Length == 0)
            return;

        if (pasoActual > 0)
        {
            pasoActual--;
            MostrarPaso();
        }
    }

    public void FinalizarTutorial()
    {
        tutorialActivo = false;

        if (panelTutorial != null)
        {
            panelTutorial.SetActive(false);
        }

        Debug.Log("Tutorial finalizado.");
    }

    private void MostrarPaso()
    {
        if (imagenPaso != null && pasosTutorial != null && pasoActual >= 0 && pasoActual < pasosTutorial.Length)
        {
            imagenPaso.sprite = pasosTutorial[pasoActual];
        }

        ActualizarBotones();
    }

    private void ActualizarBotones()
    {
        if (botonAnterior != null)
        {
            botonAnterior.gameObject.SetActive(tutorialActivo && pasoActual > 0);
        }
    }
}