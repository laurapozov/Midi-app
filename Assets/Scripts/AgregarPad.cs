using UnityEngine;
using UnityEngine.UI;

public class AgregarPad : MonoBehaviour
{
    [Header("Configuración de los pads")]
    [SerializeField] private Transform contenedorPads;
    [SerializeField] private GameObject prefabPad;

    [Header("Botón para agregar")]
    [SerializeField] private Button botonAgregar;

    private const int MAX_PADS = 8;

    private void Start()
    {
        ActualizarBoton();
    }

    public void AgregarNuevoPad()
    {
        if (contenedorPads.childCount >= MAX_PADS)
        {
            ActualizarBoton();
            return;
        }

        Instantiate(prefabPad, contenedorPads);

        ActualizarBoton();
    }

    private void ActualizarBoton()
    {
        botonAgregar.interactable = contenedorPads.childCount < MAX_PADS;
    }
}
