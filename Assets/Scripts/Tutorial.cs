using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour,IPointerClickHandler
{
    [Header("Elementos del tutorial")]
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private Image imagenPaso;

    [Header("Assets de los pasos")]
    [SerializeField] private Sprite[] pasosTutorial;
    private int pasoActual = 0;
    private bool tutorialActivo = false;

    void Start()
    {
        panelTutorial.SetActive(false);
    }

    // Update is called once per frame
     public void IniciarTutorial()
    {
        if (pasosTutorial == null || pasosTutorial.Length == 0)
        {
            Debug.LogWarning("No hay pasos asignados al tutorial."); return;
        }

        pasoActual = 0;
        tutorialActivo = true;

        panelTutorial.SetActive(true);

        MostrarPaso();
    }

      public void OnPointerClick(PointerEventData eventData)
    {
        if (!tutorialActivo)
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

     public void FinalizarTutorial()
    {
        tutorialActivo = false;
        panelTutorial.SetActive(false);

        Debug.Log("Tutorial finalizado.");
    }

    private void MostrarPaso()
    {
        imagenPaso.sprite = pasosTutorial[pasoActual];
    }
}
